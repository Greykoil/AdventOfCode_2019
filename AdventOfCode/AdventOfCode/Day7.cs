using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AdventOfCode
{
    class Day7
    {

        static public int Run()
        {
            int maxValue = int.MinValue;

            List<int> instructions = AdventUtils.ReadCommaIntList("Inputs/Day7Input.txt");
            List<int> opCodes = new List<int>() { 0, 1, 2, 3, 4 };

            var things = opCodes.Permute();

            foreach (var foo in things)
            {
                var thing = foo.GetEnumerator();
                thing.MoveNext();
                int output1 = IntCode.Run(new List<int>(instructions), new List<int>() { thing.Current, 0 });
                thing.MoveNext();
                int output2 = IntCode.Run(new List<int>(instructions), new List<int>() { thing.Current, output1 });
                thing.MoveNext();

                int output3 = IntCode.Run(new List<int>(instructions), new List<int>() { thing.Current, output2 });
                thing.MoveNext();

                int output4 = IntCode.Run(new List<int>(instructions), new List<int>() { thing.Current, output3 });
                thing.MoveNext();

                int output5 = IntCode.Run(new List<int>(instructions), new List<int>() { thing.Current, output4 });
                maxValue = Math.Max(maxValue, output5);
            }

            return maxValue;
        }
    }
}
