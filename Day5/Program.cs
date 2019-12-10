using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        private const int POSITION = 0;
        private const int IMMEDIATE = 1;

        static void Main(string[] args)
        {
            const char COMMA = ',';

            // Read input ints and parse
            var originalInput = File.ReadAllText("input.txt").Split(COMMA)
                            .Select(i => int.Parse(i))
                            .ToList();

            // Part 1
            var part1 = new List<int>(originalInput);
            RunIntcode(part1);

            Console.Read();
        }

        private static int RunIntcode(List<int> input)
        {
            // Process each opcode
            for (int i = 0; i < input.Count;)
            {
                var opCode = input[i].ToString().PadLeft(5, '0');

                // Parse Opcode (right 2 digits)
                var op = int.Parse(opCode.Substring(3));
                var pm1 = int.Parse(opCode[2].ToString());
                var pm2 = int.Parse(opCode[1].ToString());
                var pm3 = int.Parse(opCode[0].ToString());

                int pos1, pos2, poso, result, val;

                Console.WriteLine($"Processing opcode {opCode}");

                switch (op)
                {
                    case 1: // Add
                        pos1 = input[i + 1];
                        pos2 = input[i + 2];
                        poso = input[i + 3];

                        // Use position mode or immediate mode
                        result = (pm1 == POSITION ? input[pos1] : pos1) 
                               + (pm2 == POSITION ? input[pos2] : pos2);

                        // Write instruction is always position mode
                        input[poso] = result;

                        // Move pointer
                        i += 4;
                        break;
                    case 2: // Multiply
                        pos1 = input[i + 1];
                        pos2 = input[i + 2];
                        poso = input[i + 3];

                        // Use position mode or immediate mode
                        result = (pm1 == POSITION ? input[pos1] : pos1)
                               * (pm2 == POSITION ? input[pos2] : pos2);

                        input[poso] = result; // Update input

                        // Move pointer
                        i += 4;
                        break;
                    case 3: // Take input value
                        var inputValue = GetIntInput();

                        pos1 = input[i + 1];
                        input[pos1] = inputValue;

                        // Move pointer
                        i += 2;
                        break;
                    case 4: // Output value 
                        pos1 = input[i + 1];

                        if (pm1 == POSITION)
                            Console.WriteLine(input[pos1]); // Output value at position 1
                        else
                            Console.WriteLine(pos1); // Output immediate value 

                        // Move pointer
                        i += 2;
                        break;
                    case 5: // Jump if true
                        pos1 = input[i + 1];
                        pos2 = input[i + 2];
                        val = pm1 == POSITION ? input[pos1] : pos1;
                        if (val != 0)
                        {
                            // Set instruction pointer
                            i = pm2 == POSITION ? input[pos2] : pos2;
                        }
                        else
                            i += 3;
                        break;
                    case 6: // Jump if false
                        pos1 = input[i + 1];
                        pos2 = input[i + 2];
                        val = pm1 == POSITION ? input[pos1] : pos1;
                        if (val == 0)
                        {
                            // Set instruction pointer
                            i = pm2 == POSITION ? input[pos2] : pos2;
                        }
                        else
                            i += 3;
                        break;
                    case 7: // Less than
                        pos1 = input[i + 1];
                        pos2 = input[i + 2];
                        poso = input[i + 3];

                        // If less than, store 1 in output
                        if ((pm1 == POSITION ? input[pos1] : pos1) < (pm2 == POSITION ? input[pos2] : pos2))
                            input[poso] = 1;
                        else
                            input[poso] = 0;

                        i += 4;
                        break;
                    case 8: // Equals
                        pos1 = input[i + 1];
                        pos2 = input[i + 2];
                        poso = input[i + 3];

                        // If less than, store 1 in output
                        if ((pm1 == POSITION ? input[pos1] : pos1) == (pm2 == POSITION ? input[pos2] : pos2))
                            input[poso] = 1;
                        else
                            input[poso] = 0;

                        i += 4;
                        break;
                    case 99:
                        Console.WriteLine($"Program output = {input[0]}");
                        return input[0];
                    default:
                        Console.WriteLine($"Unrecognized opcode {opCode}, exiting...");
                        Console.Read();
                        return 0;
                }
            }

            return 0;
        }

        private static int GetIntInput()
        {
            bool isValid = false;
            int val = 0;
            do
            {
                Console.Write("Input?");
                isValid = int.TryParse(Console.ReadLine(), out val);

                if (!isValid)
                {
                    Console.WriteLine("Input not a valid integer");
                }
            } while (!isValid);

            return val;
        }
    }
}
