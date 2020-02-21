using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipsPlayer1
{
    public partial class Form1 : Form
    {
        TcpListener mTCPListener;
        
        TcpClient mTCPClient;
        byte[] mRx;
        IPAddress IP;
        public  bool playerFound = false;
        Player player;
        Player oponent;
        public Form1()
        {
            InitializeComponent();
        }
        int[,] tempOpponentBoard = new int[7,7] {
            { 0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0}
        };
        int tempNumOfShipLocations = 0;
        public int mx { get; private set; }
        public int my { get; private set; }

        void setOponentShipPart(int x, int y)
        {
            tempOpponentBoard[y, x] = 1;
        }
        void onCompleteReadTCPClientStream(IAsyncResult iar)
        {
            TcpClient tcpc;
            int nCountReadBytes = 0;
            string strRecv;
            try
            {
                tcpc = (TcpClient)iar.AsyncState;

                nCountReadBytes = tcpc.GetStream().EndRead(iar);
                //if no data is recieved, it closes the connection
                if (nCountReadBytes == 0)
                {
                    printLine("Oponent Disconected");
                    return;
                }
                //converts recieved bytes to ASCII characters
                strRecv = Encoding.ASCII.GetString(mRx, 0, nCountReadBytes);
                //prints message in the textBox
              
                //prints message in the textBox


                string[] recievedPacket = strRecv.Split(',');
                int code = int.Parse(recievedPacket[0]);
                if (code == 1)
                {
                    string name = recievedPacket[1];
                    printLine(name + " Connected");
                    oponent = new Player(name, new Board());
                    



                }
                if (code == 2)
                {
                    string[] cords = recievedPacket[1].Split(':');
                    int x = int.Parse(cords[0]);
                    int y = int.Parse(cords[1]);
                    setOponentShipPart(x, y);
                    this.tempNumOfShipLocations++;

                }



                //prepares for the new read
                mRx = new byte[512];
                tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadTCPClientStream, tcpc);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       
        void onCompleteAcceptTcpClient(IAsyncResult iar)
        {
            TcpListener tcpl = (TcpListener)iar.AsyncState;
            try
            {
                //Async accepts incoming connection
                mTCPClient = tcpl.EndAcceptTcpClient(iar);

                //this represents a packet
                tcpl.BeginAcceptTcpClient(onCompleteAcceptTcpClient, tcpl);
                mRx = new byte[512];
                //reads data from network stream
                mTCPClient.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadTCPClientStream, mTCPClient);
                printLine("Opponent found");
                
                sendName();
                sendBoard();
               

                playerFound = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public void sendBoard()
        {
            int[,] board = player.getBoard();

            //MessageBox.Show(packet);
            try {
                if (mTCPClient != null)
                {
                    if (mTCPClient.Client.Connected)
                    {

                        for (int i = 0; i < 7; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {

                                if (board[i, j] == 1)
                                {
                                    string packet = "2," + i + ":" + j;
                                    byte[] tx;
                                    //MessageBox.Show(packet);
                                    tx = Encoding.ASCII.GetBytes(packet);
                                    printLine("Code 1: Ship part Sent" + i + "-" + j + " to :" + this.oponent.getName());
                                    mTCPClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToClient, mTCPClient);
                                }

                            }
                        }
                    }

                }
            }catch(Exception ex)
            {
                MessageBox.Show("Ovde greska:" + ex);
            }
            

        }
        void onCompleteWriteToClient(IAsyncResult iar)
        {
            TcpClient tcpc;
            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                tcpc.GetStream().EndWrite(iar);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
        }

        //Sends current players name
        public void sendName()
        {
            string name = tbUsername.Text;
            string packet = "1," + name.Trim(' ');
            byte[] tx;
            //MessageBox.Show(packet);
            tx = Encoding.ASCII.GetBytes(packet);
            if (mTCPClient != null)
            {
                if (mTCPClient.Client.Connected)
                {
                    printLine("Code 1: Name Sent to :" + mTCPClient.ToString());
                    mTCPClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToClient, mTCPClient);
                }
            }
        }
        public void printLine(string _strPrint)
        {
            tbLogBox.Invoke(new Action<string>(doInvoke), _strPrint);

        }
        public void doInvoke(string _strPrint)
        {
            tbLogBox.Text = _strPrint + Environment.NewLine + tbLogBox.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string strThisHostName = string.Empty;
            IPHostEntry thisHostDNSEntry = null;
            IPAddress[] allIPsOfThisHost = null;
            IPAddress ipv4Ret = null;
            try
            {

                
                //Codes 1:getName 2:getShipCordinates 
                string name = tbUsername.Text;
                string packet = "1:"+name.Trim(' ');
                if (String.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Enter Username before start looking for an Opponent");
                    return;
                }
               

                strThisHostName = System.Net.Dns.GetHostName();
              //  printLine(strThisHostName);
                thisHostDNSEntry = System.Net.Dns.GetHostEntry(strThisHostName);
                allIPsOfThisHost = thisHostDNSEntry.AddressList;
                //Stores all adreses into IP Array, And looks for ipv4 addres
                for (int idx = allIPsOfThisHost.Length - 1; idx > 0; idx--)
                {
                    if (allIPsOfThisHost[idx].AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipv4Ret = allIPsOfThisHost[idx];
                        break;

                    }
                }

                player = new Player(tbUsername.Text.ToString(), new Board());
            }
            catch (Exception ex)
            //reads data from network stream
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string text = lbIP.Text;
            this.IP = ipv4Ret;
            lbIP.Text = text + ipv4Ret.ToString();

            startListening();


        }
        void startListening()
        {
            IPAddress ipAddr = this.IP;
            int nPort = 23000;

            //Converting from String to int
         


            //Converting from String to IPAddress
           
            //creating new TCPListener with given ip addres and port
            mTCPListener = new TcpListener(ipAddr, nPort);
            //Start Listening for incoming connections

            mTCPListener.Start();
            //Accepting begins accepting incoming connection    
            mTCPListener.BeginAcceptTcpClient(onCompleteAcceptTcpClient, mTCPListener);
        }

        //Player board
        private void pnlBoard_Paint(object sender, PaintEventArgs e)
        {
            if(player == null)
            {
                return;
            }
            if (String.IsNullOrEmpty(player.getName()) )
            {
                return;
            }

            
           
                int spacing = 5;
              
                System.Drawing.Graphics graphicsOb;
                graphicsOb = pnlBoard.CreateGraphics();
                Pen pen = new Pen(System.Drawing.Color.Blue);

            for(int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    pen.Color = System.Drawing.Color.Blue;

                    Rectangle rect = new Rectangle(spacing + i * 30, spacing + j * 30 + 10, 30 - 2 * spacing, 30 - 2 * spacing);
                    if (player.getBoard()[j,i] == 1)
                    {
                        pen.Color = System.Drawing.Color.Brown;
                    }

                    if (player.getBoard()[i, j] == 2)
                    {
                        pen.Color = System.Drawing.Color.Black;
                    }
                    graphicsOb.DrawRectangle(pen, rect);
                    
                }
            }



           Thread.Sleep(500);

          

        }
        //Oppoent Board
        private void pnlOponent_Paint(object sender, PaintEventArgs e)
        {
            
            if(tempNumOfShipLocations < 16)
            {
                return;
            }
            oponent.setBoard(tempOpponentBoard);
            int spacing = 5;
            
            System.Drawing.Graphics graphicsOb;
            graphicsOb = pnlOponent.CreateGraphics();
            Pen pen = new Pen(System.Drawing.Color.Blue);
            
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Rectangle rect = new Rectangle(spacing + i * 30, spacing + j * 30 + 10, 30 - 2 * spacing, 30 - 2 * spacing);
                    if (mx >= spacing + i * 30 && mx < spacing + i * 30 + 30 - 2 * spacing && my >= spacing + j * 30 && my < spacing + j * 30 + 15 + spacing)
                    {
                        pen.Color = System.Drawing.Color.Red;
                    }

                    if (oponent.getBoard()[j,i] == 1)
                    {
                        pen.Color = System.Drawing.Color.Brown;
                    }

                    if (oponent.getBoard()[i, j] == 2)
                    {
                        pen.Color = System.Drawing.Color.Black;
                    }



                    pen.Color = System.Drawing.Color.Blue;
                    graphicsOb.DrawRectangle(pen, rect);

                }
            }
        }
       //Opponent/Right pael mouseMove Event Listener
        private void pnlOponent_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mx = e.X;
            my = e.Y;


            pnlOponent.Refresh();
        }

        //Player/left panel mouseMove Event Listener
        private void pnlBoard_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mx = e.X;
            my = e.Y;


            pnlBoard.Refresh();
        }
    }
    

}
