using System;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var min = 158126;
            var max = 624574;

            int passwords = 0;
            for (int i = min; i <= max; i ++)
            {
                if (IsValid(i))
                {
                    passwords++;
                }
            }

            Console.WriteLine($"There are {passwords} valid passwords in Part 1");
            Console.Read();

            passwords = 0;
            for (int i = min; i <= max; i++)
            {
                if (IsValidPart2(i))
                {
                    passwords++;
                }
            }

            Console.WriteLine($"There are {passwords} valid passwords in Part 2");
            Console.Read();
        }

        private static bool IsValid(int pwd)
        {
            var s = pwd.ToString();
            // Has a double digit
            if (s[0] == s[1]
                || s[1] == s[2]
                || s[2] == s[3]
                || s[3] == s[4]
                || s[4] == s[5])
            {
                // Digits only increasing
                if (s[1] >= s[0]
                 && s[2] >= s[1]
                 && s[3] >= s[2]
                 && s[4] >= s[3]
                 && s[5] >= s[4] )
                {
                    return true;
                }
            }

            return false;

        }

        private static bool IsValidPart2(int pwd)
        {
            var s = pwd.ToString();
            // Has a double digit
            if (((s[0] == s[1]) && (s[2] != s[0]) && (s[3] != s[0]) && (s[4] != s[0]) && (s[5] != s[0])) // Pair in 00xxxx
             || ((s[1] == s[2]) && (s[0] != s[1]) && (s[3] != s[1]) && (s[4] != s[1]) && (s[5] != s[1])) // Pair in x00xxx
             || ((s[2] == s[3]) && (s[0] != s[2]) && (s[1] != s[2]) && (s[4] != s[2]) && (s[5] != s[2])) // Pair in xx00xx
             || ((s[3] == s[4]) && (s[0] != s[3]) && (s[1] != s[3]) && (s[2] != s[3]) && (s[5] != s[3])) // Pair in xxx00x
             || ((s[4] == s[5]) && (s[0] != s[4]) && (s[1] != s[4]) && (s[2] != s[4]) && (s[3] != s[4])))// Pair in xxxx00
            {
                // Digits only increasing
                if (s[1] >= s[0]
                 && s[2] >= s[1]
                 && s[3] >= s[2]
                 && s[4] >= s[3]
                 && s[5] >= s[4])
                {
                    return true;
                }
            }

            return false;

        }
    }
}
