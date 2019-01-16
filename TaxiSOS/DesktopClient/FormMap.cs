using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Timers;
using System.Windows.Forms;

namespace DesktopClient
{
    public partial class FormMap : Form
    {
        static Dictionary<string, string> tokenDictionary;
        static string idOrder;
        static string arrivalPoint;
        static string destinationPoint;
        static double firstPoint;
        static double lastPoint;
        private const string APP_PATH = "http://localhost:53389";
        private BackgroundWorker worker;
        private BackgroundWorker worker2;
        private System.Timers.Timer timer;
        private System.Timers.Timer timer2;

        public FormMap(Dictionary<string, string> token)
        {
            InitializeComponent();
            tokenDictionary = token;
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            timer = new System.Timers.Timer(10000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)        //ReceiveOrder
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(APP_PATH + $"/api/Orders/CheckClient?idDriver={tokenDictionary["id_Driver"]}").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != "")
                {
                    // Десериализация полученного JSON-объекта
                    Dictionary<string, string> orderDictionary =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                    foreach (var key in orderDictionary.Keys)
                    {
                        if (key == "idOrder")
                            idOrder = orderDictionary[key];
                        if (key == "arrivalPoint")
                            arrivalPoint = orderDictionary[key];
                        if (key == "destinationPoint")
                            destinationPoint = orderDictionary[key];
                    }

                    DialogResult dialogResult = MessageBox.Show("Пункт отправления: " + orderDictionary["arrivalPoint"] + Environment.NewLine +
                                                                "Пункт назначения: "  + orderDictionary["destinationPoint"] + Environment.NewLine +
                                                                "Стоимость: "         + orderDictionary["cost"],
                        "Входящий заказ", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        timer.Stop();
                        worker2 = new BackgroundWorker();
                        worker2.DoWork += worker2_DoWork;
                        System.Timers.Timer timer2 = new System.Timers.Timer(10000);
                        timer2.Elapsed += timer2_Elapsed;
                        timer2.Start();
                    }
                    else
                    {
                        client.GetAsync(APP_PATH + $"/api/Orders/DriverIgnore?idDriver={idOrder}");
                        idOrder = "";
                        arrivalPoint = "";
                        destinationPoint = "";
                    }               
                }
            }
        }

        void timer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!worker2.IsBusy)
                worker2.RunWorkerAsync();
        }

        void worker2_DoWork(object sender, DoWorkEventArgs e)       //DeclineOrder
        {
            using (var client = new HttpClient())
            {
                var response =
                    client.GetAsync(APP_PATH + $"/api/Orders/CheckDenyClient?idOrder={idOrder}&idDriver={tokenDictionary["id_Driver"]}").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != "")
                {
                    // Десериализация полученного JSON-объекта
                    Dictionary<string, string> orderDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                    MessageBox.Show("Клиент отказался от поездки", "Отмена заказа", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    idOrder = "";
                    arrivalPoint = "";
                    destinationPoint = "";
                    firstPoint = 0;
                    lastPoint = 0;
                    timer2.Stop();
                    timer.Start();
                }
            }
        }

        private void buttonOnPlace_Click(object sender, EventArgs e)
        {

        }

        private void buttonGetRoute_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxBusy_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonAccount_Click(object sender, EventArgs e)
        {
            FormAccount fa = new FormAccount(tokenDictionary);
            fa.Show();
            this.Close();
        }

        private void FormMap_Load(object sender, EventArgs e)
        {
            //Выбор подложки
            gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //Установка положения на карте по названию объекта(объектов)
            gmap.SetPositionByKeywords("Tomsk");

            //Установка положения на карте по координатам
            //gmap.Position = new PointLatLng(-25.966688, 32.580528);

            //МАРКЕР
            //Создание и добавление маркера на карту
            /*GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(-25.966688, 32.580528),
              GMarkerGoogleType.green);
            markersOverlay.Markers.Add(marker);
            gmap.Overlays.Add(markersOverlay);*/

            //МАРШРУТ
            //Получение маршрута между двумя точками
            /*PointLatLng start = new PointLatLng(56.4894541, 84.8685479); //56.4894541,84.8685479
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
            gmap.Overlays.Add(routesOverlay);*/
        }
    }
}
