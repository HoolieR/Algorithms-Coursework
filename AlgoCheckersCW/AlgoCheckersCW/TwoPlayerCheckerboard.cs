using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCheckersCW
{
    class TwoPlayerCheckerboard
    {
        private string[,] checkerBoard;
        public const int DIMEN = 8;

        private Piece piece;
        private MovePiece movepiece;
        //private AIMovePiece aimovepiece;


        public TwoPlayerCheckerboard()
        {
            piece = new Piece();
            movepiece = new MovePiece();
            //aimovepiece = new AIMovePiece();

            checkerBoard = new string[DIMEN, DIMEN];
            CheckerBoardHoriz = "+---";
            CheckerBoardVert = "| ";
        }
        public string CheckerBoardHoriz { get; set; }
        public string CheckerBoardVert { get; set; }

        public void displayCheckerBoard()
        {
            while (!movepiece.Exit)
            {
                Console.Clear();
                Console.WriteLine("   0   1   2   3   4   5   6   7");

                for (int r = 0; r < DIMEN; r++)
                {
                    Console.Write("  ");
                    for (int c = 0; c < DIMEN; c++)
                    {
                        Console.Write(CheckerBoardHoriz);
                    }
                    Console.Write("+\n");

                    for (int c = 0; c < DIMEN; c++)
                    {
                        if (c == 0)
                            Console.Write(r + " ");
                        Console.Write(CheckerBoardVert + Piece.pieces[r, c] + " ");

                    }
                    Console.Write("|\n");
                }

                Console.Write("  ");
                for (int c = 0; c < DIMEN; c++)
                {
                    Console.Write(CheckerBoardHoriz);
                }

                Console.Write("+\n\n");
                movepiece.miniMenu();
                movepiece.moveChoice();
            }
        }


     }
}
