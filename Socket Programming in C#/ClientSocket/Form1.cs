using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSocket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Send(string message)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry("synclapn5500");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket clientSocket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(ipEndPoint);
            message += "<EOF>";
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            int sentCount=clientSocket.Send(bytes);
            lblStatus.Text = "bytes Sent " + sentCount;
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();

        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMessage.Text))
                Send(txtMessage.Text);
            txtMessage.Text = string.Empty;
        }
    }
}
