using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCheckersCW
{
    public class Piece
    {
        //Defining the pieces for the board.
        public const char PIECE_SYM = 'x';
        public const char PIECE_SYM_X_KING = '=';
        public const char PIECE_SYM_W_KING = '#';
        public const char PIECE_SYM2 = 'o';
        public const char PIECE_SPACE = ' ';

        public static char[,] pieces; //Defining a piece array that will hold all the pieces.

        public Piece()
        {
            
            pieces = new char[Checkerboard.DIMEN, Checkerboard.DIMEN]; //New array with the dimensions of the checkboard.
            initPiece(); //Initialises the pieces onto the board.
            
        }

        private void initPiece()
        {
            for (int r = 0; r < Checkerboard.DIMEN; r++) //Starts the ROW loop
            {
                for (int c = 0; c < Checkerboard.DIMEN; c++) //starts the column loop.
                {
                    if (r == 0 || r == 2) //For Row 0 and 2 
                        pieces[r, c++] = PIECE_SYM; //Place a piece on the every second square.
                    if (r == 1 && c == 1 || r == 1 && c == 3 || r == 1 && c == 5 || r == 1 && c == 7) //For Rows 1-3-5-7
                        pieces[r, c] = PIECE_SYM;  //Place a piece on every other square.
                    else 
                        pieces[r, c] = PIECE_SPACE; //Place the spaces afterward.
                    if  (r == Checkerboard.DIMEN - 1 && c == 1 || r == Checkerboard.DIMEN - 1 && c == 3 || r == Checkerboard.DIMEN - 1 && c == 5 || r == Checkerboard.DIMEN - 1 && c == 7) 
                        pieces[r, c++] = PIECE_SYM2;   
                    if  (r == Checkerboard.DIMEN - 2 && c == 0 || r == Checkerboard.DIMEN - 2 && c == 2 || r == Checkerboard.DIMEN - 2 && c == 4 || r == Checkerboard.DIMEN - 2 && c == 6)
                        pieces[r, c] = PIECE_SYM2;
                    if (r == Checkerboard.DIMEN - 3 && c == 1 || r == Checkerboard.DIMEN - 3 && c == 3 || r == Checkerboard.DIMEN - 3 && c == 5 || r == Checkerboard.DIMEN - 3 && c == 7)
                        pieces[r, c++] = PIECE_SYM2;
                  
                }
            }
        }
    }
}
