using System;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
class Squares
{
    static Random random = new Random();
    static int round = 1;
    static int cursor_x = 2, cursor_y = 1;
    static int playerScore;
    static int computerScore;
    static bool[,] ownerless_squares = new bool[9, 16];
    static bool[,] player_ownership = new bool[9, 16];
    static bool[,] computer_ownership = new bool[9, 16];
    static bool[,] new_lines = new bool[19, 33];
    static bool[,] lines = new bool[19, 33]; //   (even, even) points are constant
                                             //                                      and always "false" (they refer to "+")

    //                                      (odd, odd) points refer to squareable areas
    //                                      and "true" iff there is a square (P, C, or ownerless (:))
    static bool[,] rndcizgi = new bool[5, 5];
    static int[,] connectedlik = new int[5, 5];


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
                else if (new_lines[row, column] == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (row % 2 == 0) Console.Write("-");             //The lines created for phase 3 appear in green.
                    else Console.Write("|");
                    Console.ForegroundColor = ConsoleColor.White;
                }
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


    static void Discretestage3()
    {


        int hehe = 0;
        int x = 0, y = 22;
        int abc = 0;

        void besebestabloyazdırma()
        {
            y = 22;
            for (int i = 0; i < 5; i++)
            {
                x = 0;
                for (int j = 0; j < 5; j++)
                {
                    Console.SetCursorPosition(x + abc, y);
                    if (i % 2 == 0 && j % 2 == 0) { Console.Write("+"); }
                    else if (i % 2 == 1 && j % 2 == 1) { Console.Write(" "); }
                    else if (rndcizgi[i, j])
                    {
                        if (i % 2 == 0 && j % 2 != 0)
                        { Console.Write("-"); }
                        else if (i % 2 != 0 && j % 2 == 0)
                        {
                            Console.Write("|");
                        }
                    }
                    x++;
                }
                y++;

            }
            if (hehe == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(x + abc, y - 3);
                Console.Write(" -->");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
        void besebessilme()
        {
            Console.ReadLine();
            for (int i = 0; i < 65; i++)
            {
                for (int j = 22; j < 27; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(' ');
                }
            }
            x = 0;
            abc = 0;
        }

        void karsilastirma()
        {
            int count = 0;
            bool c;
            int rndxmainden, rndymainden;

            do
            {

                c = false;

                do
                {
                    rndxmainden = random.Next(2, 17);
                    rndymainden = random.Next(2, 31);
                } while (rndxmainden % 2 == 1 || rndymainden % 2 == 1);

                if (lines[rndxmainden - 1, rndymainden - 2] == true && rndcizgi[1, 0] == true)
                {
                    c = true;
                }
                if (lines[rndxmainden - 1, rndymainden] == true && rndcizgi[1, 2] == true)
                {
                    c = true;
                }
                if (lines[rndxmainden - 1, rndymainden + 2] == true && rndcizgi[1, 4] == true)
                {
                    c = true;
                }
                if (lines[rndxmainden + 1, rndymainden - 2] == true && rndcizgi[3, 0] == true)
                {
                    c = true;
                }
                if (lines[rndxmainden + 1, rndymainden] == true && rndcizgi[3, 2] == true)
                {

                    c = true;
                }
                if (lines[rndxmainden + 1, rndymainden + 2] == true && rndcizgi[3, 4] == true)
                {

                    c = true;
                }
                if (lines[rndxmainden - 2, rndymainden - 1] == true && rndcizgi[0, 1] == true)
                {

                    c = true;
                }
                if (lines[rndxmainden, rndymainden - 1] == true && rndcizgi[2, 1] == true)
                {

                    c = true;
                }
                if (lines[rndxmainden + 2, rndymainden - 1] == true && rndcizgi[4, 1] == true)
                {

                    c = true;
                }
                if (lines[rndxmainden - 2, rndymainden + 1] == true && rndcizgi[0, 3] == true)
                {

                    c = true;
                }
                if (lines[rndxmainden, rndymainden + 1] == true && rndcizgi[2, 2] == true)
                {

                    c = true;
                }
                if (lines[rndxmainden + 2, rndymainden + 1] == true && rndcizgi[4, 3] == true)
                {

                    c = true;
                }

            } while (c && count < 100);
            if (c == false)
            {
                if (rndcizgi[4, 3]) { lines[rndxmainden + 2, rndymainden + 1] = rndcizgi[4, 3]; new_lines[rndxmainden + 2, rndymainden + 1] = rndcizgi[4, 3]; }
                if (rndcizgi[2, 3]) { lines[rndxmainden, rndymainden + 1] = rndcizgi[2, 3]; new_lines[rndxmainden, rndymainden + 1] = rndcizgi[2, 3]; }
                if (rndcizgi[0, 3]) { lines[rndxmainden - 2, rndymainden + 1] = rndcizgi[0, 3]; new_lines[rndxmainden - 2, rndymainden + 1] = rndcizgi[0, 3]; }
                if (rndcizgi[4, 1]) { lines[rndxmainden + 2, rndymainden - 1] = rndcizgi[4, 1]; new_lines[rndxmainden + 2, rndymainden - 1] = rndcizgi[4, 1]; }
                if (rndcizgi[2, 1]) { lines[rndxmainden, rndymainden - 1] = rndcizgi[2, 1]; new_lines[rndxmainden, rndymainden - 1] = rndcizgi[2, 1]; }
                if (rndcizgi[0, 1]) { lines[rndxmainden - 2, rndymainden - 1] = rndcizgi[0, 1]; new_lines[rndxmainden - 2, rndymainden - 1] = rndcizgi[0, 1]; }
                if (rndcizgi[1, 0]) { lines[rndxmainden - 1, rndymainden - 2] = rndcizgi[1, 0]; new_lines[rndxmainden - 1, rndymainden - 2] = rndcizgi[1, 0]; }
                if (rndcizgi[1, 2]) { lines[rndxmainden - 1, rndymainden] = rndcizgi[1, 2]; new_lines[rndxmainden - 1, rndymainden] = rndcizgi[1, 2]; }
                if (rndcizgi[1, 4]) { lines[rndxmainden - 1, rndymainden + 2] = rndcizgi[1, 4]; new_lines[rndxmainden - 1, rndymainden + 2] = rndcizgi[1, 4]; }
                if (rndcizgi[3, 0]) { lines[rndxmainden + 1, rndymainden - 2] = rndcizgi[3, 0]; new_lines[rndxmainden + 1, rndymainden - 2] = rndcizgi[3, 0]; }
                if (rndcizgi[3, 2]) { lines[rndxmainden + 1, rndymainden] = rndcizgi[3, 2]; new_lines[rndxmainden + 1, rndymainden] = rndcizgi[3, 2]; }
                if (rndcizgi[3, 4]) { lines[rndxmainden + 1, rndymainden + 2] = rndcizgi[3, 4]; new_lines[rndxmainden + 1, rndymainden + 2] = rndcizgi[3, 4]; }

            }



        }
        int countstage3 = 0;
        int rndcizgix;
        int rndcizgiy;



        while (countstage3 < 3)
        {
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    rndcizgi[i, j] = false;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    connectedlik[i, j] = 0;
                }
            }
            for (int i = 0; i < 3 - countstage3; i++)
            {
                do
                {
                    rndcizgix = random.Next(0, 5);
                    rndcizgiy = random.Next(0, 5);
                } while ((rndcizgix % 2 == rndcizgiy % 2) || rndcizgi[rndcizgix, rndcizgiy] == true);
                if (rndcizgix % 2 == 0 && rndcizgiy % 2 != 0)
                {
                    rndcizgi[rndcizgix, rndcizgiy] = true;
                    connectedlik[rndcizgix, rndcizgiy - 1] = 1;
                    connectedlik[rndcizgix, rndcizgiy + 1] = 1;

                }
                else if (rndcizgix % 2 != 0 && rndcizgiy % 2 == 0)
                {
                    rndcizgi[rndcizgix, rndcizgiy] = true;
                    connectedlik[rndcizgix - 1, rndcizgiy] = 1;
                    connectedlik[rndcizgix + 1, rndcizgiy] = 1;
                }
            }

            for (int i = 0; i < rndcizgi.GetLength(0); i += 2)
            {
                for (int j = 0; j < rndcizgi.GetLength(1); j += 2)
                {
                    if (connectedlik[i, j] == 1) { count++; }
                }
            }
            if (countstage3 == 0 && count == 4 || countstage3 == 1 && count == 3 || countstage3 == 2 && count == 2)
            {


                besebestabloyazdırma();
                hehe = 1;
                abc += 11;
                while (!(connectedlik[0, 0] == 1 || connectedlik[0, 2] == 1 || connectedlik[0, 4] == 1))
                {
                    for (int i = 0; i <= rndcizgi.GetLength(0) - 1; i += 2)
                    {
                        connectedlik[0, i] = connectedlik[2, i];
                        connectedlik[2, i] = connectedlik[4, i];
                        connectedlik[4, i] = 0;
                        rndcizgi[1, i] = rndcizgi[3, i];
                        rndcizgi[3, i] = false;
                        if (i < 4)
                        {

                            rndcizgi[0, i + 1] = rndcizgi[2, i + 1];
                            rndcizgi[2, i + 1] = rndcizgi[4, i + 1];
                            rndcizgi[4, i + 1] = false;
                        }
                    }

                }
                while (!(connectedlik[0, 0] == 1 || connectedlik[2, 0] == 1 || connectedlik[4, 0] == 1))
                {
                    for (int i = 0; i <= rndcizgi.GetLength(0) - 1; i += 2)
                    {
                        connectedlik[i, 0] = connectedlik[i, 2];
                        connectedlik[i, 2] = connectedlik[i, 4];
                        connectedlik[i, 4] = 0;
                        rndcizgi[i, 1] = rndcizgi[i, 3];
                        rndcizgi[i, 3] = false;
                        if (i < 4)
                        {
                            rndcizgi[i + 1, 0] = rndcizgi[i + 1, 2];
                            rndcizgi[i + 1, 2] = rndcizgi[i + 1, 4];
                            rndcizgi[i + 1, 4] = false;
                        }

                    }

                }
                besebestabloyazdırma();
                hehe = 0;
                abc += 11;
                karsilastirma();

                countstage3++;



            }

        }


        besebessilme();
        OwnershipTag(ref ownerless_squares, lines, computer_ownership, player_ownership);

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
        Console.SetCursorPosition(34, 0);
        Console.Write($"Round: {round} ");
        Console.SetCursorPosition(34, 4);
        Console.Write($"Your score: {playerScore}");
        Console.SetCursorPosition(34, 5);
        Console.Write($"Computer score: {computerScore}");
        Console.SetCursorPosition(0, 19);

        int pCount = 0;
        foreach (bool s in player_ownership) if (s == true) pCount++;
        Console.SetCursorPosition(34, 7);
        Console.Write($"Your squares: {pCount}");
        Console.SetCursorPosition(34, 8);
        int cCount = 0;
        foreach (bool s in computer_ownership) if (s == true) cCount++;

        Console.Write($"Computer squares: {cCount}");
        Console.SetCursorPosition(34, 9);
        int oCount = 0;
        foreach (bool s in ownerless_squares) if (s == true) oCount++;

        Console.Write($"Ownerless squares: {oCount}");

        Console.SetCursorPosition(0, 19);
    }

