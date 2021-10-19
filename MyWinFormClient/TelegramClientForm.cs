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
    public partial class TelegramClientForm : Form
    {
        private readonly TelegramHelper _helper;

        public TelegramClientForm(TelegramHelper helper)
        {
            InitializeComponent();
            _helper = helper;
        }

        private async void TelegramClientForm_Load(object sender, EventArgs e)
        {
            var result = await _helper.GetContacts();
            var contacts = result.Contacts;
            var users = result.Users;

            IList<ListViewItem> lvis = new ListViewItem[users.Count];
            int i = 0;
            foreach (TeleSharp.TL.TLUser user in users)
            {
                var lvi = new ListViewItem(new string[] { $"{user.FirstName} {user.LastName}", user.Phone, user.Id.ToString()});//TODO: Can be changed to object?
                lvis[i] = lvi;
                i++;
            }
            lvContacts.Items.AddRange(lvis.ToArray());
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            //var phone = lvContacts.SelectedItems[0].SubItems[1].Text;
            var id = lvContacts.SelectedItems[0].SubItems[2].Text;
            await _helper.SendMessage(int.Parse(id), rtbMessage.Text);
            await addMoneyToUser(id);
        }

        private Task addMoneyToUser(string id)
        {
            //throw new NotImplementedException();
        }
    }
}
