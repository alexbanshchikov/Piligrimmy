using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace DesktopClient
{
    public partial class FormMap : Form
    {
        static Dictionary<string, string> tokenDictionary;
        static string idOrder;
        private const string APP_PATH = "http://localhost:53389";
        private BackgroundWorker worker;

        public FormMap(Dictionary<string, string> token)
        {
            InitializeComponent();
            tokenDictionary = token;
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response =
                    client.GetAsync(APP_PATH + $"/api/Orders/CheckClient?idDriver={tokenDictionary["id_Driver"]}").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != "")
                {
                    // Десериализация полученного JSON-объекта
                    Dictionary<string, string> orderDictionary =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                    foreach (var key in orderDictionary.Keys)
                    {
                        textBox1.Text += key + orderDictionary[key] + Environment.NewLine;
                    }
                }
            }
        }

        private async Task Form1_LoadAsync(object sender, EventArgs e)
        {
            //Выбор подложки
            gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            //Установка положения на карте по названию объекта(объектов)
            gmap.SetPositionByKeywords("Tomsk");

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

            await CreateProductAsync(textBox1);
        }

        async Task CreateProductAsync(TextBox textBox1)
        {
            while (true)
            {
                using (var client = new HttpClient())
                {
                    var response =
                        client.GetAsync(APP_PATH + $"/api/Orders/CheckClient?idDriver={tokenDictionary["access_token"]}").Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result == null)
                        continue;
                    else
                    {
                        // Десериализация полученного JSON-объекта
                        Dictionary<string, string> orderDictionary =
                            JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                        foreach (var key in orderDictionary.Keys)
                        {
                            textBox1.Text += key + orderDictionary[key] + Environment.NewLine;
                        }
                    }
                }
            }
        }

        private void ReceiveOrder()
        {
            using (var client = new HttpClient())
            {
                var response =
                    client.GetAsync(APP_PATH + $"/api/Orders/CheckClient?idDriver={tokenDictionary["access_token"]}").Result;
                var result = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, string> orderDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);


            }
        }

        

        private void buttonAcceptOrder_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response =
                    client.GetAsync(APP_PATH + $"/api/Orders/ChangeStatus?newStatus=2&idOrder={FormMap.tokenDictionary["access_token"]}").Result;
                var result = response.Content.ReadAsStringAsync().Result;

                // Десериализация полученного JSON-объекта
                Dictionary<string, string> orderDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                foreach (var key in orderDictionary.Keys)
                {
                    textBox1.Text += key + orderDictionary[key] + Environment.NewLine;
                }
            }
        }

        private void buttonOnPlace_Click(object sender, EventArgs e)
        {

        }

        private void buttonGetRoute_Click(object sender, EventArgs e)
        {

        }

        private void buttonIgnore_Click(object sender, EventArgs e)
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
    }
}
