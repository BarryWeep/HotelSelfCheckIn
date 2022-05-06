using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Alpha_WindanginnAssist
{
    public partial class RemoteKeycut : Form
    {
        public RemoteKeycut()
        {
            InitializeComponent();
        }
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        static bool exitFlag = false;

        private void RemoteKeycut_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            label1.Hide();
        }
        private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Interval = 30000;
            string roomnumberdata = "0";
            string adddataData = "0";
            string StatusData = "0";


            MySqlConnection connectionwst = new MySqlConnection("datasource=10.0.4.143; username=barryT;password=barryT;database=windanginns;sslMode=none");

            MySqlCommand CntDatabase = new MySqlCommand("Select * from remotekeycard", connectionwst);

            CntDatabase.CommandTimeout = 60;

            MySqlDataReader reader;

            connectionwst.Open();
            reader = CntDatabase.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                roomnumberdata = reader.GetString(1);
                adddataData = reader.GetString(2);
                StatusData = reader.GetString(3);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Show();

            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 5 seconds.

            myTimer.Start();

            // Runs the timer, and raises the event.
            while (exitFlag == false)
            {
                // Processes all the events in the queue.
                Application.DoEvents();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myTimer.Stop();
            myTimer.Enabled = false;
            exitFlag = true;
            this.Close();
        }
    }
}
