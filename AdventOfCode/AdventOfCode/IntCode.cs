using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class IntCode
    {

        public static int Run(List<int> codeList, List<int> inputList)
        {
            int location = 0;
            while (codeList[location] != 4)
            {
                (int, List<(bool, int)>) nextCode = ReadOpCode(codeList, location);
                RunOpCode(nextCode.Item1, nextCode.Item2, codeList, ref location, inputList);
            }
            (int, List<(bool, int)>) lastCode = ReadOpCode(codeList, location);
            return RunOpCode(lastCode.Item1, lastCode.Item2, codeList, ref location, inputList);
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

        static int RunOpCode(int codeNumber, List<(bool, int)> locations, List<int> instructionList, ref int location, List<int> inputList)
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
                int input = GetInput(inputList);
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

                return value1;
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

            return 0;
        }

        private static int GetInput(List<int> inputList)
        {
            int value = inputList[0];

            inputList.RemoveAt(0);
            return value;
        }
    }
}
