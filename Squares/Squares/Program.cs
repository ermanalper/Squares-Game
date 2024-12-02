using System;
class Squares
{
     static Random random = new Random();
     static void Main()
     {
          Console.Clear();
          bool[,] lines = new bool[19, 33]; //   (even, even) points are constant
          //                                      and always "false" (they refer to "+")

          //                                      (odd, odd) points refer to squareable areas
          //                                      and "true" iff there is a square (P, C, or ownerless (:))

          bool[,] ownerless_squares = new bool[9, 16];
          bool[,] player_ownership = new bool[9, 16];
          bool[,] computer_ownership = new bool[9, 16];


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
          LinePrint(lines); // Prints all the lines including the inner lines
          OwnershipTag(ownerless_squares, lines, player_ownership, computer_ownership);
          // Since the random-formed-squares are ownerless, we signed them so 

          PrintOwnership(0, ownerless_squares); // Prints (:) since Mode == 0 and 
                                                // PrintModeArray == ownerless_squares
          int m = Convert.ToInt16(Console.ReadLine());
                    int n = Convert.ToInt16(Console.ReadLine());
          lines = Stage2ExtraLine(lines, m , n);
          OwnershipTag(player_ownership, lines, ownerless_squares, computer_ownership);
          Console.Clear();
LinePrint(lines); // Prints all the lines including the inner lines
PrintOwnership(1, player_ownership);
PrintOwnership(0, ownerless_squares);
          






     }

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

     static void OwnershipTag(bool[,] whose_ownership, bool[,] lines_array, bool[,] check_array, bool[,] check_array2)
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


     static bool[,] Stage2ExtraLine(bool[,] lines_array, int i, int j) // biri çift biri tek olcak

     {
          bool[,] new_lines_array = lines_array;
          new_lines_array[i, j] = true;
          return new_lines_array;
     }





     
}