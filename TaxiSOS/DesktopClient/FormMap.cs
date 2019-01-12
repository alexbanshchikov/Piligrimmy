using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopClient
{
    public partial class FormMap : Form
    {
        public FormMap()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Выбор подложки
            gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //Установка положения на карте по названию объекта(объектов)
            //gmap.SetPositionByKeywords("Novosibirsk");

            //Установка положения на карте по координатам
            //gmap.Position = new PointLatLng(-25.966688, 32.580528);

            //Создание и добавление маркера на карту
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(-25.966688, 32.580528),
              GMarkerGoogleType.green);
            markersOverlay.Markers.Add(marker);
            gmap.Overlays.Add(markersOverlay);

            //Получение маршрута между двумя точками
            PointLatLng start = new PointLatLng(56.4894541, 84.8685479); //56.4894541,84.8685479
            PointLatLng end = new PointLatLng(54.969655, 82.6692233); //54.969655,82.6692233
            MapRoute route = GMap.NET.MapProviders.OpenStreetMapProvider.Instance.GetRoute(
              start, end, false, false, 13);

            //Создание маршрута
            GMapRoute r = new GMapRoute(route.Points, "My route");
            r.Stroke.Width = 2;
            r.Stroke.Color = Color.Red;

            //Добавление маршрута на карту
            GMapOverlay routesOverlay = new GMapOverlay("routes");
            routesOverlay.Routes.Add(r);
            gmap.Overlays.Add(routesOverlay);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
