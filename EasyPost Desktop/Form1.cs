using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using EasyPost;

namespace EasyPost_Desktop
{
    public partial class Form1 : System.Windows.Forms.Form
    {

        String url = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var appSettings = ConfigurationManager.AppSettings;
            from_name.Text = appSettings["from_name"];
            from_company.Text = appSettings["from_company"];
            from_phone.Text = appSettings["from_phone"];
            from_email.Text = appSettings["from_email"];
            from_street_1.Text = appSettings["from_street_1"];
            from_street_2.Text = appSettings["from_street_2"];
            from_city.Text = appSettings["from_city"];
            from_state.Text = appSettings["from_state"];
            from_zip.Text = appSettings["from_zip"];
            from_country.Text = appSettings["from_country"];
            to_name.Text = appSettings["to_name"];
            to_company.Text = appSettings["to_company"];
            to_phone.Text = appSettings["to_phone"];
            to_email.Text = appSettings["to_email"];
            to_street_1.Text = appSettings["to_street_1"];
            to_street_2.Text = appSettings["to_street_2"];
            to_city.Text = appSettings["to_city"];
            to_state.Text = appSettings["to_state"];
            to_zip.Text = appSettings["to_zip"];
            to_country.Text = appSettings["to_country"];
            length.Text = appSettings["length"];
            width.Text = appSettings["width"];
            heigth.Text = appSettings["heigth"];
            weight.Text = appSettings["weight"];
            predefined_package.Text = appSettings["predefined_package"];
            carrier.Text = appSettings["carrier"];
            service.Text = appSettings["service"];
            machinable.Text = appSettings["machinable"];
            label_size.Text = appSettings["label_size"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var appSettings = ConfigurationManager.AppSettings;
            ClientManager.SetCurrent(appSettings["api_key"]);

            Address fromAddress = new Address()
            {
                name = from_name.Text,
                company = from_company.Text,
                phone = from_phone.Text,
                email = from_email.Text,
                street1 = from_street_1.Text,
                street2 = from_street_2.Text,
                city = from_city.Text,
                state = from_state.Text,
                zip = from_zip.Text,
                country = from_country.Text
            };
            
            Address toAddress = new Address()
            {
                name = to_name.Text,
                company = to_company.Text,
                phone = to_phone.Text,
                email = to_email.Text,
                street1 = to_street_1.Text,
                street2 = to_street_2.Text,
                city = to_city.Text,
                state = to_state.Text,
                zip = to_zip.Text,
                country = to_country.Text
            };

            Parcel parcel = new Parcel()
            {
                length = Convert.ToDouble(length.Text),
                width = Convert.ToDouble(width.Text),
                height = Convert.ToDouble(heigth.Text),
                weight = Convert.ToDouble(weight.Text),
                predefined_package = predefined_package.Text
            };

            Options options = new Options()
            {
                label_size = label_size.Text,
                machinable = machinable.Text
            };

            Shipment shipment = new Shipment()
            {
                from_address = fromAddress,
                to_address = toAddress,
                parcel = parcel,
                options = options
            };

            try
            {
                shipment.Buy(shipment.LowestRate(
                includeCarriers: new List<string>() { carrier.Text },
                includeServices: new List<string>() { service.Text }
            ));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
            finally
            {
                url = shipment.postage_label.label_url;
                linkLabel1.Text = url;
            }
        }
        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }

        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited
            // to true.
            linkLabel1.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start(url);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }
    }
}
