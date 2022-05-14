using System.Text;
using System.Net.Sockets;

namespace TCPServer_form_
{
    public partial class TCPServer : Form
    {
        int NUMERO_PORTA = 8000;
        
        TcpListener tcpListener;
        TcpClient tcpClient;
        NetworkStream networkStream;
        Thread thInteraction;


        public TCPServer()
        {
            InitializeComponent();
        }

        private void TCPServer_Load(object sender, EventArgs e)
        {
            start();
        }
            
        private bool connect()
        {
            bool _return = false;
            try
            {
                tcpListener = new TcpListener(System.Net.IPAddress.Any, NUMERO_PORTA);
                tcpListener.Start();
                _return = true;
            }catch {}
            return _return; 
        }

        private void disconnect()
        {
            if (thInteraction != null)
            {
                if (thInteraction.ThreadState == ThreadState.Running)
                    thInteraction.Abort();

            }

            if(tcpClient != null)
            {
                tcpClient.Client.Disconnect(true);
            }

            tcpListener.Stop();

            setMessage("## Conexões perdidas... ",true);

        }

        private void acceptConnection()
        {
            try
            {
                tcpClient = tcpListener.AcceptTcpClient();

            }catch { }
        }

        private void sendMessage(string message)
        {
            if (canWrite())
            {
                byte[] sendBytes = Encoding.ASCII.GetBytes(message);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
            }
            
        }

        private bool canWrite()
        {
            if (networkStream == null)
                return false;

            if (networkStream.CanWrite && tcpClient != null)
                return true;
            else
                return false;
        }

        delegate void delSetMsg(string message, bool escape);
        private void setMessage(string message, bool escape)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new delSetMsg(setMessage), message, escape);
            else
                if (escape == true || canWrite() == true)
                    rtbConversa.Text += "\nEu: " + message;
               
        }

        delegate void delGetMsg(string message);
        private void getMessage(string message)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new delGetMsg(getMessage), message);
            else
                if (canWrite() == true)
                rtbConversa.Text += "\nClient: " + message;
        }

        private void start()
        {
            if (connect())
                setMessage("## Aguardando uma conexão...",true);

            thInteraction = new Thread(new ThreadStart(interaction));
            thInteraction.IsBackground = true;
            thInteraction.Priority = ThreadPriority.Highest;
            thInteraction.Name = "thInteraction";
            thInteraction.Start();
        }

        private void interaction()
        {
            try
            {
                acceptConnection();
                setMessage("## Conexão aceita...",true);

                do
                {
                    networkStream = tcpClient.GetStream();
                    if (networkStream.CanRead)
                    {
                        byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                        networkStream.Read(bytes, 0, Convert.ToInt32(tcpClient.ReceiveBufferSize));

                        string clientData = Encoding.ASCII.GetString(bytes);
                        if (clientData.Replace("\0", "").Trim() != "")
                            getMessage(clientData);
                        else
                            tcpClient = null;
                    }
                } while (tcpClient != null);

                disconnect();
                start();

            }catch  {}
        }

        private void TCPServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            setMessage("## Encerando conexão com o servidor",true);
            disconnect();
        }

        private void rtbMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string message = rtbMensagem.Text;
                sendMessage(message);
                setMessage(message, false);
            }
        }

        private void rtbMensagem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rtbMensagem.Clear();
        }
    }
}