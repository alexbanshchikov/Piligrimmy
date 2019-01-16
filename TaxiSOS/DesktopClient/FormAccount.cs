using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient
{
    public partial class FormAccount : Form
    {
        public Dictionary<string, string> tokenDictionary;
        private const string APP_PATH = "http://localhost:53389";


        public FormAccount(Dictionary<string, string> token)
        {
            InitializeComponent();
            tokenDictionary = token;
            GetOrders();
        }

        public void GetOrders()
        {
            using (var client = new HttpClient())
            {
                var response =
                    client.GetAsync(APP_PATH + $"/api/Orders/GetAllDriverOrder?idDriver={tokenDictionary["id_Driver"]}").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != "")
                {
                    // Десериализация полученного JSON-объекта
                    List<Orders> orderList =
                    JsonConvert.DeserializeObject<List<Orders>>(result);
                    dataGridView1.DataSource = orderList;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[3].HeaderText = "От";
                    dataGridView1.Columns[4].HeaderText = "До";
                    dataGridView1.Columns[6].HeaderText = "Cтоимость";
                    // textBox1.Text = "Клиент отказался от поездки";
                    // idOrder = "";


                }
            }
        }
        private void buttonMap_Click(object sender, EventArgs e)
        {
            FormMap fm = new FormMap(tokenDictionary);
            fm.Show();
            this.Close();
        }

        private void buttonAccount_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxBusy_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonBackToMap_Click(object sender, EventArgs e)
        {

        }
    }

    public class Orders
    {
        public Guid IdOrder { get; set; }
        public Guid IdClient { get; set; }
        public Guid IdDriver { get; set; }
        public string ArrivalPoint { get; set; }
        public string DestinationPoint { get; set; }
        public DateTime OrderTime { get; set; }
        public int Cost { get; set; }
        public int Status { get; set; }

    }
}
