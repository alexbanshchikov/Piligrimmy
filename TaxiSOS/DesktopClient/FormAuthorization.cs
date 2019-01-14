using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;

namespace DesktopClient
{
    public partial class FormAuthorization : Form
    {
        public FormAuthorization()
        {
            InitializeComponent();
        }

        private const string APP_PATH = "http://localhost:8080";
        static string token;
        static string idDriver;

        private void buttonAuthorize_Click(object sender, EventArgs e)
        {
            string login = "4578965878";//textBoxLogin.Text;
            string password = "111";//textBoxPassword.Text;

            Dictionary<string, string> tokenDictionary = GetTokenDictionary(login, password);

            token = tokenDictionary["access_token"];
            idDriver = tokenDictionary["id_Driver"];
        }

        Dictionary<string, string> GetTokenDictionary(string login, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "login", login ),
                    new KeyValuePair<string, string> ( "password", password )
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(APP_PATH + "/TokenDriver", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                // Десериализация полученного JSON-объекта
                Dictionary<string, string> tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                return tokenDictionary;
            }
        }

        static HttpClient CreateClient(string accessToken = "")
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return client;
        }

    }
}
