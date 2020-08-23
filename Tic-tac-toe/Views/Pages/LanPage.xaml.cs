using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tic_tac_toe.Views.Pages
{
    /// <summary>
    /// Interaction logic for LanPage.xaml
    /// </summary>
    public partial class LanPage : UserControl
    {
       /* private Socket sock;
        private BackgroundWorker MessageReceiver = new BackgroundWorker();
        private TcpListener server = null;
        private TcpClient client;
       */
        public LanPage() {
            InitializeComponent();
           // MessageReceiver.DoWork += MessageReceiver_DoWork;

        }
/*
        private void Host_Button_Click(object sender, RoutedEventArgs e) {
            server = new TcpListener(System.Net.IPAddress.Any, 5732);
            server.Start();
            sock = server.AcceptSocket();
        }

        private void Join_Button_Click(object sender, RoutedEventArgs e) {
            try {
                client = new TcpClient(IpAdress_TextBox.Text, 5732);
                sock = client.Client;
                MessageReceiver.RunWorkerAsync();
                this.Messages_TextBlock.Text = "Connection success";
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void MessageReceiver_DoWork(object sender, DoWorkEventArgs e) {
            RecieveMessage();
        }

        private void SendMessage() {
            byte[] num = { 1, 2, 3, 8 };
            sock.Send(num);
            //MessageReceiver.RunWorkerAsync();
        }

        private void RecieveMessage() {
            byte[] buffer = new byte[1];
            sock.Receive(buffer);
            // this.MessageType_TextBlock.Text += String.Join(" ", buffer);
        }

        private void SendMessage_Button_Click(object sender, RoutedEventArgs e) {
            SendMessage();
        }
*/
    }
}
