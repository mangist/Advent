using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            const char COMMA = ',';

            // Read input ints and parse
            var originalInput = File.ReadAllText("input.txt").Split(COMMA)
                            .Select(i => int.Parse(i))
                            .ToList();

            var part1 = new List<int>(originalInput);

            // Restore 1202 program alarm
            part1[1] = 12;
            part1[2] = 2;

            // Part 1
            RunIntcode(part1);

            Console.WriteLine("Press any key to start part 2");
            Console.Read();

            // Part 2
            bool done = false;
            for (int n = 0; n <= 99; n++)
            {
                for (int v = 0; v <= 99; v ++)
                {
                    var input = new List<int>(originalInput);
                    input[1] = n;
                    input[2] = v;

                    var output = RunIntcode(input);

                    Console.WriteLine($"Ran intcode for n={n} v={v} output={output}");

                    if (output == 19690720)
                    {
                        Console.WriteLine($"100 x noun + verb = {100 * n + v}");
                        done = true;
                        break;
                    }
                }

                if (done)
                    break;
            }

            Console.Read();
        }

        private static int RunIntcode(List<int> input)
        {
            // Create output
            int[] output = new int[input.Count];

            // Process each opcode
            for (int i = 0; i < input.Count;)
            {
                var opCode = input[i];
                int pos1, pos2, poso, result;

                //Console.WriteLine($"{input[i]}\t{input[i + 1]}\t{input[i + 2]}\t{input[i + 3]}");

                switch (opCode)
                {
                    case 1: // Add
                        pos1 = input[i + 1];
                        pos2 = input[i + 2];
                        poso = input[i + 3];
                        result = input[pos1] + input[pos2];

                        output[i] = opCode;
                        output[i + 1] = pos1;
                        output[i + 2] = pos2;

                        input[poso] = result; // Update input
                        output[poso] = result; // Store output

                        // Move pointer
                        i += 4;
                        break;
                    case 2: // Multiply
                        pos1 = input[i + 1];
                        pos2 = input[i + 2];
                        poso = input[i + 3];
                        result = input[pos1] * input[pos2];

                        output[i] = opCode;
                        output[i + 1] = pos1;
                        output[i + 2] = pos2;

                        input[poso] = result; // Update input
                        output[poso] = result; // Store output

                        // Move pointer
                        i += 4;
                        break;
                    case 99:
                        Console.WriteLine($"Program output = {output[0]}");
                        return output[0];
                    default:
                        Console.WriteLine($"Unrecognized opcode {opCode}, exiting...");
                        Console.Read();
                        return 0;
                }
            }

            return 0;
        }
    }
}
