using System;
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
    static void besebestabloyazdırma()
    {
        Console.Clear();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.SetCursorPosition(j, i);
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
            }
        }
        Console.ReadLine();
    }

    static void Dicretestage3()
    {
        Random rnd = new Random();// burda gerekli değişkenleri atadım.
                                  // bağlantılı olup olmadığını göstericek 1.adımda 4 se/ 2 de 3/ 3te 2 taneyse bağlantılı demektir.
        int countstage3 = 0; // 3 bağlantılı şekilden 1 bağlantılı şekle kadarki stage aşamaları
        int rndcizgix = 0; // dizinin x bileşeni
        int rndcizgiy = 0;// dizinin y bileşeni 



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
            for (int i = 0; i < 3 - countstage3; i++) // bu kenarlara + koyma işlemini adımlara göre 3 2 veya 1 defa yapıyorum.
            {
                do
                {
                    rndcizgix = rnd.Next(0, 5);
                    rndcizgiy = rnd.Next(0, 5);
                } while ((rndcizgix % 2 == rndcizgiy % 2) || rndcizgi[rndcizgix, rndcizgiy] == true); // x ve y aynı anda tek ya da çift olmayana kadar dönmeli. 2 si de tek olursa boşluk kısımlar ikisi de çift olursa + ların geleceği kısım olmuş oluyor.
                if (rndcizgix % 2 == 0 && rndcizgiy % 2 != 0)
                {
                    rndcizgi[rndcizgix, rndcizgiy] = true; // x çift y tek olursa yatay kısımlardan biri olmuş olur soluna ve sağına + diyip o kısma true koyuyorum.
                    connectedlik[rndcizgix, rndcizgiy - 1] = 1;
                    connectedlik[rndcizgix, rndcizgiy + 1] = 1;

                }
                else if (rndcizgix % 2 != 0 && rndcizgiy % 2 == 0)// x tek y çift olursa dikey kısımlardan biri olmuş olur yukarısına ve aşağısına + diyip o kısma true atıyorum.
                {
                    rndcizgi[rndcizgix, rndcizgiy] = true;
                    connectedlik[rndcizgix - 1, rndcizgiy] = 1;
                    connectedlik[rndcizgix + 1, rndcizgiy] = 1;
                }
            }

            for (int i = 0; i < rndcizgi.GetLength(0); i += 2) //connectedlıa atadığım + değerlerini sayıyorum
            {
                for (int j = 0; j < rndcizgi.GetLength(1); j += 2)
                {
                    if (connectedlik[i, j] == 1) { count++; }
                }
            }
            if (countstage3 == 0 && count == 4 || countstage3 == 1 && count == 3 || countstage3 == 2 && count == 2)
            {


                besebestabloyazdırma();
                Console.Clear();
                while (!(connectedlik[0, 0] == 1 || connectedlik[0, 2] == 1 || connectedlik[0, 4] == 1))//yukarı shitlemek için
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
                while (!(connectedlik[0, 0] == 1 || connectedlik[2, 0] == 1 || connectedlik[4, 0] == 1))//sola shiftlemek için
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
                Console.ReadLine();

                countstage3++;
            }


        }
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

            // add a new line
            AddNewLineWithCursor(ref lines);
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
                            player_ownership[squareRow, squareCol] = true;
                            
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
            else if(isNeighbor) Console.WriteLine($"Square formed at ({dispRow}, {dispCol})!");
            if(!squareFormed)
            {
                Console.WriteLine("No more squares can be formed. Stage 1 ends.");
                if (irregularFormed)Console.WriteLine($"Irregular square at ({dispRow}, {dispCol})! -5 points.");
            }

        }
        cursor_x = 2;
        cursor_y = 1;

        
    }




    static bool IsTheAreaSquareable(int i, int j) // is the area squareable just by adding 1 new line
    {                               // !! PARAMETERS ARE i, j OF OWNERSHIP ARRAYS (not lines array)
                                    // ownership arrays are [9, 16], where the lines array is [19, 33].
                                    // the function returns true or false by calcculating the corresponding i, j
                                    // point in the lines array
        byte counter = 0;
        if(lines[(2*i) + 1, 2 * j]) counter++;
        if (lines[2*i, (2*j) + 1]) counter++;
        if(lines[(2*i) + 1, (2 * j) + 2]) counter++;
        if(lines[(2*i) + 2, (2*j) + 1]) counter++;
        
        return counter == 3;
    }
    
    static void SquareTheArea(ref bool[,] imaginaryLines, int i, int j)// this makes a square in the selected area
    {
        imaginaryLines[(2*i) + 1, 2 * j] = true;
        imaginaryLines[2*i, (2*j) + 1] = true;
        imaginaryLines[(2*i) + 1, (2 * j) + 2] = true;
        imaginaryLines[(2*i) + 2, (2*j) + 1] = true;
    }


    
    static void ComputerAIStage1(int difficulty) // difficulty is either 5 or 50 or 500 
    {

        int highestSquareCountReached = 0;

        byte[] theBestDirections = new byte[143]; // a total of 144 boards on the board, so max 143 directions
                                    // can be choosen at a time. (it will probably never happen...) 
        byte[] theBestStartingPoint = new byte[2];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                if (ownerless_squares[i, j] || player_ownership[i, j] || computer_ownership[i, j]) continue;
                // skip to the next AREA if the current one is already a square
                
                if (IsTheAreaSquareable(i, j)) // now we found a starting point that can be turned into a square
                {
                    
                    int x = i, y = j;
                    for (int tries = 0; tries < difficulty; tries++)
                    {
                        bool[,] imaginaryLines = (bool[,])lines.Clone();
                        bool keepSquaring = true;
                        SquareTheArea(ref imaginaryLines, i , j);
                        int newSquares = 1;

                        byte[]currentDirections = new byte[143];
                        int directionIndex = 0;
                        byte direction;

                        while(keepSquaring)
                        {
                            int currX = x, currY = y;
                            do{
                                x = currX;
                                y = currY;
                                direction = (byte)random.Next(1, 5); // 1: UP, 2: RIGHT, 3: DOWN, 4: LEFT
                                                                    // (0 is used in the array where we hold the directions
                                                                    // so it would be a problem if 0 sampled a direction)
                                switch (direction)
                                {
                                    case 1: // UP
                                        x--;
                                        break;
                                    case 2: // RIGHT
                                        y++;
                                        break;
                                    case 3: // DOWN
                                        x++;
                                        break;
                                    case 4: //LEFT
                                        y--;
                                        break;
                                }
                            }while(x >= 0 && y >= 0 && x < 9 && y < 16); // now the ai has chosen a valid direction
                            // (x, y) is the neighbor area that it will try to square next
                            if(!IsTheAreaSquareable(x, y)) keepSquaring = false;
                            else
                            {
                                SquareTheArea(ref imaginaryLines, x, y);
                                newSquares++;
                                currentDirections[directionIndex] = direction;
                                directionIndex++;
                            }
                        }
                        if(newSquares > highestSquareCountReached) // setting the new high
                        {
                            highestSquareCountReached = newSquares;
                            for (int b = 0; b < theBestDirections.Length; b++) b = 0; // reset the directions array
                            int insertIndex = 0;
                            int nextIndex = 1;
                            do{
                                theBestDirections[insertIndex] = currentDirections[insertIndex];
                                insertIndex++;
                                nextIndex++;
                            }while(nextIndex != 0 && insertIndex < 143); //stored the best directions
                            // Format: 4, 2, 1, 4, 3, 0, 0, 0, 0, 0, 0, .....0    :: total 143 numbers
                            theBestStartingPoint[0] = (byte)i;
                            theBestStartingPoint[1] = (byte)j;
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
                    if (chars[i, j] == true)
                    {
                        lines[i + x, j + y] = true;
                        new_lines[i + x, j + y] = true;
                    }
                }
            }
        }
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
        Stage1(lines, ref player_ownership, ref ownerless_squares);

        Stage3Placing();
        Console.ReadLine();
        OwnershipTag(ref player_ownership, lines, ownerless_squares, computer_ownership);
        PrintAll();

        Console.ReadLine();





    }



}