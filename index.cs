using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha_WindanginnAssist
{
    public partial class index : Form
    {
        public index()
        {
            InitializeComponent();
        }


        private void index_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnlineRegisterForm onlineRF = new OnlineRegisterForm();
            onlineRF.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RemoteKeycut remotekeycut = new RemoteKeycut();

            remotekeycut.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemoteKeycut remotekeycut = new RemoteKeycut();

            remotekeycut.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OfflineKeyRetrieve offlinekeycut = new OfflineKeyRetrieve();

            offlinekeycut.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RemoteKeycut remotekeycut = new RemoteKeycut();

            remotekeycut.ShowDialog();
        }
    }
}
