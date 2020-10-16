using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCheckersCW
{
    public class Checkerboard
    {
        private string[,] checkerBoard; //2D String Array for the checkerboard.
        public const int DIMEN = 8; //Constant checkerboard dimension of 8x8.

        //Accessing the other class files
        private Piece piece;
        private MovePiece movepiece;
        private AIMovePiece aimovepiece;
        

        public Checkerboard() //Checkerboard constructer where all the checkerboard re-drawing is done.
        {
            //Accessing all the public members for the classes.
            piece = new Piece();
            movepiece = new MovePiece();
            aimovepiece = new AIMovePiece();

            
            checkerBoard = new string[DIMEN, DIMEN]; //Initializing the 2D array.
            CheckerBoardHoriz = "+---"; //Defining the horizontal graphical properties of the checkerboard.
            CheckerBoardVert = "| "; //Defining the vertical graphical properties of the checkerboard.
        }
        public string CheckerBoardHoriz { get; set; }
        public string CheckerBoardVert { get; set; }

        public void displayCheckerBoard()
        {
            while(!movepiece.Exit)
            {
                Console.Clear(); //Clears the checkerboard
                Console.WriteLine("   0   1   2   3   4   5   6   7"); //Draws the Text present on the left and top of the checkerboard.
                
                for (int r = 0; r < DIMEN; r++) //Begins a loop for the rows
                {
                    Console.Write("  "); //Inputs the spaces
                    for (int c = 0; c < DIMEN; c++) //Begins a loop for the columns
                    {
                        Console.Write(CheckerBoardHoriz); //Draws the horizontal graphical properties onto the board.
                    }
                    Console.Write("+\n"); //Inputs a new line

                    for (int c = 0; c < DIMEN; c++) //Begins a loop for the columns
                    {
                    if (c == 0) //if column = Zero
                        Console.Write(r + " "); //Draw the rows and input the spaces
                        Console.Write(CheckerBoardVert + Piece.pieces[r, c] + " "); //Draws the vertical graphical properties onto the board, along with the pieces and spaces.
                  
                    }
                    Console.Write("|\n"); //Writes the other side of the checkerboard wall.
                }

            
            Console.Write("  ");
            for (int c = 0; c < DIMEN; c++)
            {
                Console.Write(CheckerBoardHoriz);
            }

            Console.Write("+\n\n");
                movepiece.miniMenu(); //Displays the mini menu on the checkerboard

                movepiece.moveChoice(); //Displays the user selection to move pieces then moves the piece
               
                //ALL CODE BELOW - repeat of the ABOVE code but redraws the board again for the AI to make their move.
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
                aimovepiece.aiMove(); //Displays the AI piece selection, then moves the AI's piece.


            }
            
            
        }
    }
}
