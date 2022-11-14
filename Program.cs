/*
 * Working from left to right, calculate the numbers in the order shown (without using order of operation).
 * Replace each question mark with a mathematical sign.
 * Plus, minus, multiply and divide can each be used once only.
 * What are the highest and lowest numbers you can possibly score?
 * 8 ? 2 ? 4 ? 7 ? 6  = ?
 */

// Array with numbers we will perform operations on.
int[] num = { 8, 2, 4, 7, 6 };
// Variable we will start and end our operations on numbers on.
float result = num[0];
// Lowest and highest numbers variable.
float low = 0, high = 0;
// Explained down in code.
int check = 0;

// Array with used spots marked as order of operations.
int[] used = { -1, -1, -1, -1 };
// Array with order of used spots.
int[] order = { -1, -1, -1, -1 };
// Variable assigning used operations with correct number indicating what number in order is the operations.
int oper = 0;
int count = 0;
int c6 = 0;
int del = 0;

/*
 * The program works by going down with loops and assiging used spots.
 * loop 
 *     0
 *      loop
 *          1
 *           loop
 *               2
 *                loop
 *                    3
 *                         pattern made  - 0 1 2 3
 *
 * When pattern is made we erase certain used spots and go back to upper loops
 * creating different patterns.
 * loop 
 *     0
 *      loop
 *          1
 *           loop
 *               X -> 3    
 *                loop
 *                    X -> 2   
 *                         pattern made  - 0 1 3 4
 *                         
 * It keeps going back and calculating the result of all operations made 
 * with certain patterns until all the patterns are completed.
 */


for (int i = 0; i < 4; i++)
{
    // Assign a value of order.
    used[i] = oper++;


    for (int j = 0; j < 4; j++)
    {
        /*
         * Check if the spot was already taken with value of order.
         * If it was continue to next iteration,
         * if it wasn't assign it to unused spot.
         */
if (used[j] != -1) continue;
        else
        {
            used[j] = oper++;

            for (int k = 0; k < 4; k++)
            {

                if (used[k] != -1) continue;
                else
                {
                    used[k] = oper++;

                    for (int l = 0; l < 4; l++)
                    {

                        if (used[l] != -1) continue;
                        else
                        {
                            used[l] = oper++;
  
                            /*
                             * Convert array with used spots to another array showing 
                             * sequence of operations in order.
                             * { 2 3 4 1 } -> { 4 1 2 3 }
                             * { + - * / } -> { / + - * }
                             */
                            for (int b = 0; b < 4; b++)
                            {
                                int tym = used[b];
                                order[tym] = b;
                            }                           

                            /*
                             * Calculate the result using array with opperations in order.
                             */
                            for (int m = 0; m < 4; m++)
                            {
                                /*
                                 * Check the number that corresponds to the certain operation in array with order.
                                 * Print made operation to Console.
                                 * 0 -> +
                                 * 1 -> -
                                 * 2 -> *
                                 * 3 -> /
                                 */
                                if (order[m] == 0)
                                {
                                    Console.Write($"{result} + {num[m + 1]}    ");
                                    result += num[m + 1];
                                }
                                else if (order[m] == 1)
                                {
                                    Console.Write($"{result} - {num[m + 1]}    ");
                                    result -= num[m + 1];
                                }
                                else if (order[m] == 2)
                                {
                                    Console.Write($"{result} * {num[m + 1]}    ");
                                    result *= num[m + 1];
                                }
                                else if (order[m] == 3)
                                {
                                    Console.Write($"{result} / {num[m + 1]}    ");
                                    result /= num[m + 1];
                                }
                                // Round result of operation to 2 floating points.
                                result = (float)Math.Round(result, 2);

                                /*
                                 * After completing all operations of certain order:
                                 * - check if result may be the lowest or the highest
                                 * - check what many operations on current pattern has been made
                                 * - go down to previous certain loops
                                 * - erase used spots related to how many patters has been made
                                 */
                                if (m == 3)
                                {
                                    /*
                                     * If it's the first operation assign first result
                                     * to the lowest and highest variables.
                                     * So we have the base to copare to results from next operations
                                     */
                                    if ( check == 0)
                                    {
                                        low = result;
                                        high = result;
                                        check = 1;
                                    }
                                    else
                                    {
                                        // Compare low and high to newest result.
                                        if (low > result) low = result;
                                        if (high < result) high = result;
                                    }
                                    Console.WriteLine(result);
                                    /*
                                     * Asign result back to the first number in the array of our numbers.
                                     * So each set of operations are starting from the same base value.
                                     */
                                    result = num[0];

                                    //Count the number of operations in current pattern.
                                    count++;

                                    /*
                                     * If it's the first pattern (3rd and 4rd operations are in order)
                                     * ex. 2 3 1 4
                                     *     3 1 2 4
                                     * Assign delete varaible to 2 - it will erase 2 last used spots in next loop.
                                     */
                                    if (count == 1)
                                    {
                                        del = 2;

                                    }
                                    /*
                                     * If it's the second pattern (3rd and 4rd operations are not in order)
                                     * ex. 2 4 1 3
                                     *     4 1 2 3
                                     * Assign delete varaible to 3 - it will erase 3 last used spots in next loop.
                                     * Let the program know that k loop and l loop completed their task and can be 
                                     * ended going back to the loop j.
                                     */
                                    if (count == 2)
                                    {
                                        del = 3;
                                        k = 5;
                                        l = 5;
                                        // Add number of patterns made to c6 variable - explained before next if.
                                        c6 += count;
                                        // Zero the count - we need to know maximium of the 2 operations ware made.
                                        count = 0;
                                    }

                                    /*
                                     * Keep track of how many patterns made with j loop has been made.
                                     * So we know when to go back to the i loop.
                                     * If it's 6 patterns end j k l loops and go back to first i loop.
                                     * Assign delete varaible to 4 - it will erase all used spots in next loop.
                                     * Zero c6 variable.
                                     */
                                    if (c6 == 6)
                                    {
                                        c6 = 0;
                                        j = 5;
                                        k = 5;
                                        l = 5;
                                        del = 4;
                                    }

                                    /*
                                     * Erase used spots based on what variable del is.
                                     * Subtract operations variable related to used spots array so the used spots are marked right.
                                     */
                                    for (int n = 0; n < 4; n++)
                                    {
                                        if (used[n] >= 4 - del)
                                        {
                                            used[n] = -1;
                                            oper--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

//Print lowest and highest number
Console.WriteLine($"The lowest possible number is: {low}\nThe highest possible number is {high}");