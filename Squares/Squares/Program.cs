using System;
using System.Runtime.InteropServices;
class Squares
{
    static Random random = new Random();
    static int cursor_x = 2, cursor_y = 1;
    static int playerScore;
    static bool[,] ownerless_squares = new bool[9, 16];
    static bool[,] player_ownership = new bool[9, 16];
    static bool[,] computer_ownership = new bool[9, 16];
    static bool[,] lines = new bool[19, 33]; //   (even, even) points are constant
                                             //                                      and always "false" (they refer to "+")

    //                                      (odd, odd) points refer to squareable areas
    //                                      and "true" iff there is a square (P, C, or ownerless (:))


    static void LinePrint(bool[,] lines_array) //      This function prints the current state of the board
                                               //      WITHOUT PRINTING THE OWNERSHIP STATUS (P / C / :)
                                               //      You need to run PrintOwnerShip function to print them
    {
        for (int row = 0; row < 19; row++)
        {
            for (int column = 0; column < 33; column++)
            {
                if (row % 2 == 0 && column % 2 == 0) // (even, even) point
                    Console.Write("+");
                else if (row * column % 2 == 1) // (odd, odd) point
                    Console.Write(" ");
                else if (lines_array[row, column] == true)
                {
                    if (row % 2 == 0) Console.Write("-");
                    else Console.Write("|");
                }
                else Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    static bool[,] RandomizeInitialBoard(bool[,] lines_array) // This function switches random 90 bools to "true"
                                                              //                                                         in order to randomize the initial board
    {
        for (int i = 1; i <= 90; i++)
        {
            int row = random.Next(19), column;
            if (row % 2 == 0)
            {
                do
                {
                    column = random.Next(33);
                } while (column % 2 == 0); // Lines can only be at (odd, even) or (even, odd) points
            }
            else
            {
                do
                {
                    column = random.Next(33);
                } while (column % 2 == 1);
            } // Now it is obvious that we have selected an (odd, even) or an (even, odd) point
              //   (All lines (| or -) must be on these points)
            if (lines_array[row, column]) i--; // Iff there is already a line at that point,
                                               //                              (so lines_array[row, column] == true)
                                               //                              DO NOT count that cycle
            else lines_array[row, column] = true; // Creates a line (| or -) at that point
        }
        return lines_array;
    }

    static void OwnershipTag(ref bool[,] whose_ownership, bool[,] lines_array, bool[,] check_array, bool[,] check_array2)
    {
        //       To check if the move has formed a square, write their array
        //        instead of "whose_ownership"
        //        e.g.: After Stage 2 or Initial   -->  ownerless_squares
        //              After Stage 1:
        //                            Player's Move:  --> player_ownership
        //                            Computer's Move: --> computer_ownership

        //        check_array and check_array2 are there
        //        in order to check if there already occured a square at that point beforehand
        //        
        //        Whatever array is written for "whose_ownership", write the leftover-arrays
        //        instead of "check_array" and "check_array2"
        //        e.g.: OwnershipTag(ownerless_squares, lines, player_ownership, computer_ownership);

        for (int row = 1; row <= 17; row += 2)
        {
            for (int column = 0; column <= 30; column += 2)
            {
                if (lines_array[row, column] && lines_array[row, column + 2] &&
                    lines_array[row - 1, column + 1] && lines_array[row + 1, column + 1]) // Determines if 
                                                                                          // there occured a square or not
                                                                                          // by checking the adjacent lines

                {
                    int checkrow = (row - 1) / 2, checkccolumn = column / 2;
                    if (check_array[checkrow, checkccolumn] ||
                          check_array2[checkrow, checkccolumn]) continue; // When a square is found
                                                                          // but if it has already been occupied
                                                                          // and signed as P, C or :   ,
                                                                          // this code line skips to the next cycle

                    else whose_ownership[checkrow, checkccolumn] = true; // Signs and switches the value to "true"
                                                                         // if it is a new one.
                }
            }
        }
    }

    static void PrintOwnership(byte mode, bool[,] PrintModeArray)
    {
        // 1. Enter a mode accordingly: 0 for Ownerless (:)
        //                              1 for Player (P)
        //                              2 for Computer (C)

        //    ---->  Entering an invalid mode will cause an inproper use of the function

        // 2: Write the name of the array instead of PrintModeArray
        // e.g.: PrintOwnership(0, ownerless_squares);
        //       PrintOwnership(1, player_ownership)


        if (mode == 0 || mode == 1 || mode == 2)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 16; column++)
                {
                    if (PrintModeArray[row, column])
                    {
                        Console.SetCursorPosition((column * 2) + 1, (row * 2) + 1);
                        switch (mode)
                        {
                            case 0: // Mode: Ownerless (:)
                                Console.Write(":");
                                break;
                            case 1: // Mode: Player (P)
                                Console.Write("P");
                                break;
                            case 2: // Mode: Computer (C)
                                Console.Write("C");
                                break;
                        }
                    }
                }
            }
        }
        else // This must not occur, the developer made a mistake in the use of the function
        {
            Console.WriteLine("!! PrintOwnership function ERROR! You (the developer) must chose among Modes: 0, 1 or 2");
            Console.ReadLine();
            Environment.Exit(0);
        }
        Console.SetCursorPosition(0, 19);

    }

