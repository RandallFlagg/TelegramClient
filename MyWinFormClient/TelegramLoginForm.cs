using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelegramClientLib;

namespace MyWinFormClient
{
    public partial class TelegramLoginForm : Form
    {
        public TelegramHelper Helper { get; private set; }

        public TelegramLoginForm()
        {
            InitializeComponent();
            Helper = new TelegramHelper();
        }

        private void TelegramClientForm_Load(object sender, EventArgs e)
        {
            Helper.StartClient();
            if (Helper.IsConnected)
            {
                Close();
                //new TelegramClientForm(_helper).Show();
                //Hide(pnlLogin);
                //    Hide(tbPhoneNumber);
                //    Hide(btnLogin);
                //}
                //else
                //{
                //    Show(lblPhoneNumber);
                //    Show(tbPhoneNumber);
                //    Show(btnLogin);
            }
        }

        //private void Hide(Control control)
        //{
        //    control.Visible = false;
        //}

        //private void Show(Control control)
        //{
        //    control.Visible = true;
        //}

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblPhoneNumber.Visible = false;
            tbPhoneNumber.Visible = false;
            btnLogin.Visible = false;
            tbCode.Visible = true;
            btnVerify.Visible = true;
            await Helper.SendCodeRequest(tbPhoneNumber.Text);
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            await Helper.Authenticate(tbCode.Text);
            //await Helper.Connect();
            Close();
        }
    }
}
