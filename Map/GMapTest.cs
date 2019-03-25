/************************************************************************ 
 * 项目名称 ：Map
 * 项目描述 ：
 * 文件名称 ：GMapTest.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2015
 * 创建时间 ：2019/3/14 15:53:27
 * 更新时间 ：2019/3/14 15:53:27
************************************************************************
 * Copyright @ Njbosa 2019. All rights reserved.
************************************************************************/
using GMap.NET;
using GMap.NET.MapProviders;
using GMapProvidersExt;
using System.Windows.Forms;

namespace Map
{
    public partial class GMapTest : Form
    {
        public GMapTest()
        {
            InitializeComponent();
        }

        private void GMapTest_Load(object sender, System.EventArgs e)
        {
            gmap.MapProvider = AMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmap.SetPositionByKeywords("南京");
            gmap.MinZoom = 0;
            gmap.MaxZoom = 18;
            gmap.Zoom = 10;
            gmap.DragButton = MouseButtons.Left;
            //gmap.Position = new GMap.NET.PointLatLng(39.923518, 116.539009);
        }

        private void GMapTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            gmap.Dispose();
        }
    }
}
