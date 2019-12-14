using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day8
    {
        public static int Run()
        {
            List <int> input = AdventUtils.ReadIntList("Inputs/Day8Input.txt");

            List<List<List<int>>> layers = new List<List<List<int>>>();

            // 25 wide by 6 tall

            int layersCount = input.Count / (25 * 6);
            int count = 0;
            for (int i = 0; i < layersCount; ++i)
            {
                List<List<int>> currentLayer = new List<List<int>>();
                for (int j = 0; j < 6; ++j)
                {
                    List<int> currentRow = new List<int>();
                    for (int k = 0; k < 25; ++k)
                    {
                        currentRow.Add(input[count]);
                        ++count;
                    }
                    currentLayer.Add(currentRow);
                }
                layers.Add(currentLayer);
            }
            List<List<int> > finalPicture = new List<List<int>>();

            for (int i = 0; i < 6; ++i)
            {
                List<int> values = new List<int>();
                for (int j = 0; j < 25; ++j)
                {
                    for (int k = 0; k < layers.Count; ++k)
                    {
                        if (layers[k][i][j] != 2)
                        {
                            values.Add(layers[k][i][j]);
                            break;
                        }

                        if (k == layersCount - 1)
                        {
                            throw new ArgumentException("uh oh");
                        }
                    }
                }
                finalPicture.Add(values);
            }


            foreach (var row in finalPicture)
            {
                foreach (var value in row)
                {
                    if (value == 1)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
            }

            return 0;
        }
    }
}
