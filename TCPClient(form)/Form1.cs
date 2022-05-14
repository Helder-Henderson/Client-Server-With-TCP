using System;
using System.Text;
using System.Windows.Forms;

/* Trabalhando com sockets */
using System.Net.Sockets;
using System.Threading;

namespace TCPClient_form_
{
    public partial class SocketClient : Form
    {
        TcpClient tcpClient;
        NetworkStream networkStream;
        Thread thInteraction;

        public SocketClient()
        {
            InitializeComponent();
        }

        private void connect()
        {
            tcpClient = new TcpClient();
            setMessage("## Estabelecendo conexão...");
            tcpClient.Connect("127.0.0.1", 8000);
        }
       
        private void disconnect()
        {
            if (thInteraction != null)
            {
                if (thInteraction.ThreadState == ThreadState.Running)
                    thInteraction.Abort();
            }

            tcpClient.Close();
        }

        private void sendMessage(string message)
        {
            if (networkStream.CanWrite)
            {
                byte[] sendBytes = Encoding.UTF8.GetBytes(message);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
            }

        }

        delegate void delSetMsg(string message);
        private void setMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new delSetMsg(setMessage), message);
            }else
            {
                rtbConversa.Text += "\nEu" + message;
            }
        }

        delegate void delGetMsg(string message);
        private void getMsg(string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new delGetMsg(getMsg), message);
            }
            else
            {
                rtbConversa.Text += "\nServer: " + message;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connect();

            thInteraction = new Thread(new ThreadStart(interaction));
            thInteraction.IsBackground = true;
            thInteraction.Priority = ThreadPriority.Highest;
            thInteraction.Name = "thInteraction";
            thInteraction.Start();
        }


        private void rtbMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(networkStream.CanWrite)
                {
                    string message = rtbMensagem.Text;
                    sendMessage(message);
                    setMessage(message);
                }else
                {
                    setMessage("## Não é possivel enviar dados deste stream");
                    disconnect();
                }
            }
        }

        private void rtbMensagem_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                rtbMensagem.Clear();
            }
        }
            
        private void interaction()
        {
            try
            {
                do
                {
                    networkStream = tcpClient.GetStream();
                    if (networkStream.CanRead)
                    {
                        byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                        networkStream.Read(bytes, 0, Convert.ToInt32(tcpClient.ReceiveBufferSize));

                        string returnData = Encoding.UTF8.GetString(bytes);
                        getMsg(returnData);
                    }else
                    {
                        setMessage("## Não é possivel ler dados para este stream... ");
                        disconnect();
                    }

                } while (tcpClient.Connected);

            }
            catch 
            {

            }
        }

        private void SocketClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            disconnect();
        }
    }
}
