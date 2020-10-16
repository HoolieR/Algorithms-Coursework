using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using Console = Colorful.Console;
using System.Drawing;


namespace AlgoCheckersCW
{
    class Program
    {
        static void Main(string[] args)
        {
            int DA = 244;
            int V = 212;
            int ID = 255;
            //Console.WriteAscii("CHECKERS", Color.FromArgb(DA, V, ID)); //Writing the LOGO out.
            Console.WriteLine("CHECKERS", Color.Red);
            Console.WriteLine("By Ross Houliston - 40282609", Color.Red);
            
            Console.CursorVisible = false; //Disables the flicking cursor on the command line.
            

            drawMenu(new string[] { //Creates the main menu strings.
                "Player Vs. AI",
                "Player Vs. Player",
                "Exit",
            });
        }
        public static int drawMenu(string[] menuItems)
        {
            int DA = 244;
            int V = 212;
            int ID = 255;
            int selectedItem = 0; //By default the initial selected item (cursor) is set to zero.
            
            while (true)
            {
                for (int i = 0; i < menuItems.Length; i++) 
                {
                    if (selectedItem == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray; //Sets the Cursor colour to Gray.
                        Console.ForegroundColor = ConsoleColor.Black; //Sets the background to the cursor to black.
                        Console.WriteLine(menuItems[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuItems[i]);
                    }
                }

                ConsoleKeyInfo consolekeyInfo = Console.ReadKey();

                switch (consolekeyInfo.Key) //Begins a Case Statement to handle the main menu selection.
                {
                    case ConsoleKey.UpArrow: //Pressing the Up arrow on the keyboard moves the selection up.
                        if ((selectedItem - 1) < 0)
                        {
                            selectedItem = menuItems.Length - 1;
                        }
                        else
                        {
                            selectedItem--;
                        }
                        break;
                    case ConsoleKey.DownArrow: //Pressing the down arrow on the keyboard moves the selectio down.
                        if ((selectedItem + 1) > menuItems.Length - 1)
                        {
                            selectedItem = 0;
                        }
                        else
                        {
                            selectedItem++;
                        }
                        break;
                    case ConsoleKey.Enter: //When pressing enter:

                        if (menuItems[selectedItem] == "Exit") //If the user choose "Exit"
                        {
                            Environment.Exit(0); //The program is closed.
                        }
                        else if (menuItems[selectedItem] == "Player Vs. AI") //If the user selects the first option - to play against an AI:
                        {
                            Checkerboard checkerBoard = new Checkerboard(); //The checkerboard is launched.
                            checkerBoard.displayCheckerBoard();
                        }
                        else if (menuItems[selectedItem] == "Player Vs. Player") //If the user selects the second option - to play against another player:
                        {
                            TwoPlayerCheckerboard checkerBoard = new TwoPlayerCheckerboard(); //The special - two player - checkerboard is selected.
                            checkerBoard.displayCheckerBoard();
                        }
                        break;
                }
                Console.Clear(); //On each button press the console is cleared. 
                //Console.WriteAscii("CHECKERS", Color.FromArgb(DA, V, ID)); //Redraws the checkers logo
                Console.WriteLine("By Ross Houliston - 40282609", Color.Red); //redraws the programmer name
            }
        }
    }
}

