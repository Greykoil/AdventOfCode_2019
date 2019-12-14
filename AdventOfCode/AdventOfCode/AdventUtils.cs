using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    static class AdventUtils
    {

        public static List<int> ReadCommaIntList(string filename)
        {
            string code = File.ReadAllText(filename);

            var split = code.Split(",");
            var values = new List<int>();
            foreach (var num in split)
            {
                values.Add(Int32.Parse(num));
            }
            return values;
        }


        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                yield break;
            }

            var list = sequence.ToList();

            if (!list.Any())
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var startingElementIndex = 0;

                foreach (var startingElement in list)
                {
                    var index = startingElementIndex;
                    var remainingItems = list.Where((e, i) => i != index);

                    foreach (var permutationOfRemainder in remainingItems.Permute())
                    {
                        yield return startingElement.Concat(permutationOfRemainder);
                    }

                    startingElementIndex++;
                }
            }
        }

        private static IEnumerable<T> Concat<T>(this T firstElement, IEnumerable<T> secondSequence)
        {
            yield return firstElement;
            if (secondSequence == null)
            {
                yield break;
            }

            foreach (var item in secondSequence)
            {
                yield return item;
            }
        }


        public static List<int> ReadIntList(string input)
        {

            string code = File.ReadAllText(input);
            List<int> values = new List<int>();
            foreach (var character in code)
            {
                values.Add(int.Parse(character.ToString()));
            }

            return values;
        }

        public static List<(int, int)> ReadAsteroidMap(string input)
        {

            string[] lines = File.ReadAllLines(input);

            List<(int, int)> commets = new List<(int, int)>();

            for (int i = 0; i < lines.Length; ++i)
            {
                for (int j = 0; j < lines[i].Length; ++j)
                {
                    if (lines[i][j] == '#')
                    {
                        commets.Add((j, i));
                    }
                }
            }

            return commets;
        }
    }


}
