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
using Kantor.API;

namespace Kantor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cur_data.View = View.Details;
            cur_data.GridLines = true;
            cur_data.FullRowSelect = true;
            cur_data.Columns.Add("Waluta", 70);
            cur_data.Columns.Add("Kupno", 70);
            cur_data.Columns.Add("Sprzedaż", 70);
            cur_data.Columns.Add("Wzrost/Spadek", 100);

            GetData("EUR");
            GetData("USD");
            GetData("CHF");
            GetData("GBP");
            GetData("JPY");

        }
        public void GetData(string curr)
        {
            
            string[] data = new string[4];
            ListViewItem dataitm;
            data[0] = curr;
            WebRequest request = HttpWebRequest.Create("http://api.nbp.pl/api/exchangerates/rates/c/" + curr + "/last/2/?format=json");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string currency_JSON = reader.ReadToEnd();
            RootObject myCurrency = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(currency_JSON);
            var today_data = myCurrency.rates.LastOrDefault();
            var yesterday_data = myCurrency.rates.FirstOrDefault();
            data[1] = Math.Round(today_data.bid,3).ToString();
            data[2] = Math.Round(today_data.ask,3).ToString();
            data[3] = Math.Round(((today_data.ask + today_data.bid) - (yesterday_data.ask + yesterday_data.bid)),5).ToString();
            dataitm = new ListViewItem(data);
            cur_data.Items.Add(dataitm);
            if (String.IsNullOrEmpty(lblDate.Text))
            {
                lblDate.Text = today_data.effectiveDate.ToString();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                GetData(tbxCurr.Text.ToUpper());
            }
            catch
            {
                MessageBox.Show($"{tbxCurr.Text} nie jest skrótem walutowym", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (cur_data.SelectedItems.Count != 1)
            {
                MessageBox.Show("Wybierz dane", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cur_data.Items.Remove(cur_data.SelectedItems[0]);
        }
    }
}