    static bool AddNewLineWithCursor(ref bool[,] lines_array) // Adds a new line wherever the
                                                              // the player chooses

    //              Use arrows to move

    //               WARNING!: This DOES NOT print the board. Use PrintAll(),
    //                         hence OwnershipTag (because you muse use OwnershipTag 
    //                         before using PrintAll), to print the board
    //     

    // !!!! IF THE PLAYER SKIPS THIS (TAB) THIS RETURNS FALSE, ELSE (IF PLAYER CHOOSES TO ADD A NEW LINE) TRUE
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

                        case ConsoleKey.Tab:
                            if (lines_array[y_save, x_save] == false)
                            {
                                Console.SetCursorPosition(x_save, y_save);
                                Console.Write(" ");
                            }
                            return false;

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
        return true;


    }

    static int[] DidExtraSquareOccurAtFirst(int squareRow, int squareCol)  // if any extra square is formed, this function returns the coordinates of one
    {                                                               // Use: Stage 1 (if any extra square forms in the first move, choose one as the starting point)
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 16; j++)
                if (IsTheAreaSquareable(i, j, lines) == 4 && !player_ownership[i, j] && !computer_ownership[i, j] && !ownerless_squares[i, j]
                    && ((Math.Abs(squareCol - j) == 1 && i == squareRow) || (Math.Abs(squareRow - i) == 1 && j == squareCol))) return [(2 * i) + 1, (2 * j) + 1];
        return null;
    }


    static void Stage1(bool[,] lines, ref bool[,] player_ownership, ref bool[,] ownerless_squares)
    {
        PrintAll();
        Console.WriteLine("Stage 1: Squaring - Begin!");
        int lastSquareRow = -1, lastSquareCol = -1;
        bool continueSquaring = true;
        int irrRow = 0; //unassigned
        int irrCol = 0; //unassigned
        bool first = false;
        int dispRow = 0; //unassigned
        int dispCol = 0; //unassigned
        while (continueSquaring)
        {
            Console.SetCursorPosition(34, 1);
            Console.Write("Your Turn");
            Console.SetCursorPosition(34, 2);
            Console.Write("Stage 1");
            Console.SetCursorPosition(0, 20);
            Console.Write("Press TAB to skip Stage 1");

            // add a new line
            if (!AddNewLineWithCursor(ref lines))   // the player may skip Stage 1 to avoid any irregular square penalties
            {
                Console.SetCursorPosition(0, 19);
                Console.WriteLine("Stage 1 is skipped.                      ");
                Console.Write("Press enter to continue...                     ");
                return;
            }

            bool squareFormed = false;
            int squareRow = 0; //unassigned
            int squareCol = 0;  //unassigned
            bool irregularFormed = false;
            bool isNeighbor = false;


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

                        dispRow = squareRow;
                        dispCol = squareCol;

                        // first square
                        if (lastSquareRow == -1)
                        {
                            if (DidExtraSquareOccurAtFirst(squareRow, squareCol) != null) // if any extra square if formed in the first move, the player must choose one as their starting point
                            {
                                int unchosenRow = DidExtraSquareOccurAtFirst(squareRow, squareCol)[0];
                                int unchosenCol = DidExtraSquareOccurAtFirst(squareRow, squareCol)[1];
                                Console.SetCursorPosition(0, 19);
                                Console.Write("Extra square is formed, chose one as your starting point! (Chose with 'X')");
                                Console.SetCursorPosition(0, 20);
                                Console.Write("                                                                                             ");
                                Console.SetCursorPosition(col + 1, row);

                                Console.Write("X");
                                ConsoleKeyInfo cki;
                                bool loop = true;
                                int cursor_x = col + 1, cursor_y = row;
                                while (loop)
                                {
                                    if (Console.KeyAvailable)
                                    {
                                        cki = Console.ReadKey(true);
                                        switch (cki.Key)
                                        {
                                            case ConsoleKey.RightArrow:
                                                if (unchosenCol == cursor_x + 2)
                                                {
                                                    Console.SetCursorPosition(cursor_x, cursor_y);
                                                    Console.Write(" ");
                                                    Console.SetCursorPosition(unchosenCol, row);
                                                    Console.Write("X");
                                                    (cursor_x, unchosenCol) = (unchosenCol, cursor_x);
                                                }
                                                break;
                                            case ConsoleKey.LeftArrow:
                                                if (unchosenCol == cursor_x - 2)
                                                {
                                                    Console.SetCursorPosition(cursor_x, cursor_y);
                                                    Console.Write(" ");
                                                    Console.SetCursorPosition(unchosenCol, row);
                                                    Console.Write("X");
                                                    (cursor_x, unchosenCol) = (unchosenCol, cursor_x);
                                                }
                                                break;
                                            case ConsoleKey.UpArrow:
                                                if (unchosenRow == cursor_y - 2)
                                                {
                                                    Console.SetCursorPosition(cursor_x, cursor_y);
                                                    Console.Write(" ");
                                                    Console.SetCursorPosition(col + 1, unchosenRow);
                                                    Console.Write("X");
                                                    (cursor_y, unchosenRow) = (unchosenRow, cursor_y);
                                                }
                                                break;
                                            case ConsoleKey.DownArrow:
                                                if (unchosenRow == cursor_y + 2)
                                                {
                                                    Console.SetCursorPosition(cursor_x, cursor_y);
                                                    Console.Write(" ");
                                                    Console.SetCursorPosition(col + 1, unchosenRow);
                                                    Console.Write("X");
                                                    (cursor_y, unchosenRow) = (unchosenRow, cursor_y);
                                                }
                                                break;

                                            case ConsoleKey.Spacebar:
                                                loop = false;
                                                squareRow = (cursor_y - 1) / 2;
                                                squareCol = (cursor_x - 1) / 2;
                                                dispRow = squareRow;
                                                dispCol = squareCol;
                                                break;

                                        }
                                    }



                                }

                            }




                            player_ownership[squareRow, squareCol] = true;
                            OwnershipTag(ref ownerless_squares, lines, player_ownership, computer_ownership);
                            first = true;

                            lastSquareRow = squareRow;
                            lastSquareCol = squareCol;
                            squareFormed = true;
                        }
                        else
                        {
                            // checking neighbor (prev square)
                            isNeighbor = false;

                            if ((squareRow == lastSquareRow && Math.Abs(squareCol - lastSquareCol) == 1) || // right - left
                                (squareCol == lastSquareCol && Math.Abs(squareRow - lastSquareRow) == 1))   // up - down
                            {
                                isNeighbor = true;
                            }

                            if (isNeighbor)
                            {
                                player_ownership[squareRow, squareCol] = true;
                                OwnershipTag(ref ownerless_squares, lines, player_ownership, computer_ownership);
                                //  --> if an extra square is formed, mark it as ownerless


                                lastSquareRow = squareRow;
                                lastSquareCol = squareCol;
                                squareFormed = true;
                            }
                            else
                            {
                                // if the new square is not neighbour 

                                ownerless_squares[squareRow, squareCol] = true;

                                playerScore -= 5;
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

                continueSquaring = false;


            }
            else playerScore++;
            PrintAll();
            if (first)
            {
                Console.WriteLine($"First square formed at ({dispRow}, {dispCol})!");
                first = false;
            }
            else if (isNeighbor) Console.WriteLine($"Square formed at ({dispRow}, {dispCol})!");
            if (!squareFormed)
            {
                Console.WriteLine("No more squares can be formed. Stage 1 ends.");
                if (irregularFormed) Console.WriteLine($"Irregular square at ({dispRow}, {dispCol})! -5 points.");
            }

        }
        cursor_x = 2;
        cursor_y = 1;


    }

    static void Stage2()
    {
        Console.SetCursorPosition(0, 20);
        Console.WriteLine("Stage 2: Placing an Extra Line - Begin!");

        Console.SetCursorPosition(34, 1);
        Console.Write("Your Turn");
        Console.SetCursorPosition(34, 2);
        Console.Write("Stage 2");
        Console.SetCursorPosition(0, 21);
        Console.Write("Press TAB to skip Stage 2");


        bool squareFormed = false;
        if (!AddNewLineWithCursor(ref lines)) // Adding a new line
        {
            Console.SetCursorPosition(0, 19);
            Console.WriteLine("Stage 2 is skipped.                                         ");
            Console.WriteLine("Press enter to continue...                   ");
            Console.Write("                                                  ");

            // Console.ReadLine();
            return;
        }

        for (int row = 1; row <= 17; row += 2) // Check all squares on the board
        {
            for (int col = 0; col <= 30; col += 2)
            {
                if (lines[row, col] && lines[row, col + 2] &&
                    lines[row - 1, col + 1] && lines[row + 1, col + 1])
                {
                    int squareRow = (row - 1) / 2;
                    int squareCol = col / 2;

                    // If already owned, continue
                    if (player_ownership[squareRow, squareCol] || computer_ownership[squareRow, squareCol] || ownerless_squares[squareRow, squareCol])
                        continue;


                    ownerless_squares[squareRow, squareCol] = true; // Mark as ownerless
                    squareFormed = true;
                }
            }
        }


        PrintAll(); // Updating the board         
        Console.SetCursorPosition(0, 19);
        if (!squareFormed)
        {
            Console.WriteLine("No square was formed. Stage 2 ends.");
        }
        else
        {
            Console.WriteLine("A square has formed and marked as ownerless. Stage 2 ends.");
        }


        Console.Write("Press enter to continue...");
    }


    static sbyte IsTheAreaSquareable(int i, int j, bool[,] lines_array) // is the area squareable just by adding 1 new line
    {                               // !! PARAMETERS ARE i, j OF OWNERSHIP ARRAYS (not lines array)
                                    // ownership arrays are [9, 16], where the lines array is [19, 33].
                                    // the function returns 3 if the area is squareable, 4 if it is already a square
        if (i < 0 || i > 8 || j < 0 || j > 15) return -1; // if the area is out of the board
        sbyte counter = 0;
        if (lines_array[(2 * i) + 1, 2 * j]) counter++;
        if (lines_array[2 * i, (2 * j) + 1]) counter++;
        if (lines_array[(2 * i) + 1, (2 * j) + 2]) counter++;
        if (lines_array[(2 * i) + 2, (2 * j) + 1]) counter++;



        return counter;
    }

    static void SquareTheArea(ref bool[,] lines_array, int i, int j, bool mode)// this makes a square in the selected area
    {                                                   // For lines_array:
                                                        // if ComputerAIStage1 function, use "imaginaryLines"
        lines_array[(2 * i) + 1, 2 * j] = true; // if DisplayComputerMoves function, use "lines"
        lines_array[(2 * i), (2 * j) + 1] = true;
        lines_array[(2 * i) + 1, (2 * j) + 2] = true;
        lines_array[(2 * i) + 2, (2 * j) + 1] = true;

        // MODE: IF YOU ARE DISPLAYING AFTER SQUARING, USE true
        // e.g. if lines_array == imaginaryLines: false
        //      if lines_array == 'lines' (that is the array we display on the board) : true
        if (mode)
        {
            Console.SetCursorPosition(2 * j, (2 * i) + 1);
            Console.Write("|");
            Console.SetCursorPosition((2 * j) + 1, 2 * i);
            Console.Write("-");
            Console.SetCursorPosition((2 * j) + 2, (2 * i) + 1);
            Console.Write("|");
            Console.SetCursorPosition((2 * j) + 1, (2 * i) + 2);
            Console.Write("-");
            Console.SetCursorPosition(2 * j + 1, (2 * i) + 1);

            computer_ownership[i, j] = true;
            OwnershipTag(ref ownerless_squares, lines, player_ownership, computer_ownership); // if any extra square occured (2 squares at the same time)
                                                                                              // mark it ass ownerless
            PrintOwnership(2, computer_ownership);
            PrintOwnership(0, ownerless_squares);
        }
    }





    static void ComputerAIStage1(int difficulty, ref byte[] theBestDirections, ref int[] theBestStartingPoint) // difficulty is either 5 or 50 or 500 
    {

        int highestSquareCountReached = 0;


        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                if (IsTheAreaSquareable(i, j, lines) == 3) // now we found a starting point that can be turned into a square
                {


                    for (int tries = 0; tries < difficulty; tries++)
                    {
                        int x = i, y = j;
                        byte[] currentDirections = new byte[143];
                        int squaresMade = 0;

                        bool[,] imaginaryLines = (bool[,])lines.Clone(); // it will try to square the area with these imaginary lines, 
                                                                         // and update the real lines array with the best sequence

                        bool keepSquaring = true;

                        while (keepSquaring)
                        {
                            SquareTheArea(ref imaginaryLines, x, y, false);
                            squaresMade++;
                            int xSave = x, ySave = y;
                            byte direction;
                            do
                            {
                                x = xSave;
                                y = ySave;
                                direction = (byte)random.Next(1, 5);
                                switch (direction)
                                {
                                    case 1: // up
                                        x--;
                                        break;
                                    case 2: // right
                                        y++;
                                        break;
                                    case 3: // down
                                        x++;
                                        break;
                                    case 4: // left
                                        y--;
                                        break;
                                }
                            } while (x < 0 || x > 8 || y < 0 || y > 15); // now it has chosen a valid direction
                            if (IsTheAreaSquareable(x, y, imaginaryLines) != 3) keepSquaring = false; // if a square cannot be formed in that direction
                                                                                                      // , stop squaring and 
                                                                                                      // move on to the next try / starting point
                            else currentDirections[squaresMade - 1] = direction;
                        }
                        if (squaresMade > highestSquareCountReached)
                        {
                            highestSquareCountReached = squaresMade;
                            theBestStartingPoint[0] = i;                 // save that sequence as the best one
                            theBestStartingPoint[1] = j;
                            for (int m = 0; m < squaresMade; m++)
                            {
                                theBestDirections[m] = currentDirections[m];
                            }
                        }
                    }
                }
            }

        }
    }
    // now we have the best starting point -> theBestStartingPoint
    //            and the best directions  -> theBestDirections


    public static char[,] Stage3(int numberOfLines)
    {
        char[,] lines = new char[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)            // We create an empty 5 by 5 table.
            {
                if (i % 2 == 0 && j % 2 == 0)
                {
                    lines[i, j] = '+';
                }
                else lines[i, j] = ' ';
            }
        }
        Random rnd = new Random();
        int x = rnd.Next(3) * 2;                   //We choose one of 9 random points in a 5 by 5 table and choose a point to start drawing a line.
        int y = rnd.Next(3) * 2;
        int counter = 0;
        bool isItVertical = false;
        bool isItFarLeft = false;
        bool isItUpper = false;
        int num;
        while (counter < numberOfLines)
        {
            isItVertical = Convert.ToBoolean(rnd.Next(2));       // We check whether the line to be drawn from the selected point is vertical or horizontal.
                                                                 // 0 represents false and 1 represents true.

            num = ((rnd.Next(2) + 1) * 4) - 6;                   //Here, the direction the line will go is chosen randomly.
                                                                 // If it is 2, it means it is going up or to the right; if it is -2, it means it is going down or to the left.

            if (isItVertical && x + num < 5 && x + num >= 0)     //If it is vertical, it goes into this block.
            {
                if (lines[x + (num / 2), y] != '|')               //If there is a line, it does not process these blocks. If not, it draws lines according to the values.
                {
                    lines[x + (num / 2), y] = '|';                //Since the y values ​​remain the same, a line is added to one side of the selected point.
                    counter++;
                    if (x + (num / 2) == 1)                       //Here we check whether the line formed is at the top.
                    {
                        isItUpper = true;
                    }
                    if (y == 0)                                    //Here we check whether the line formed is on the far left.
                    {
                        isItFarLeft = true;
                    }
                    x = x + num;                                  //We update the x coordinate of the new point.

                }
            }
            else if (!isItVertical && y + num < 5 && y + num >= 0) //If it is vertical, it goes into this block.
            {
                if (lines[x, y + (num / 2)] != '-')
                {
                    lines[x, y + (num / 2)] = '-';                //Since the x values ​​remain the same, a line is added to one side of the selected point.
                    counter++;
                    if (y + (num / 2) == 1)
                    {
                        isItFarLeft = true;
                    }
                    if (x == 0)
                    {
                        isItUpper = true;
                    }
                    y = y + num;                                   //We update the x coordinate of the new point.
                }
            }
        }
        while (!isItUpper)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((lines[i, j] == '|') || (lines[i, j] == '-'))
                    {
                        lines[i - 2, j] = lines[i, j];             //If the line is not at the top, it moves it up until it is at the top.
                        lines[i, j] = ' ';
                        if ((i - 2 == 0) || (i - 2 == 1))          //When it reaches the top it will be out of this block.
                        {
                            isItUpper = true;
                        }
                    }

                }

            }

        }
        while (!isItFarLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((lines[i, j] == '|') || (lines[i, j] == '-'))
                    {
                        lines[i, j - 2] = lines[i, j];              //If the line is not at the far left, it moves it left until it is at the far left.
                        lines[i, j] = ' ';
                        if ((j - 2 == 0) || (j - 2 == 1))           //When it reaches the far left it will be out of this block.
                        {
                            isItFarLeft = true;
                        }
                    }

                }

            }

        }
        return lines;


    }
    static void Stage3Placing()
    {
        int y = 0;
        int x = 0;
        bool flag = false;
        bool[,] tempchars = new bool[5, 5];

        for (int a = 3; a >= 1; a--)                     //The number of lines required for Stage 3 is controlled by the variable a.
        {
            y = random.Next(0, 15) * 2;                  //To start placement, a random point is chosen with x and y variables.
            x = random.Next(0, 8) * 2;
            char[,] chars = Stage3(a);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    tempchars[i, j] = false;             //We created a temporary bool array and made the inside wrong.
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(chars[i, j]);
                }
                Console.WriteLine();
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (chars[i, j] == '|' || chars[i, j] == '-')
                    {
                        tempchars[i, j] = true;              //Pinned the selected rows to a temporary array so we could check them correctly when placing them.
                    }
                }
            }
            while (!flag)
            {
                flag = true;
                y = random.Next(0, 15) * 2;
                x = random.Next(0, 8) * 2;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (tempchars[i, j] == true)
                        {
                            if (lines[i + x, j + y] == tempchars[i, j])  //Checking where the lines are in the 5 by 5 table.
                            {
                                flag = false;                            //By restoring the flag variable, rows from the selected point are placed in the main table and the loop can continue.
                            }


                        }
                    }
                }
            }
            Console.WriteLine("y:" + x + " x:" + y);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (tempchars[i, j] == true)
                    {
                        lines[i + x, j + y] = true;
                        new_lines[i + x, j + y] = true;
                    }
                }
            }
        }
    }

    static void DisplayComputerMoves(int i, int j, byte[] directions, int programcounter, int prevLength)
    {
        // THIS IS A RECURSIVE FUNCTION THAT DISPLAYS THE COMPUTER'S MOVES ON THE RIGHT SIDE OF THE SCREEN
        // programcounter is the NEXT INDEX (in the directions array) of the direction that will be displayed
        SquareTheArea(ref lines, i, j, true);
        computerScore++;
        Console.SetCursorPosition(50, 5);
        Console.Write(computerScore);
        if (programcounter == 0)
        {
            Console.SetCursorPosition(34, 12);
            Console.Write("Starting Point: " + i + ", " + j);
            Console.SetCursorPosition(34, 13);
            Console.Write("Best Directions: ");
        }
        string display = "";
        if (directions[programcounter] == 1) display = "UP ";
        else if (directions[programcounter] == 2) display = "RIGHT ";
        else if (directions[programcounter] == 3) display = "DOWN ";
        else if (directions[programcounter] == 4) display = "LEFT ";
        else display = "END.";

        Console.SetCursorPosition(34, 8);
        int cCount = 0;
        foreach (bool s in computer_ownership) if (s == true) cCount++;

        Console.Write($"Computer squares: {cCount}");

        Console.SetCursorPosition(51 + prevLength, 13);
        prevLength += display.Length;
        Console.Write(display);       // prevLength is the direction's length, so that the next direction will be displayed
                                      // with the proper CursorPosition (spaces between the directions (RIGHT, LEFT etc.) will be equal)




        Console.ReadLine();

        if (directions[programcounter] == 0) return; // if the moves are over, stop the recursive function

        else if (directions[programcounter] == 1) DisplayComputerMoves(i - 1, j, directions, programcounter + 1, prevLength); // Play the next move
        else if (directions[programcounter] == 2) DisplayComputerMoves(i, j + 1, directions, programcounter + 1, prevLength);
        else if (directions[programcounter] == 3) DisplayComputerMoves(i + 1, j, directions, programcounter + 1, prevLength);
        else if (directions[programcounter] == 4) DisplayComputerMoves(i, j - 1, directions, programcounter + 1, prevLength);

    }

    static void ComputerMove(int difficulty)
    {
        PrintAll();
        Console.SetCursorPosition(34, 1);
        Console.Write("Computer's Turn");
        Console.SetCursorPosition(34, 2);
        Console.Write("Stage 1");
        byte[] theBestDirections = new byte[143]; // a total of 144 areas on the board, so max 143 directions
                                                  // can be choosen at a time. (it will probably never happen...) 
        int[] theBestStartingPoint = new int[2];
        ComputerAIStage1(difficulty, ref theBestDirections, ref theBestStartingPoint);
        Console.SetCursorPosition(0, 23);
        DisplayComputerMoves(theBestStartingPoint[0], theBestStartingPoint[1], theBestDirections, 0, 0); // This function not only displays the moves,
                                                                                                         // but also updates the lines in the 'lines' array
    }

    static int[] FollowThePath(ref bool[,] imaginaryLines, int i, int j, List<byte> path) 
    {
        int x = i; 
        int y = j;    


        for(int k = 0; k < path.Count; k++)   // follows the path and returns the ending point
        {
            switch (path[k])
            {
                case 1: // up
                    x--;
                    break;
                case 2: // right
                    y++;
                    break;
                case 3: // down
                    x++;
                    break;
                case 4: // left
                    y--;
                    break;
            }
            SquareTheArea(ref imaginaryLines, x, y, false);
        }
        return [x, y];
    }
    static byte[] TheBestPathFromTheStartingPoint(int i, int j) // (i, j) is the starting point on the board
    {
        List<List<byte>> listOfAllPaths = new List<List<byte>>();

        if (IsTheAreaSquareable(i, j + 1, lines) == 3)
        {
            listOfAllPaths.Add(new List<byte> { 2 }); // right
        }
        if (IsTheAreaSquareable(i, j - 1, lines) == 3)
        {
            listOfAllPaths.Add(new List<byte> { 4 }); // left
        }
        if (IsTheAreaSquareable(i - 1, j, lines) == 3)
        {
            listOfAllPaths.Add(new List<byte> { 1 }); // up
        }
        if (IsTheAreaSquareable(i + 1, j, lines) == 3)
        {
            listOfAllPaths.Add(new List<byte> { 2 }); // down
        }
        if (listOfAllPaths.Count == 0) return new byte[0]; // if there is no path to follow, return an empty array

        // now we have added the paths of length 1

        for (int n = 0; n < listOfAllPaths.Count; n++)
        {
            bool[,] imaginaryLines = (bool[,])lines.Clone();
            SquareTheArea(ref imaginaryLines, i, j, false); // square the starting area
            int[] terminalVertex = FollowThePath(ref imaginaryLines, i, j, listOfAllPaths[n]);
            int x = terminalVertex[0];
            int y = terminalVertex[1];

            if (IsTheAreaSquareable(x, y + 1, imaginaryLines) == 3)
            {
                List<byte> clonePath = new List<byte>(listOfAllPaths[n]);
                clonePath.Add(2);
                listOfAllPaths.Add(clonePath); // right
            }
            if (IsTheAreaSquareable(x, y - 1, imaginaryLines) == 3)
            {
                List<byte> clonePath = new List<byte>(listOfAllPaths[n]);
                clonePath.Add(4);
                listOfAllPaths.Add(clonePath); // left
            }
            if (IsTheAreaSquareable(x - 1, y, imaginaryLines) == 3)
            {
                List<byte> clonePath = new List<byte>(listOfAllPaths[n]);
                clonePath.Add(1);
                listOfAllPaths.Add(clonePath); // up
            }
            if (IsTheAreaSquareable(x + 1, y, imaginaryLines) == 3)
            {
                List<byte> clonePath = new List<byte>(listOfAllPaths[n]);
                clonePath.Add(3);
                listOfAllPaths.Add(clonePath); // down
            }
        }

        // now we have all the paths starting from (i, j)

        byte[] bestDirections = new byte[listOfAllPaths[listOfAllPaths.Count - 1].Count];

        int length = listOfAllPaths[listOfAllPaths.Count - 1].Count;

        for (int k = 0; k < length; k++)
        {
            bestDirections[k] = listOfAllPaths[listOfAllPaths.Count - 1][k];
        }
        return bestDirections; // returns the best path from the given starting point

    }

    static void ComputerAIExtreme(ref byte[] bestPath, ref int[] startingPoint) // returns the best path and the starting point on the board
    {
        int threshold = 0;
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 16; j++)
            {
                if (IsTheAreaSquareable(i, j, lines) != 3) continue;
                // now we have found a starting point
                byte[] bestPathStartingFromThatPoint = TheBestPathFromTheStartingPoint(i, j);
                if (bestPathStartingFromThatPoint.Length > threshold)
                {
                    threshold = bestPathStartingFromThatPoint.Length;
                    for (int k = 0; k < bestPathStartingFromThatPoint.Length; k++)
                    {
                        bestPath[k] = bestPathStartingFromThatPoint[k]; // save the best path

                    }
                    startingPoint[0] = i;
                    startingPoint[1] = j;
                }
            }
       
    }

    static void ComputerMoveExtreme()
    {
        PrintAll();
        Console.SetCursorPosition(34, 1);
        Console.Write("Computer's Turn");
        Console.SetCursorPosition(34, 2);
        Console.Write("Stage 1");
        byte[] theBestDirections = new byte[143]; // a total of 144 areas on the board, so max 143 directions
                                                  // can be choosen at a time. (it will probably never happen...) 
        int[] theBestStartingPoint = new int[2];
        ComputerAIExtreme(ref theBestDirections, ref theBestStartingPoint);
        Console.SetCursorPosition(0, 23);
        DisplayComputerMoves(theBestStartingPoint[0], theBestStartingPoint[1], theBestDirections, 0, 0); // This function not only displays the moves,
                                                                                                         // but also updates the lines in the 'lines' array
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



        Stage1(lines, ref player_ownership, ref ownerless_squares);
        /*
                Stage3Placing();
                Console.ReadLine();
                OwnershipTag(ref player_ownership, lines, ownerless_squares, computer_ownership);
                PrintAll();

        Discretestage3();//Stage3Placing();

        OwnershipTag(ref player_ownership, lines, ownerless_squares, computer_ownership);
        PrintAll();


        */
        Stage2();
        Console.ReadLine();


        //ComputerMove(50000);

        ComputerMoveExtreme();







    }



}