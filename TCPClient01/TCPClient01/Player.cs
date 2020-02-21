using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient01
{


    class Player
    {
        string name;
        Board playerBoard;

        public Player(string name, Board board)
        {
            this.name = name;
            this.playerBoard = board;
        }

        public void setName(string name)
        {
            this.name = name;

        }
        public string getName()
        {
            return this.name;
        }

        public void setBoard(int[,] board)
        {
            this.playerBoard.board = board;
        }
        public int[,] getBoard()
        {
            return this.playerBoard.board;
        }

         public bool Attack(int x, int y, Player p)
        {
            if (p.sunkShip(x,y))
            {
                return true;   
            }
            return false;
        }

        public bool sunkShip(int x, int y)
        {
            {
                if (playerBoard.board[x,y] == 1)
                {
                    //2 means that ship part is destroyed
                    playerBoard.board[x,y] = 2;
                    return true;
                }
                    return false;
            }

        }

       

    }
}
