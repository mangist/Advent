using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read input file
            var wires = File.ReadAllLines("input.txt");

            // Read the wire segments
            var w1 = wires[0].Split(',');
            var w2 = wires[1].Split(',');

            // This board holds all the wire segments
            var wire1 = new Dictionary<ValueTuple<int, int>, int>();
            var wire2 = new Dictionary<ValueTuple<int, int>, int>();

            ParseWire(w1, wire1);
            ParseWire(w2, wire2);

            // Find all intersections
            var intersections = new Dictionary<ValueTuple<int, int>, int>();
            foreach (var pos in wire1.Keys)
            {
                if (wire2.ContainsKey(pos))
                {
                    // Found an intersection
                    var steps = wire1[pos] + wire2[pos];
                    intersections.Add(pos, steps);
                }
            }
            
            // Now find the closest Manhattan Distance
            var closest = 0;
            foreach (var i in intersections.Keys)
            {
                var manhattan = Math.Abs(i.Item1) + Math.Abs(i.Item2);

                if (manhattan < closest || closest == 0)
                {
                    closest = manhattan;
                }
            }

            // Now calculate the Manhattan Distance (board is fully populated)
            Console.WriteLine($"Manhattan distance of closest intersection is {closest}");

            // Part 2
            // Find the intersection with the minimum number of steps
            closest = 0;
            foreach (var i in intersections.Keys)
            {
                var steps = intersections[i];
                if (steps < closest || closest == 0)
                {
                    closest = steps;
                }
            }

            Console.WriteLine($"Fewest combined steps to reach an intersection is {closest}");

        }

        // Read all wire segments and populate the board segments
        private static void ParseWire(string[] segments, Dictionary<ValueTuple<int, int>, int> wire)
        {
            // Current position on the board
            var x = 0;
            var y = 0;
            var step = 1;

            foreach (var s in segments)
            {
                // Read direction
                var direction = s[0];
                var length = int.Parse(s.Substring(1));

                for (int i = 0; i < length; i++)
                {
                    switch (direction)
                    {
                        case 'U': y++; break;
                        case 'D': y--; break;
                        case 'L': x--; break;
                        case 'R': x++; break;
                        default:
                            throw new NotSupportedException("Unknown direction");
                    }

                    // Add this position to the board (increment per wire crossing)
                    var pos = new ValueTuple<int, int>(x, y);

                    if (wire.ContainsKey(pos))
                        wire[pos] = step; // Update step
                    else
                        wire.Add(pos, step);
                    
                    // Increment step
                    step++;
                }
            }
        }
    }
}
