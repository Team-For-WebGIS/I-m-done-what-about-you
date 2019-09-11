using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace ShowTable
{
    class OpenAttributeTb : BaseCommand
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private AxMapControl axMapControl1;
        private ITOCControl2 m_TOCControl = null;
        private IToolbarMenu2 m_MenuLayer = null;
        private IToolbarMenu2 m_MenuTOC = null;
        #endregion

        public OpenAttributeTb(AxMapControl pMapControl)
        {
            base.m_caption = "Open Attribute Table";
            axMapControl1 = pMapControl;
        }

        public override void OnClick()
        {
            FormTable formtable = new FormTable(axMapControl1, m_mapControl);
            formtable.Show();
            //base.OnClick();
        }

        public override void OnCreate(object hook)
        {
            m_mapControl = (IMapControl3)hook;
            //throw new NotImplementedException();
        }
    }
}
