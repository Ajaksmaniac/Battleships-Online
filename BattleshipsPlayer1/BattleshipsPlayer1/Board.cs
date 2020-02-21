﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsPlayer1
{
    class Board
    {
        public int WIDTH = 7;
        public int HEIGHT = 7;
        public int[,] board;
        public Board()
        {
            Random r = new Random();
            int n = r.Next(1, 5);
            /* switch (n)
             {

                 //Harcoded boards, until manual board construction is implemented
                 case 1:
                     board = board1;
                     break;
                 case 2:
                     board = board2;
                     break;
                 case 3:
                     board = board3;
                     break;
                 case 4:
                     board = board4;
                     break;
                 default:
                     board = board5;
                     break;
             }
             */

            this.board = new int[7,7] {
                                       { 1,0,0,1,1,0,0},
                                       { 1,0,0,0,0,0,0},
                                       { 1,0,1,1,1,1,0},
                                       { 1,0,0,0,0,0,0},
                                       { 1,0,0,0,1,0,0},
                                       { 0,0,0,0,1,0,0},
                                       { 1,1,0,0,1,0,0}
                                    
                                     


        };

        }

        int[,] board1 = new int[,] { { 1,0,0,0,0,0,0,1,1,0 },
                                       { 1,0,0,0,0,0,0,0,0,0 },
                                       { 1,0,1,1,1,1,0,0,0,0 },
                                       { 1,0,0,0,0,0,0,0,1,0 },
                                       { 1,0,0,0,1,0,0,0,1,0 },
                                       { 0,0,0,0,1,0,0,0,0,0 },
                                       { 0,0,0,0,1,0,0,0,0,0 },


        };
        int[,] board2 = new int[,] {   { 0,0,1,1,1,1,1,0,0,0 },
                                       { 0,0,0,0,0,0,0,0,0,0 },
                                       { 0,0,1,1,1,1,0,0,0,0 },
                                       { 0,0,0,0,0,0,0,0,0,0 },
                                       { 0,0,1,1,1,0,0,0,0,0 },
                                       { 0,1,0,0,0,0,0,1,0,0 },
                                       { 0,1,0,0,0,0,0,1,0,0 },


        };
        int[,] board3 = new int[,] {   { 0,0,0,1,0,0,0,1,1,0 },
                                       { 0,0,0,1,0,0,0,0,0,0 },
                                       { 0,0,0,1,0,0,0,0,0,0 },
                                       { 0,0,0,1,0,0,0,0,1,0 },
                                       { 0,0,0,1,0,0,0,0,1,0 },
                                       { 0,0,0,0,0,1,1,1,1,0 },
                                       { 0,0,1,1,1,0,0,0,0,0 },


        };
        int[,] board4 = new int[,] {   { 1,0,0,0,0,0,0,0,0,0 },
                                       { 1,0,0,0,0,0,0,0,0,0 },
                                       { 1,0,0,0,1,0,0,0,0,0 },
                                       { 1,0,0,0,1,0,0,0,1,0 },
                                       { 1,0,0,0,0,0,0,0,1,0 },
                                       { 0,0,1,1,1,0,0,0,0,0 },
                                       { 0,0,0,0,0,0,1,1,1,1 },


        };
        int[,] board5 = new int[,] {   { 0,0,0,0,0,0,0,0,0,0 },
                                       { 1,0,1,1,1,1,0,0,0,0 },
                                       { 1,0,0,0,0,0,0,0,0,0 },
                                       { 1,0,0,0,1,0,0,0,0,0 },
                                       { 1,0,0,0,1,0,1,0,0,0 },
                                       { 1,0,0,0,1,0,1,0,0,0 },
                                       { 0,1,1,0,0,0,0,0,0,0 },


        };


    }
}
