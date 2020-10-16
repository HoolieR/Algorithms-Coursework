using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoCheckersCW
{
    class AIMovePiece : Piece
    {
        //Defining the AI's piece co-ordinates.
        private int AIplaceX;
        private int AIplaceY;
        private int AIdestinX;
        private int AIdestinY;

        //Sets the "First Move" counter to zero - so the first turn always happens.
        int firstMove = 0;

        public AIMovePiece() 
        {
            //Resets the AI's co-ordinates.
            AIplaceX = 0;
            AIplaceY = 0;
            AIdestinX = 0;
            AIdestinY = 0;
            Exit = false;
        }
        public bool Exit { get; set; }


        public void aiMove()
        {

            aiChoice(); //The AI chooses the co-ordinates.
            firstMove++;  //The first move counter is incremented.
            FileStream filestream = new FileStream("AIMoves.txt", FileMode.Create, FileAccess.Write); //Writes the moves to the AImoves.txt file.
            var streamwrite = new StreamWriter(filestream);
            //streamwrite.AutoFlush = true;
            filestream.Seek(0, SeekOrigin.End);
            streamwrite.WriteLine(AIplaceX);
            streamwrite.WriteLine(AIplaceY);
            streamwrite.WriteLine(AIdestinX);
            streamwrite.WriteLine(AIdestinY);
            streamwrite.Close();
            if (!Exit) //If the AI doesn't trip the error handling then the piece is moved.
            aiAlterPiece();
        }

        public void aiChoice() //The Artificial Intelligences algorithm - based on the Fisher Yates shuffle.
        {

            Random r = new Random(); //Starts a random number generator.
            if (firstMove == 0) //When the first move happens - the AI will ALWAYS pick a piece that exists on (Y = 5) - as it can't move a piece from any other location.
            {

                int moveOption3;
                List<int> yValues2 = new List<int>(new int[] { 5 });
                IEnumerable<int> threeRandom2 = yValues2.OrderBy(x => r.Next()).Take(1);
                AIplaceY = threeRandom2.First();
                if (AIplaceY == 5) //First the AI always Picks 5 for the Y-Axis.
                {
                    List<int> xValues = new List<int>(new int[] { 1, 3, 5, 7 }); //Then the AI will pick between 4 other co-ordinates, this is where valid pieces exist.

                    IEnumerable<int> xRandom = xValues.OrderBy(x => r.Next()).Take(1);
                    AIplaceX = xRandom.First();
                }
                if (AIplaceX == 1 && AIplaceY == 5) //If the AI picks (1,5) THEN
                {
                    List<int> moveChoice = new List<int>(new int[] { -1, 1 }); //The AI will pick two values to decide which direction to go
                    IEnumerable<int> xRandom = moveChoice.OrderBy(x => r.Next()).Take(1);
                    moveOption3 = xRandom.First();
                    if (moveOption3 == -1)  
                    {
                        AIdestinX = AIplaceX + 1;
                        AIdestinY = AIplaceY - 1;

                    }
                    if (moveOption3 == 1)
                    {
                        AIdestinX = AIplaceX - 1;
                        AIdestinY = AIplaceY - 1;
                    }
                }
                else if (AIplaceX == 7 && AIplaceY == 5) //If the AI chooses (7,5) then
                {
                    AIdestinX = AIplaceX - 1; //It can only move left - as moving right will exist the board.
                    AIdestinY = AIplaceY - 1;
                }
                else if (AIplaceX == 5 && AIplaceY == 5 || AIplaceX == 3 && AIplaceY == 5) //If the AI picks (5,5) or (3,5) THEN
                {
                    List<int> moveChoice = new List<int>(new int[] { -1, 1 }); //Decide which direction to go
                    IEnumerable<int> xRandom = moveChoice.OrderBy(x => r.Next()).Take(1);
                    moveOption3 = xRandom.First();
                    if (moveOption3 == -1)
                    {
                        AIdestinX = AIplaceX - 1; //Left Direction
                        AIdestinY = AIplaceY - 1;

                    }
                    if (moveOption3 == 1)
                    {
                        AIdestinX = AIplaceX + 1; //Right Direction
                        AIdestinY = AIplaceY - 1;
                    }
                }
                firstMove++;

            }
            else if (firstMove > 0) //Once the first move has been played - the FirstMove++ counter is incremented. 
            {
                int redoMoveInt; //The AI can choose to move a piece it's already moved.
                List<int> redoMove = new List<int>(new int[] { 1, 2, 3 }); //The AI puts three choices "into a hat"
                IEnumerable<int> redoMoveRNG = redoMove.OrderBy(x => r.Next()).Take(1);
                redoMoveInt = redoMoveRNG.First();
                if (redoMoveInt == 1) //If the AI draws a "1" - it will move the previous piece to the left.
                {
                    StreamReader AIMovesStream = new StreamReader("AIMoves.txt"); //The AI reads the last move
                    string aiXDestin = File.ReadLines("AIMoves.txt").ElementAt(2);
                    string aiYDestin = File.ReadLines("AIMoves.txt").ElementAt(3);
                    AIMovesStream.Close();

                    AIplaceX = Int32.Parse(aiXDestin);
                    AIplaceY = Int32.Parse(aiYDestin);

                    AIdestinX = AIplaceX - 1;
                    AIdestinY = AIplaceY - 1;
                }
                else if (redoMoveInt == 2) //If the AI draws a "2" - it will move the previous piece to the right.
                {
                    StreamReader AIMovesStream = new StreamReader("AIMoves.txt"); //The AI reads the last move
                    string aiXDestin = File.ReadLines("AIMoves.txt").ElementAt(2);
                    string aiYDestin = File.ReadLines("AIMoves.txt").ElementAt(3);
                    AIMovesStream.Close();

                    AIplaceX = Int32.Parse(aiXDestin);
                    AIplaceY = Int32.Parse(aiYDestin);
                    AIdestinX = AIplaceX + 1;
                    AIdestinY = AIplaceY - 1;

                }
                else if (redoMoveInt == 3) //If the AI draws a "3" - it will move a new piece.
                {
                    int moveOption;
                    List<int> yValues = new List<int>(new int[] { 5, 6, 7 }); //First the AI picks the Y-Axis.

                    IEnumerable<int> threeRandom = yValues.OrderBy(x => r.Next()).Take(1);
                    AIplaceY = threeRandom.First();

                    if (AIplaceY == 5) //If the AI picks Y = 5 THEN
                    {
                        List<int> xValues = new List<int>(new int[] { 1, 3, 5, 7 }); //Then the AI picks the X-Cord, where pieces exist.

                        IEnumerable<int> xRandom = xValues.OrderBy(x => r.Next()).Take(1);
                        AIplaceX = xRandom.First();
                    }
                    if (AIplaceY == 6) //If the AI picks Y = 6 THEN
                    {
                        List<int> xValues = new List<int>(new int[] { 0, 2, 4, 6 }); //Then the AI picks the X-Cord, where pieces exist.
                        IEnumerable<int> xRandom = xValues.OrderBy(x => r.Next()).Take(1);
                        AIplaceX = xRandom.First();
                    }
                    if (AIplaceY == 7) //If the AI picks Y = 7 THEN
                    {
                        List<int> xValues = new List<int>(new int[] { 1, 3, 5, 7 }); //Then the AI picks the X-Cord, where pieces exist.
                        IEnumerable<int> xRandom = xValues.OrderBy(x => r.Next()).Take(1);
                        AIplaceX = xRandom.First();
                    }

                    if (AIplaceX == 1 && AIplaceY == 7)
                    {
                        AIdestinX = AIplaceX + 3;
                        AIdestinY = AIplaceY - 3;
                    }
                    else if (AIplaceX == 3 && AIplaceY == 7)
                    {
                        List<int> moveChoice = new List<int>(new int[] { -1, 1 });
                        IEnumerable<int> xRandom = moveChoice.OrderBy(x => r.Next()).Take(1);
                        moveOption = xRandom.First();
                        if (moveOption == -1)
                        {
                            AIdestinX = AIplaceX + 3;
                            AIdestinY = AIplaceY + 3;

                        }
                        if (moveOption == 1)
                        {
                            AIdestinX = AIplaceX - 3;
                            AIdestinY = AIplaceY - 3;
                        }
                    }
                    else if (AIplaceX == 5 && AIplaceY == 7)
                    {
                        AIdestinX = AIplaceX - 3;
                        AIdestinY = AIplaceY - 3;
                    }
                    else if (AIplaceX == 7 && AIplaceY == 7)
                    {
                        AIdestinX = AIplaceX - 3;
                        AIdestinY = AIplaceY - 3;
                    }

                    if (AIplaceX == 4 && AIplaceY == 6)
                    {
                        List<int> moveChoice = new List<int>(new int[] { -1, 1 });
                        IEnumerable<int> xRandom = moveChoice.OrderBy(x => r.Next()).Take(1);
                        moveOption = xRandom.First();
                        if (moveOption == -1)
                        {
                            AIdestinX = AIplaceX + 2;
                            AIdestinY = AIplaceY + 2;

                        }
                        if (moveOption == 1)
                        {
                            AIdestinX = AIplaceX - 2;
                            AIdestinY = AIplaceY - 2;
                        }
                    }
                    else if (AIplaceX == 2 && AIplaceY == 6)
                    {
                        List<int> moveChoice = new List<int>(new int[] { -1, 1 });
                        IEnumerable<int> xRandom = moveChoice.OrderBy(x => r.Next()).Take(1);
                        moveOption = xRandom.First();
                        if (moveOption == -1)
                        {
                            AIdestinX = AIplaceX + 2;
                            AIdestinY = AIplaceY - 2;

                        }
                        if (moveOption == 1)
                        {
                            AIdestinX = AIplaceX - 2;
                            AIdestinY = AIplaceY - 2;
                        }
                    }
                    else if (AIplaceX == 6 && AIplaceY == 6)
                    {
                        AIdestinX = AIplaceX - 2;
                        AIdestinY = AIplaceY - 2;
                    }
                    else if (AIplaceX == 0 && AIplaceY == 6)
                    {
                        AIdestinX = AIplaceX + 2;
                        AIdestinY = AIplaceY - 2;
                    }

                    if (AIplaceX == 1 && AIplaceY == 5) //If the AI picks (1,5) THEN
                    {
                        List<int> moveChoice = new List<int>(new int[] { -1, 1 }); //The AI will pick two values to decide which direction to go
                        IEnumerable<int> xRandom = moveChoice.OrderBy(x => r.Next()).Take(1);
                        moveOption = xRandom.First();
                        if (moveOption == -1)
                        {
                            AIdestinX = AIplaceX + 1;
                            AIdestinY = AIplaceY - 1;

                        }
                        if (moveOption == 1)
                        {
                            AIdestinX = AIplaceX - 1;
                            AIdestinY = AIplaceY - 1;
                        }
                    }
                    else if (AIplaceX == 7 && AIplaceY == 5) //If the AI chooses (7,5) then
                    {
                        AIdestinX = AIplaceX - 1; //It can only move left - as moving right will exist the board.
                        AIdestinY = AIplaceY - 1;
                    }
                    else if (AIplaceX == 5 && AIplaceY == 5 || AIplaceX == 3 && AIplaceY == 5) //If the AI picks (5,5) or (3,5) THEN
                    {
                        List<int> moveChoice = new List<int>(new int[] { -1, 1 }); //Decide which direction to go
                        IEnumerable<int> xRandom = moveChoice.OrderBy(x => r.Next()).Take(1);
                        moveOption = xRandom.First();
                        if (moveOption == -1)
                        {
                            AIdestinX = AIplaceX - 1; //Left Direction
                            AIdestinY = AIplaceY - 1;

                        }
                        if (moveOption == 1)
                        {
                            AIdestinX = AIplaceX + 1; //Right Direction
                            AIdestinY = AIplaceY - 1;
                        }
                    }

                }

            }

            
            //The console will then output the AI's moves
            Console.WriteLine("AI picked the X-Cord " + AIplaceX);
            Exit = validateUserInput(int.TryParse(AIplaceX.ToString(), out AIplaceX)); //The AI will then have its moves checked and validated.
            Console.ReadLine();
            
            if (!Exit)
            {
                Console.WriteLine("AI picked the Y-Cord " + AIplaceY);
                Exit = validateUserInput(int.TryParse(AIplaceY.ToString(), out AIplaceY));
                Console.ReadLine();
           
            }
            if (!Exit)
            {
                Console.WriteLine("AI picked the destination x-cord " + AIdestinX);
                Exit = validateUserInput(int.TryParse(AIdestinX.ToString(), out AIdestinX));
                Console.ReadLine();
   
            }
            if (!Exit)
            {
                Console.WriteLine("AI picked the destination y-cord " + AIdestinY);
                Exit = validateUserInput(int.TryParse(AIdestinY.ToString(), out AIdestinY));
                Console.ReadLine();
           
            }
        }

        private bool validateUserInput(bool parsed) //The AI runs through the same co-ordinate validation as the user in MovePiece().
        {
            bool error = false;
            if (!parsed)
                error = true;
            else if (AIplaceX < 0 || AIplaceX < 0 || AIdestinX < 0 || AIdestinY < 0)
                error = true;
            else if (AIplaceX > Checkerboard.DIMEN + 1 || AIplaceX > Checkerboard.DIMEN + 1 || AIdestinX > Checkerboard.DIMEN + 1 || AIdestinY > Checkerboard.DIMEN + 1)
                error = true;
            if (error)
                Console.WriteLine("Invalid Input Detected. Exiting");

            return error;
        }


        private void aiAlterPiece()
        {

            pieces[AIdestinY, AIdestinX] = pieces[AIplaceY, AIplaceX]; //The co-ordinates are flipped for the AI, as they play from the bottom of the array upwards.
            if (AIdestinX > 7 || AIdestinY < 0)
            {
                Console.WriteLine("The AI attempted to move out of bounds. Exiting.");
                Console.ReadLine();
                Environment.Exit(0);

            }
            if (AIdestinX == 0) //If The AI's piece lands on any grid with y = 0 - then that piece becomes a King.
            {
                pieces[AIdestinX, AIdestinY] = PIECE_SYM_W_KING;
            }
            pieces[AIplaceY, AIplaceX] = PIECE_SPACE; //replaces the original AI piece with a space.

        }

    }
}
