using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace DesktopClient
{
    public partial class FormMap : Form
    {
        public Dictionary<string, string> tokenDictionary;
        public FormMap(Dictionary<string, string> token)
        {
            InitializeComponent();
            tokenDictionary = token;
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

        private void buttonAcceptOrder_Click(object sender, EventArgs e)
        {
            //Какая-либо проверка входных данных
            //if (s.Length > 0)//Проверка на непустую строку
            {
                //---Запрос---
                WebRequest request = WebRequest.Create("");
                //WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
                //    + "key=trnsl.1.1.20170125T084253Z.cc366274cc3474e9.68d49c802348b39b5d677c856e0805c433b7618c"//Ключ
                //    + "&text=" + s//Текст
                //    + "&lang=" + language);//Язык

                //Получаем ответ
                WebResponse response = request.GetResponse();
                //--------------------
                //---Распарсить JSON ответ. Я скачал фреймворк Json.NET
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    string line;
                    if ((line = stream.ReadLine()) != null)
                    {
                        //Translation translation = JsonConvert.DeserializeObject<Translation>(line);
                        //s = "";
                        //foreach (string str in translation.text)
                        //{
                        //    s += str;
                        //}
                    }
                }
                //------------------

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

        private void buttonMap_Click(object sender, EventArgs e)
        {

        }
    }
}
