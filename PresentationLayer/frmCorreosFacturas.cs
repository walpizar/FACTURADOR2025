using MailKit.Net.Imap;
using MailKit.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmCorreosFacturas : Form
    {
        public frmCorreosFacturas()
        {
            InitializeComponent();
        }

        private void frmCorreosFacturas_Load(object sender, EventArgs e)
        {
            var email = "tu-email@gmail.com";
            var password = "tu-contraseña";

            using (var client = new ImapClient())
            {
                client.ConnectAsync("imap.gmail.com", 993, true);

                client.AuthenticateAsync(email, password);

                var inbox = client.Inbox;
                inbox.OpenAsync(MailKit.FolderAccess.ReadOnly);

                var query = SearchQuery.DeliveredAfter(DateTime.Now.AddDays(-1)); // Por ejemplo, correos de los últimos días
                var results = inbox.SearchAsync(query).Result;

                //foreach (var uid in results)
                //{
                //    var message =  inbox.GetMessageAsync(uid).Result;
                //    Console.WriteLine($"Asunto: {message.Subject}");

                //    if (message.Attachments != null && message.Attachments.Any())
                //    {
                //        foreach (var attachment in message.Attachments)
                //        {
                //            if (attachment is MimePart mimePart && mimePart.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                //            {
                //                Console.WriteLine($"Archivo adjunto XML encontrado: {mimePart.FileName}");
                //            }
                //        }
                //    }
                //}

                //client.DisconnectAsync(true).res;
            }
        }
    }
}
