using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace MapView
{
    public partial class MapViewForm : Form
    {
        private int mMouseFlag;
        //放大
        private ZoomIn mZoomIn = null;

        public MapViewForm()
        {
            InitializeComponent();
        }

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            //if (e.button == 1)
            //    this.axMapControl1.Extent = this.axMapControl1.TrackRectangle();
            //else if (e.button == 2)
            //    this.axMapControl1.Pan();

            //空间点查询
            if (mMouseFlag==1)
            {
                IFeatureLayer pFeatureLayer;
                IFeatureClass pFeatureClass;
                //获取图层和要素类，为空时返回
                pFeatureLayer = this.axMapControl1.Map.get_Layer(0) as IFeatureLayer ;
                if (pFeatureLayer.Name != "states")
                    return;                
                pFeatureClass = pFeatureLayer.FeatureClass;
                if (pFeatureClass == null)
                    return;

                IActiveView pActiveView;
                IPoint pPoint;
                double length;
                //获取视图范围
                pActiveView = this.axMapControl1.ActiveView;
                //获取鼠标点击屏幕坐标
                pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                //屏幕距离转换为地图距离
                length = ConvertPixelToMapUnits(pActiveView, 2);


                ITopologicalOperator pTopoOperator;
                IGeometry pGeoBuffer;
                ISpatialFilter pSpatialFilter;
                //根据缓冲半径生成空间过滤器
                pTopoOperator = pPoint as ITopologicalOperator;
                pGeoBuffer = pTopoOperator.Buffer(length);

                pSpatialFilter = new SpatialFilterClass();
                pSpatialFilter.Geometry = pGeoBuffer;
                //根据图层类型选择缓冲方式
                switch (pFeatureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        break;
                }
                //定义空间过滤器的空间字段
                pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;

                IQueryFilter pQueryFilter;
                IFeatureCursor pFeatureCursor;
                IFeature pFeature;
                //利用要素过滤器查询要素
                pQueryFilter = pSpatialFilter as IQueryFilter;
                pFeatureCursor = pFeatureLayer.Search(pQueryFilter, true);
                pFeature = pFeatureCursor.NextFeature();
                int fieldIndex;
                if(pFeature!=null)
                {
                    //选择指定要素
                    this.axMapControl1.Map.ClearSelection();
                    this.axMapControl1.Map.SelectFeature((ILayer)pFeatureLayer, pFeature);
                    this.axMapControl1.Refresh();
                    fieldIndex = pFeature.Fields.FindField("STATE_NAME");
                    MessageBox.Show("查找到“" + pFeature.get_Value(fieldIndex) + "”", "提示");
                }
            }

            if (e.button == 1)
            {
                //拉框放大
                if (mZoomIn != null)
                    mZoomIn.OnMouseDown(e.button, e.shift, e.x, e.y);
            }
            else if (e.button == 2)
            {
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPan;
                axMapControl1.Pan();
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
        }
        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            //拉框放大
            if (mZoomIn != null&&mZoomIn.isMouseDown())
                mZoomIn.OnMouseMove(e.button, e.shift, e.x, e.y);
        }

        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            //拉框放大
            if (mZoomIn != null)
                mZoomIn.OnMouseUp(e.button, e.shift, e.x, e.y);
        }

        private void txtStateName_KeyUp(object sender, KeyEventArgs e)
        {
            //判断鼠标键值，如果Enter键按下抬起后，进入查询
            if (e.KeyCode==Keys.Enter)
            {
                //定义图层，要素游标，查询过滤器，要素
                IFeatureLayer pFeatureLayer;
                IFeatureCursor pFeatureCursor;
                IQueryFilter pQueryFilter;
                IFeature pFeature;

                //获取图层
                pFeatureLayer = this.axMapControl1.Map.get_Layer(0) as IFeatureLayer;
                //如果图层名称不是states，程序退出
                if (pFeatureLayer.Name != "states")
                    return;
                //清除上次查询结果
                this.axMapControl1.Map.ClearSelection();

                //pQueryFilter的实例化
                pQueryFilter = new QueryFilterClass();
                //设置查询过滤条件
                pQueryFilter.WhereClause = "STATE_NAME='" + txtStateName.Text + "'";
                //查询
                pFeatureCursor = pFeatureLayer.Search(pQueryFilter, true);
                //获取查询到的要素
                pFeature = pFeatureCursor.NextFeature();
                //判断是否获取到要素
                if (pFeature!=null)
                {
                    //选择要素
                    this.axMapControl1.Map.SelectFeature(pFeatureLayer, pFeature);
                    //放大到要素
                    this.axMapControl1.Extent = pFeature.Shape.Envelope;
                }
                else
                {
                    //没有得到pFeature的提示
                    MessageBox.Show("没有找到名为" + txtStateName.Text + "的州", "提示");
                }
            }
        }
        private void btnPointQuery_Click(object sender, EventArgs e)
        {
            //标记点查询
            mMouseFlag = 1;
            //设置鼠标形状
            this.axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerCrosshair;

        }

        /// <summary>
        /// 根据屏幕像素计算实际的地理距离
        /// </summary>
        /// <param name="activeView">屏幕视图</param>
        /// <param name="pixelUnits">像素个数</param>
        /// <returns></returns>
        private double ConvertPixelToMapUnits(IActiveView activeView, double pixelUnits)
        {
            double realWorldDiaplayExtent;
            int pixelExtent;
            double sizeOfOnePixel;
            double mapUnits;
            
            //获取设备中视图显示宽度，即像素个数
            pixelExtent=activeView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right-
                activeView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;
            //获取地图坐标系中地图显示范围
            realWorldDiaplayExtent = activeView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            //每个像素大小代表的实际距离
            sizeOfOnePixel = realWorldDiaplayExtent / pixelExtent;
            //地理距离
            mapUnits = pixelUnits * sizeOfOnePixel;

            return mapUnits;
        }

        private void btnFixedZoomIn_Click(object sender, EventArgs e)
        {
            //声明与初始化
            FixedZoomIn fixedZoomin = new FixedZoomIn();
            //与MapControl关联
            fixedZoomin.OnCreate(this.axMapControl1.Object);
            fixedZoomin.OnClick();


        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            //初始化
            mZoomIn = new ZoomIn();
            //与MapControl的关联
            mZoomIn.OnCreate(this.axMapControl1.Object);
            //设置鼠标形状
            this.axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
        }






    }
}