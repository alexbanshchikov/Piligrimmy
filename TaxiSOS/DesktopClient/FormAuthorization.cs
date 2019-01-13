using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel;

namespace DesktopClient
{
    public partial class FormAuthorization : Form
    {
        public FormAuthorization()
        {
            InitializeComponent();
        }

        private void buttonAuthorize_Click(object sender, EventArgs e)
        {
            Account account = new Account(); //TODO Переделать DataModel на библиотеку классов .NET Standart

            string username = textBoxLogin.Text;
            string password = textBoxPassword.Text;

            //WebRequest request = WebRequest.Create("http://localhost:8080/token");
            //WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
            //    + "key=trnsl.1.1.20170125T084253Z.cc366274cc3474e9.68d49c802348b39b5d677c856e0805c433b7618c"//Ключ
            //    + "&text=" + s//Текст
            //    + "&lang=" + language);//Язык



            //Получаем ответ
            //WebResponse response = request.GetResponse();
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

        /*
         var tokenKey = "accessToken";
var clientKey = "id_Client";
$('#submitLogin').click(function (e) {
    e.preventDefault();
    var loginData = {
        grant_type: 'password',
        username: $('#emailLogin').val(),
        password: $('#passwordLogin').val()
    };

    $.ajax({
        type: 'POST',
        url: '/token',
        data: loginData
    }).success(function (data) {
        $('.userName').text(data.username);
        $('.id_client').text(data.id_Client);

        sessionStorage.setItem(tokenKey, data.access_token);
        sessionStorage.setItem(clientKey, data.id_Client);
        window.location.href = "/Index.html";
    }).fail(function (data) {
        console.log(data);
    });
});
         */
    }
}