    static void PrintAll() // Instead of printing the ownership tags
                           //                       one by one after printing the board,
                           //                       just use PrintAll()

    //                       WARNING!! : YOU STILL NEED TO MARK THE SQUARES
    //                                   WITH THE OwnershipTag FUNCTION BEFORE
    //                                   USING THIS FUNCTIONS

    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        LinePrint(lines);
        PrintOwnership(0, ownerless_squares);
        PrintOwnership(1, player_ownership);
        PrintOwnership(2, computer_ownership);
        Console.SetCursorPosition(0, 19);
    }

    static void AddNewLineWithCursor(ref bool[,] lines_array) // Adds a new line wherever the
                                                              // the player chooses

    //              Use arrows to move

    //               WARNING!: This DOES NOT print the board. Use PrintAll(),
    //                         hence OwnershipTag (because you muse use OwnershipTag 
    //                         before using PrintAll), to print the board
    //     
    {

        ConsoleKeyInfo cki;
        Console.SetCursorPosition(cursor_x, cursor_y);
        if (cursor_x % 2 == 0)
            Console.Write("|");
        else Console.Write("-");
        bool inserted = false;
        do
        {
            bool loop = true;

            bool boarder = false;

            while (loop)
            {


                if (Console.KeyAvailable)
                {
                    int x_save = cursor_x, y_save = cursor_y;

                    cki = Console.ReadKey(true);

                    switch (cki.Key)
                    {
                        case ConsoleKey.Spacebar:
                            loop = false;
                            break;
                        case ConsoleKey.RightArrow:
                            if (cursor_x < 30) cursor_x += 2;
                            else if (cursor_x == 30 && cursor_y <= 15)
                            {
                                cursor_x++;
                                cursor_y++;
                            }
                            else if (cursor_x == 30 && cursor_y == 17)
                            {
                                cursor_x++;
                                cursor_y--;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (cursor_x > 2) cursor_x -= 2;
                            else if (cursor_x == 2 && cursor_y <= 15)
                            {
                                cursor_x--;
                                cursor_y++;
                            }
                            else if (cursor_x == 2 && cursor_y == 17)
                            {
                                cursor_x--;
                                cursor_y--;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (cursor_y >= 2 && cursor_x < 31)
                            {
                                cursor_y--;
                                if (cursor_x % 2 == 1 || boarder)
                                {
                                    cursor_x++;
                                    boarder = false;
                                }
                                else cursor_x--;
                            }
                            else if (cursor_y >= 2 && cursor_x == 31)
                            {
                                cursor_y--;
                                cursor_x--;
                                boarder = true;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (cursor_y <= 16 && cursor_x < 31)
                            {
                                cursor_y++;
                                if (cursor_x % 2 == 1 || boarder)
                                {
                                    boarder = false;
                                    cursor_x++;
                                }
                                else cursor_x--;
                            }
                            else if (cursor_y <= 16 && cursor_x == 31)
                            {
                                cursor_y++;
                                cursor_x--;
                                boarder = true;
                            }
                            break;

                    }
                    if (lines_array[y_save, x_save] == false)
                    {
                        Console.SetCursorPosition(x_save, y_save);
                        Console.Write(" ");
                    }
                    Console.SetCursorPosition(cursor_x, cursor_y);
                    if (cursor_x % 2 == 0) Console.Write("|");
                    else Console.Write("-");
                }

            }
            if (lines_array[cursor_y, cursor_x] == false)
            {
                inserted = true;
                lines_array[cursor_y, cursor_x] = true;
            }
        } while (!inserted);


    }


    static int Stage1(bool[,] lines, ref bool[,] player_ownership, ref bool[,] ownerless_squares, int currentScore)
    {
        PrintAll();
        Console.WriteLine("Stage 1: Squaring - Begin!");
        int lastSquareRow = -1, lastSquareCol = -1;
        bool continueSquaring = true;
        int irrRow = 0; //unassigned
        int irrCol = 0; //unassigned
        
        while (continueSquaring)
        {
            // add a new line
            AddNewLineWithCursor(ref lines);
            bool squareFormed = false;
            int squareRow = 0; //unassigned
            int squareCol = 0;  //unassigned
            bool irregularFormed = false;

            // check all the squares on the board
            for (int row = 1; row <= 17; row += 2)
            {
                for (int col = 0; col <= 30; col += 2)
                {
                    // check if the new line forms a square
                    if (lines[row, col] && lines[row, col + 2] &&
                        lines[row - 1, col + 1] && lines[row + 1, col + 1])
                    {
                        squareRow = (row - 1) / 2;
                        squareCol = col / 2;

                        // if it is already a square continue 
                        if (player_ownership[squareRow, squareCol] || ownerless_squares[squareRow, squareCol])
                            continue;

                        // first square
                        if (lastSquareRow == -1)
                        {
                            player_ownership[squareRow, squareCol] = true;
                            PrintAll();
                            Console.WriteLine($"First square formed at ({squareRow}, {squareCol})!");

                            lastSquareRow = squareRow;
                            lastSquareCol = squareCol;
                            squareFormed = true;
                        }
                        else
                        {
                            // checking neighbor (prev square)
                            bool isNeighbor = false;

                            if ((squareRow == lastSquareRow && Math.Abs(squareCol - lastSquareCol) == 1) || // right - left
                                (squareCol == lastSquareCol && Math.Abs(squareRow - lastSquareRow) == 1))   // up - down
                            {
                                isNeighbor = true;
                            }

                            if (isNeighbor)
                            {
                                player_ownership[squareRow, squareCol] = true;

                                PrintAll();
                                Console.WriteLine($"Square formed at ({squareRow}, {squareCol})!");
                                lastSquareRow = squareRow;
                                lastSquareCol = squareCol;
                                squareFormed = true;
                            }
                            else
                            {
                                // if the new square is not neighbour 

                                ownerless_squares[squareRow, squareCol] = true;
                                PrintAll();
                                currentScore -= 5;
                                irregularFormed = true;
                                irrRow = squareRow;
                                irrCol = squareCol;
                            }
                        }
                    }
                }
            }

            if (!squareFormed)
            {
                Console.SetCursorPosition(0, 19);
                Console.WriteLine("No more squares can be formed. Stage 1 ends.");
                continueSquaring = false;
                if (irregularFormed)
                    Console.WriteLine($"Irregular square at ({irrRow}, {irrCol})! -5 points.");
            }
        }
        cursor_x = 2;
        cursor_y = 1;

        return currentScore;
    }

    static void Main()
    {
        Console.Clear();





        for (int i = 0; i < 33; i++) //Setting up the upper and the bottom-outer-border-lines
        {
            lines[0, i] = true;
            lines[18, i] = true;
        }
        for (int i = 0; i < 19; i++) //Setting up the left and the right-outer-border-lines
        {
            lines[i, 0] = true;
            lines[i, 32] = true;

        }
        lines = RandomizeInitialBoard(lines); // Initializes the board

        OwnershipTag(ref ownerless_squares, lines, player_ownership, computer_ownership);
        // Since the random-formed-squares are ownerless, we signed them so 

        PrintAll();

        //AddNewLineWithCursor(ref lines);
        OwnershipTag(ref player_ownership, lines, ownerless_squares, computer_ownership);
        PrintAll();
        playerScore = Stage1(lines, ref player_ownership, ref ownerless_squares, playerScore);

        Console.ReadLine();





    }



}