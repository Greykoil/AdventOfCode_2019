using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode
{
    class Day2
    {
        public static int Run(int first, int second)
        {
            List<int> numbers = AdventUtils.ReadCommaIntList("Inputs/Day2Input.txt");

            numbers[1] = first;
            numbers[2] = second;

            int currentPoint = 0;
            while (true)
            {
                if (numbers[currentPoint] == 99)
                    return numbers[0];

                int instruction = numbers[currentPoint];
                var value1 = numbers[currentPoint + 1];
                var value2 = numbers[currentPoint + 2];
                var position = numbers[currentPoint + 3];

                if (instruction != 1 && instruction != 2)
                {
                    throw new Exception("Uh Oh");
                }
                
                numbers[position] = instruction == 1 ? numbers[value1] + numbers[value2] : numbers[value1] * numbers[value2];

                currentPoint += 4;
            }
        }
    }
}
