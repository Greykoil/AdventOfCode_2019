using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day4
    {

        public static int Run()
        {
            int count = 0;
            for (int i = 136760; i <= 595730; ++i)
            {
                List<int> digits = new List<int>();
                int current = i;
                while (current > 0)
                {
                    digits.Add(current % 10);
                    current = current / 10;
                }
                digits.Reverse();
                if (IsValid(digits))
                {
                    ++count;
                }

            }

            return count;
        }

        static bool IsValid(List<int> number)
        {
            bool pair = false;
            int last = -1;
            int twoBack = -1;
            int next = number[1];
            for (int i = 0; i < number.Count; ++i)
            {
                int dig = number[i];
                if (dig < last)
                {
                    return false;
                }
                if (dig == last && dig != next && dig != twoBack)
                {
                    pair = true;
                }
                twoBack = last;
                last = dig;
                next = i == 4 || i == 5 ? -1 :number[i + 2];
            }

            return pair;
        }
    }
}
