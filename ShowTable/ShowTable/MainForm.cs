using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;

namespace ShowTable
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;

        private ITOCControl2 m_TOCControl = null;
        private IToolbarMenu2 m_MenuLayer = null;
        private IToolbarMenu2 m_MenuTOC = null;
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
            //初始化TOC变量
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;


            m_MenuTOC = new ToolbarMenuClass();
            //添加自定义菜单项到TOCCOntrol的图层菜单中
            m_MenuLayer = new ToolbarMenuClass();
            m_TOCControl = (ITOCControl2)this.axTOCControl1.Object;
            m_MenuLayer.AddItem(new OpenAttributeTb(axMapControl1), -1, 0, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_MenuLayer.SetHook(m_mapControl);
            m_MenuTOC.SetHook(m_mapControl);
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = Path.GetFileName(m_mapDocumentName);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 1)
            {
                //MessageBox.Show("左键按下");
            }
            else if (e.button == 2)
            {
                //MessageBox.Show("右键按下");
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap map = null;
                ILayer layer = null;
                object other = null;
                object index = null;
                //判断所选菜单的类型
                m_TOCControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
                //确定选定的菜单类型，Map或是图层菜单
                if (item == esriTOCControlItem.esriTOCControlItemMap)
                    m_TOCControl.SelectItem(map, null);
                else
                    m_TOCControl.SelectItem(layer, null);
                //设置CustomProperty为layer (用于自定义的Layer命令)                  
                m_mapControl.CustomProperty = layer;
                //弹出右键菜单
                //if (item == esriTOCControlItem.esriTOCControlItemMap)
                //    m_menuMap.PopupMenu(e.x, e.y, m_TOCControl.hWnd);
                if (item == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    m_MenuLayer.PopupMenu(e.x, e.y, m_TOCControl.hWnd);
                }
                else
                {
                    MessageBox.Show("shit!");
                }
            }
        }
    }
}