using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlgoCheckersCW
{
    public class MovePiece : Piece //Passing the Piece class file into MovePiece so it can use it.
    {
        private int placeX; //Declaring the Player Co-Ordinate variables.
        private int placeY;
        private int destinX;
        private int destinY;



        public MovePiece() //At the start of the players turn, all the co-ordinates are reset.
        {
            placeX = 0; 
            placeY = 0;
            destinX = 0;
            destinY = 0;
            Exit = false;
        }


        public bool Exit { get; set; } //Setting up the "Exit" bool variable, true or false.

        public void miniMenu() //This is the mini-menu that displays before the players turn. This allows them to choose to play, see previous moves, or exit.
        {
            getPlayInput(); //Runs the getPlayInput code - which includes accessing previous moves, playing or exiting the game.
            //if (!Exit) //If the player's co-ordinates do not trigger the error detection, then the move is un-done.
            //    alterPiece();
            //Console.ReadLine();
            countScore();
        }

        public void moveChoice() //This is the code that plays out the Players Turn.
        {
            //countScore();
            getUserInput(); //Goes to the getUserInput() code - which has the player select their co-ordinates.
            FileStream filestream = new FileStream("PlayerMoves.txt", FileMode.Create, FileAccess.Write); //The player's selected co-ordinates are then stored locally - so they can be accessed again for showing or un-doing the move.
            
            var streamwrite = new StreamWriter(filestream);
            //streamwrite.AutoFlush = true;
            filestream.Seek(0, SeekOrigin.End);
            streamwrite.WriteLine(placeX);
            streamwrite.WriteLine(placeY);
            streamwrite.WriteLine(destinX);
            streamwrite.WriteLine(destinY);
            streamwrite.Close();

            if (destinX == placeX + 1 && destinY == placeY + 1 || destinX == placeX + 1 && destinY == placeY - 1 || destinX == placeX - 1 && destinY == placeY + 1 || destinX == placeX - 1 && destinY == placeY - 1)
            { //This code restricts the players movement to moving diagonally. 
                if (!Exit)
                    alterPiece();
            }
            else //if the player tries to move up, down, left or right - then this code triggers and the players turn is skipped.
            {
                Console.WriteLine("You can't move a piece there!");
                Console.ReadLine();
            }



        }


        public void getPlayInput() //This handles the player choosing the "Moves" option on the "Mini-Menu".
        {
            StreamReader PlayerMovesStream = new StreamReader("PlayerMoves.txt"); //Reads from the PlayerMoves text file.
            StreamReader AIMovesStream = new StreamReader("AIMoves.txt"); //Reads from the AIMoves Text file.
            string PlayerXOrigin = File.ReadLines("PlayerMoves.txt").ElementAt(0); //stores the player moves into string variables.
            string PlayerYOrigin = File.ReadLines("PlayerMoves.txt").ElementAt(1);
            string PlayerXDestin = File.ReadLines("PlayerMoves.txt").ElementAt(2);
            string PlayerYDestin = File.ReadLines("PlayerMoves.txt").ElementAt(3);

            string aiXOrigin = File.ReadLines("AIMoves.txt").ElementAt(0); //stores the ai moves into string variables.
            string aiYOrigin = File.ReadLines("AIMoves.txt").ElementAt(1);
            string aiXDestin = File.ReadLines("AIMoves.txt").ElementAt(2);
            string aiYDestin = File.ReadLines("AIMoves.txt").ElementAt(3);

            PlayerMovesStream.Close(); //closes the reading streams to prevent unhandled exceptions.
            AIMovesStream.Close();

            Console.WriteLine("Type 'Exit' to Exit the game"); //Console prints the user's choices.
            Console.WriteLine("Type 'Moves' to list the last used moves.");
            Console.WriteLine("Type 'Undo' to Undo your previous move");

            string stringInput = Console.ReadLine();
            if (stringInput.Equals("Exit"))
            {
                System.Environment.Exit(0);
            }
           else if (stringInput.Equals("Moves"))
            {
                Console.WriteLine("The Last used moves: ");
                Console.WriteLine("The player picked the piece at " + "(" + PlayerXOrigin + "," + PlayerYOrigin + ")");
                Console.WriteLine("The player moved the piece to " + "(" + PlayerXDestin + "," + PlayerYDestin + ")");
                

                Console.WriteLine("The AI picked the piece at " + "(" + aiXOrigin + "," + aiYOrigin + ")");
                Console.WriteLine("The AI moved the piece to " + "(" + aiXDestin + "," + aiYDestin + ")");
                
            }
           else if (stringInput.Equals("Undo"))
            {
                Console.WriteLine("Undoing previous move");
                //placeX = Int32.Parse(PlayerXDestin);
               // placeY = Int32.Parse(PlayerYDestin);
               // destinX = Int32.Parse(PlayerXOrigin);
               // destinY = Int32.Parse(PlayerYOrigin);

            }
            
        }

        public void countScore() //Counts the number of pieces on the board.
        {
          
            int Xtotal = 0; //Sets the incremental counter to zero.
            foreach (char PIECE_SYM in pieces) //For each X piece in the pieces array
            {
                if (PIECE_SYM == 'x') //If there is a piece labelled 'x' then
                    Xtotal++;   //Increment the counter
            }
            int Ytotal = 0;
            foreach (char PIECE_SYM2 in pieces)
            {
                if (PIECE_SYM2 == 'o')
                    Ytotal++;
            }
            Console.WriteLine("There are " + Xtotal + " X pieces");
            Console.WriteLine("There are " + Ytotal + " O pieces");

            if (Ytotal == 0) //If the total number of pieces for Player 2 reaches zero
            {
                Console.WriteLine("Player 1 Wins!");
                
            }
            if (Xtotal == 0) //If the total number of pieces for PLayer 1 reaches zero
            {
                Console.WriteLine("Player 2 Wins!");
            }
        }


        public void getUserInput()
        {
            Console.WriteLine("Enter Piece's X Co-Ordinates");
            Exit = validateUserInput(int.TryParse(Console.ReadLine(), out placeX)); //Validates the users input by parsing whatever the user inputs into the command line

            if (!Exit) //If the first check completes succesfully then the next co-ordinates are queried.
            {
                Console.WriteLine("Enter Piece's Y Co-Ordinates");
                Exit = validateUserInput(int.TryParse(Console.ReadLine(), out placeY));
            }
            if (!Exit)
            {
                Console.WriteLine("Enter Piece's Destinations (X axis)");
                Exit = validateUserInput(int.TryParse(Console.ReadLine(), out destinX));
            }
            if (!Exit)
            {
                Console.WriteLine("Enter Piece's Destinations (Y axis)");
                Exit = validateUserInput(int.TryParse(Console.ReadLine(), out destinY));
            }
            
        }


        private bool validateUserInput(bool parsed) //Validates the users input when quering for a piece move.
        {
            bool error = false; //By default the error is false - UNLESS
            if (!parsed) //If the boolean fails to parse
                error = true; //The error is true
            else if (placeX < 0 || placeX < 0 || destinX < 0 || destinY < 0) //If the user inputs a number outwith the board dimensions
                error = true; //The error is true
            else if (placeX > Checkerboard.DIMEN - 1 || placeX > Checkerboard.DIMEN - 1 || destinX > Checkerboard.DIMEN - 1 || destinY > Checkerboard.DIMEN - 1) //If the user inputs a number outwith the board dimensions
                error = true; //The Error is true
            if (error) //If the error is true
                Console.WriteLine("Invalid Input Detected. Exiting"); //Display an error message

            return error; //Return the error and exit.
        }

       

        private void alterPiece()
        {
            //int pointScore = 12;
            pieces[destinX, destinY] = pieces[placeX, placeY]; //The piece destination in the 2D array equals the pieces original place in the array.
            pieces[placeX, placeY] = PIECE_SPACE; //Then the original piece place becomes a space.

            if (destinX == 7 || pieces[placeX, placeY] == PIECE_SYM_X_KING) //If Player 1's piece lands on any grid with y = 7 - then that piece becomes a King.
            {
                pieces[destinX, destinY] = PIECE_SYM_X_KING;
            }

            if (destinX == 0 || pieces[placeX, placeY] == PIECE_SYM2) //If Player 2's piece lands on any grid with y = 0 - then that piece becomes a King.
            {
                pieces[destinX, destinY] = PIECE_SYM_W_KING;
            }
            
        }
    }
}
