using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    class Day5
    {
        public static int Run()
        {

            List<int> numbers = AdventUtils.ReadCommaIntList("Inputs/Day5Input.txt");

            int location = 0;
            while (numbers[location] != 99)
            {
                (int, List<(bool, int)> ) nextCode = ReadOpCode(numbers, location);
                RunOpCode(nextCode.Item1, nextCode.Item2, numbers, ref location);
            }

            return 0;
        }

        private static (int, List<(bool, int)>) ReadOpCode(List<int> numbers, int location)
        {
            int partOne = numbers[location];
            int instructionCode = partOne % 100;

            if (instructionCode == 1 || instructionCode == 2 || instructionCode == 7 || instructionCode == 8)
            {
                bool firstMode = ((partOne / 100) % 10 == 0);
                bool secondMode = ((partOne / 1000) % 10 == 0);
                return (instructionCode, new List<(bool, int)>()
                {
                    (firstMode, numbers[location + 1]),
                    (secondMode, numbers[location + 2]),
                    (false, numbers[location + 3]),
                });
            }

            if (instructionCode == 3)
            {
                return (3, new List<(bool, int)>() { (false, numbers[location + 1]) });
            }

            if (instructionCode == 4)
            {
                return (4, new List<(bool, int)>() { (true, numbers[location + 1]) });
            }

            if (instructionCode == 5 || instructionCode == 6)
            {
                bool firstMode = ((partOne / 100) % 10 == 0);
                bool secondMode = ((partOne / 1000) % 10 == 0);
                return (instructionCode, new List<(bool, int)>()
                {
                    (firstMode, numbers[location + 1]),
                    (secondMode, numbers[location + 2])
                });
            }


            throw new ArgumentException("Arg exception");
        }

        static void RunOpCode(int codeNumber, List<(bool, int)> locations, List<int> instructionList, ref int location)
        {
            if (codeNumber == 1 || codeNumber == 2)
            {
                if (locations.Count != 3)
                {
                    throw new ArgumentException("Invalid number of arg for simple instruction");
                }

                int value1 = locations[0].Item1 ? instructionList[locations[0].Item2] : locations[0].Item2;
                int value2 = locations[1].Item1 ? instructionList[locations[1].Item2] : locations[1].Item2;
                var result = (codeNumber == 1 ? value1 + value2 : value1 * value2);

                instructionList[locations[2].Item2] = result;

                location += 4;
            }

            else if (codeNumber == 3)
            {
                if (locations.Count != 1 && locations[0].Item1 != true)
                {
                    throw new ArgumentException("Invalid number of arg for simple instruction");
                }
                int input = GetInput();
                instructionList[locations[0].Item2] = input;

                location += 2;
            }
            else if (codeNumber == 4)
            {
                if (locations.Count != 1)
                {
                    throw new ArgumentException("Invalid number of arg for simple instruction");
                }
                int value1 = locations[0].Item1 ? instructionList[locations[0].Item2] : locations[0].Item2;
                Console.WriteLine(value1);

                location += 2;
            }

            else if (codeNumber == 5 || codeNumber == 6)
            {
                int value1 = locations[0].Item1 ? instructionList[locations[0].Item2] : locations[0].Item2;
                int value2 = locations[1].Item1 ? instructionList[locations[1].Item2] : locations[1].Item2;

                bool jump = codeNumber == 5 ? value1 != 0 : value1 == 0;

                location = jump ? value2 : location + 3;
            }

            else if (codeNumber == 7 || codeNumber == 8)
            {
                int value1 = locations[0].Item1 ? instructionList[locations[0].Item2] : locations[0].Item2;
                int value2 = locations[1].Item1 ? instructionList[locations[1].Item2] : locations[1].Item2;

                bool storeOne = codeNumber == 7 ? value1 < value2 : value1 == value2;

                instructionList[locations[2].Item2] = storeOne ? 1 : 0;
                location += 4;
            }
        }

        private static int GetInput()
        {
            return 5;
        }
    }
}
