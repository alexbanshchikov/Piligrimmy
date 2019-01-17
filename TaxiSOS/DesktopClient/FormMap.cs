using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        static string idClient;
        static double[] firstPoint = new double[2]; //56.48277 84.998985
        static double[] lastPoint = new double[2];  //56.480278 85.000206
        private const string APP_PATH = "http://localhost:53389";
        private BackgroundWorker worker;
        private BackgroundWorker worker2;
        private System.Timers.Timer timer;
        private System.Timers.Timer timer2;

        public FormMap(Dictionary<string, string> token)
        {
            InitializeComponent();
            idOrder = "";
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
                        if (key == "idClient")
                            idClient = orderDictionary[key];
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
                        timer2 = new System.Timers.Timer(10000);
                        timer2.Elapsed += timer2_Elapsed;
                        timer2.Start();

                        GetPoints(arrivalPoint, destinationPoint);
                    }
                    else
                    {
                        client.GetAsync(APP_PATH + $"/api/Orders/DriverIgnore?idOrder={idOrder}");
                        idOrder = "";
                        arrivalPoint = "";
                        destinationPoint = "";
                    }               
                }
            }
        }

        private void GetPoints(string From, string To)      //TODO Уебски определяется улица
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(APP_PATH + $"/api/Orders/GetCoordinates?From={arrivalPoint}?&To={destinationPoint}").Result;
                var result = response.Content.ReadAsStringAsync().Result;

                List<double> pointsList =
                    JsonConvert.DeserializeObject<List<double>>(result);

                firstPoint[0] = pointsList[0];
                firstPoint[1] = pointsList[1];

                lastPoint[0] = pointsList[2];
                lastPoint[1] = pointsList[3];
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
                    MessageBox.Show("Клиент отказался от поездки", "Отмена заказа", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    idOrder = "";               //TODO Вынести в отдельный метод
                    arrivalPoint = "";
                    destinationPoint = "";
                    firstPoint[0] = 0;
                    firstPoint[1] = 0;
                    lastPoint[0] = 0;
                    lastPoint[1] = 0;
                    timer2.Stop();
                    timer.Start();
                }
            }
        }

        //TODO Изменять тексст в кнопке после нажатия, в зависимости от текста, выполнять разные штуки
        private void buttonOnPlace_Click(object sender, EventArgs e)
        {
            if (idOrder != "")
            {
                using (var client = new HttpClient())
                {
                    var response =
                        client.GetAsync(APP_PATH + $"/api/Clients/SendMessageFinish?id={idClient}").Result;
                   // var result = response.Content.ReadAsStringAsync().Result;                   
                }

            }
        }

        private void buttonGetRoute_Click(object sender, EventArgs e)
        {
            //Установка положения на карте по координатам
            gmap.Position = new PointLatLng(firstPoint[0], firstPoint[1]);

            //Создание и добавление маркера на карту
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle markerFrom = new GMarkerGoogle(new PointLatLng(firstPoint[0], firstPoint[1]),
                GMarkerGoogleType.green);
            GMarkerGoogle markerTo = new GMarkerGoogle(new PointLatLng(lastPoint[0], lastPoint[1]),
                GMarkerGoogleType.green);
            markersOverlay.Markers.Add(markerFrom);
            markersOverlay.Markers.Add(markerTo);
            gmap.Overlays.Add(markersOverlay);

            //Получение маршрута между двумя точками
            PointLatLng start = new PointLatLng(firstPoint[0], firstPoint[1]);
            PointLatLng end = new PointLatLng(lastPoint[0], lastPoint[1]);
            MapRoute route = GMap.NET.MapProviders.OpenStreetMapProvider.Instance.GetRoute(
              start, end, false, false, 13);

            //Создание маршрута
            GMapRoute r = new GMapRoute(route.Points, "My route");
            r.Stroke.Width = 1;
            r.Stroke.Color = Color.Red;

            //Добавление маршрута на карту
            GMapOverlay routesOverlay = new GMapOverlay("routes");
            routesOverlay.Routes.Add(r);
            gmap.Overlays.Add(routesOverlay);
        }

        private void checkBoxBusy_CheckedChanged(object sender, EventArgs e)
        {
            //TODO Запрос на сервер с изменением статуса
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


                        