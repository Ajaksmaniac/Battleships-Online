using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPClient01
{
    public partial class Form1 : Form

    {
        
        TcpClient mTCPClient;
        byte[] mRx;
        int mx = -100;
        int my = -100;
        Player player;
        Player oponent;
        public Form1()
        {
            InitializeComponent();
            
        }
        int tempNumOfShipLocations = 0;
        int[,] tempOpponentBoard = new int[,] {
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0 }
        };
        void setOponentShipPart(int x, int y)
        {
            tempOpponentBoard[x, y] = 1;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            IPAddress ipa;
            int nPort;
            try
            {
                if (String.IsNullOrEmpty(tbUsername.Text) || String.IsNullOrWhiteSpace(tbUsername.Text))
                {
                    MessageBox.Show("Supply Username Before  looking for a Opponent");
                }
                //Checks if IP  or  Port are not null
                if (String.IsNullOrEmpty(tbServerIP.Text) ) return;
                if(!IPAddress.TryParse(tbServerIP.Text, out ipa))
                {
                    MessageBox.Show("Supply server IP address");
                    return;
                }
              
                    nPort = 23000;
                player = new Player(tbUsername.Text.ToString(), new Board());
                
                

                mTCPClient = new TcpClient();
                mTCPClient.BeginConnect(ipa, nPort, onCompleteConnect, mTCPClient);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        

        }

        void onCompleteConnect(IAsyncResult iar)
        {

            TcpClient tcpc;
            try
            {

                tcpc = (TcpClient)iar.AsyncState;
                tcpc.EndConnect(iar);
                mRx = new byte[512];
                tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);
                sendName();
                sendBoard();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void onCompleteReadFromServerStream(IAsyncResult iar)
        {


            TcpClient tcpc;
            int nBytesRecievedFromServer;
            string strRecieved;
            try
            {

                tcpc = (TcpClient)iar.AsyncState;
                nBytesRecievedFromServer = tcpc.GetStream().EndRead(iar);
                if(nBytesRecievedFromServer == 0)
                {
                   printLine("Connection Lost");
                    return;
                }

                strRecieved = Encoding.ASCII.GetString(mRx, 0 , nBytesRecievedFromServer);

                string[] recievedPacket = strRecieved.Split(',');
                
                

                // char[] tempCharArray = strRecieved.ToCharArray();

                int code = int.Parse(recievedPacket[0]);
                if(code == 1)
                {
                    string name = recievedPacket[1];
                    printLine(name+" Connected");
                    oponent = new Player(name, new Board());

                    
                    
                    
                }
                if (code == 2)
                {
                    string[] cords = recievedPacket[1].Split(':');
                    int x = int.Parse(cords[0]);
                    int y = int.Parse(cords[1]);
                    printLine(x + " " + y);
                    setOponentShipPart(x, y);
                    this.tempNumOfShipLocations++;

                }


                mRx = new byte[512];
                tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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
                    mTCPClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToServer, mTCPClient);
                }
            }
        }
        public void sendBoard()
        {
            int[,] board = player.getBoard();
          
            //MessageBox.Show(packet);


            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (mTCPClient != null)
                    {
                        if (mTCPClient.Client.Connected)
                        {
                            if(board[i,j] == 1)
                            {
                                string packet = "2," + i + ":" + j;
                                byte[] tx;
                                //MessageBox.Show(packet);
                                tx = Encoding.ASCII.GetBytes(packet);
                                printLine("Code 1: Ship part Sent to :" + mTCPClient.ToString());
                                mTCPClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToServer, mTCPClient);
                            }
                            
                        }
                    }
                }

            }
           
        }
        public void printLine(string _strPrint)
        {
            tbConsole.Invoke(new Action<string>(doInvoke), _strPrint);

        }
        public void doInvoke(string _strPrint)
        {
            tbConsole.Text = _strPrint + Environment.NewLine + tbConsole.Text;
        }
        private void btSend_Click(object sender, EventArgs e)
        {
            byte[] tx;
            if (String.IsNullOrEmpty(tbPayload.Text))
            {
                return;
            }
            try
            {

                tx = Encoding.ASCII.GetBytes(tbPayload.Text);
                if(mTCPClient != null)
                {
                    if (mTCPClient.Client.Connected)
                    {
                        mTCPClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToServer, mTCPClient);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        void onCompleteWriteToServer(IAsyncResult iar)
        {
            TcpClient tcpc;
            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                tcpc.GetStream().EndWrite(iar);
            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
        }

        private void lbOpponentName_Click(object sender, EventArgs e)
        {

        }

        //opponent board
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (mTCPClient == null)
            {
                return;

            }
            if(this.tempNumOfShipLocations != 16)
            {
                return;
            }
            return;
            int spacing = 5;
            
            System.Drawing.Graphics graphicsOb;
            graphicsOb = panel1.CreateGraphics();
            Pen pen = new Pen(System.Drawing.Color.Blue);

            for (int i = 0; i < 10 ; i++)
            {
                for (int j = 0; j < 7 ; j++)
                {
                    Rectangle rect = new Rectangle(spacing + i * 30, spacing + j * 30 , 30 - 2 * spacing, 30 - 2 * spacing);

                    if (mx >= spacing + i * 30 && mx < spacing + i * 30 + 30 - 2 * spacing && my >= spacing + j * 30  && my < spacing + j * 30 + 15 + spacing)
                    {
                        pen.Color = System.Drawing.Color.Red;
                    }


                   /* if (oponent.getBoard()[i, j] == 1)
                    {
                        pen.Color = System.Drawing.Color.Brown;
                    }

                    if (oponent.getBoard()[i, j] == 2)
                    {
                        pen.Color = System.Drawing.Color.Black;
                    }*/




                    pen.Color = System.Drawing.Color.Blue;
                    

                  
                    graphicsOb.DrawRectangle(pen, rect);
                    
                }

            }
         
            
        }
        //Player board
        private void pnlBoard_Paint(object sender, PaintEventArgs e)

        {
            if (mTCPClient == null)
                {
                return;
                    
                }
            
            

            int spacing = 5;

            System.Drawing.Graphics graphicsOb;
            graphicsOb = pnlBoard.CreateGraphics();
            Pen pen = new Pen(System.Drawing.Color.Blue);

            for (int i = 0; i < 10 ; i++)
            {
                for (int j = 0; j < 7 ; j++)
                {
                   
                    Rectangle rect = new Rectangle(spacing + i * 30, spacing + j * 30, 30 - 2 * spacing, 30 - 2 * spacing);

                  
                    if(player.getBoard()[i,j] == 1 )
                    {
                        pen.Color = System.Drawing.Color.Brown;
                    }

                    if (player.getBoard()[i, j] == 2)
                    {
                        pen.Color = System.Drawing.Color.Black;
                    }



                    pen.Color = System.Drawing.Color.Blue;
                    

                    graphicsOb.DrawRectangle(pen, rect);
                    

                }
            }
        }


       

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mx = e.X;
            my = e.Y;


            panel1.Refresh();
        }

   




        /*protected override void OnMouseMove(MouseEventArgs e)
{
   base.OnMouseMove(e);

   mouseX = e.X;
   mouseY = e.Y;
   printLine((string)mouseX.ToString());


}*/
    }
}
