using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace AdventOfCode
{
    class Day10
    {

        public static int Run()
        {

            // Is it going to be better to do this as obscure or as able to see?

            // See might be the easier option, we can do width to height ratio, then work out where that would intercept each row/column and 
            // check for an asteroid there?

            // OR we could do it as the blocks? Where we know that whole numbers are required, except that is not true.

            List<(int, int)> asteroids = AdventUtils.ReadAsteroidMap("Inputs/Day10Input.txt");

            (int, int) station = (26, 29);
            //(int, int) station = (11, 13);
            //double angle = Angle((11, 12), station);
            var removedCount = 0;
            while (removedCount < 200)
            {
                var current = asteroids.Where(x => CanSee(x, station, asteroids)).ToList();
                current.Remove(station);
                var sorted = current.OrderBy(x => Angle(x, station)).ToList();
                var item = sorted[199];
                return item.Item1 * 100 + item.Item2;
            }

            bool answer = CanSee((6, 0), (4, 2), asteroids);

            List<(int, (int, int))> seeCount = new List<(int, (int, int))>();
            foreach (var item in asteroids)
            {
                int count = -1;
                foreach (var other in asteroids)
                {
                    if (CanSee(item, other, asteroids))
                    {
                        ++count;
                    }
                }
                seeCount.Add((count, item));
            }

            var location = seeCount.Where(y => y.Item1 == seeCount.Max(x => x.Item1));

            return 0;
        }

        private static double Angle((int, int) current, (int, int) station)
        {

            double xDiff = current.Item1 - station.Item1;
            double yDiff = current.Item2 - station.Item2;

            double theta = Math.Atan2(xDiff, -yDiff);
            double answer = 0;
            if (theta < 0)
            {
                theta = (Math.PI * 2) + theta;
            }

            return theta;

            answer = theta - Math.PI / 2;
            if (answer < 0)
            {
                answer = Math.PI * 2 - answer;
            }

            if (answer >= Math.PI * 2)
            {
                answer -= Math.PI * 2;
            }

            return answer;
         
        }

        public static bool CanSee((int, int) asteroidOne, (int, int) asteroidTwo, List<(int, int)> asteroidMap)
        {
            double TOLERANCE = 1e-10;
            int xDist = asteroidTwo.Item1 - asteroidOne.Item1;
            int yDist = asteroidTwo.Item2 - asteroidOne.Item2;

            // Check all the points along x
            for (int i = 1; i < Math.Abs(xDist); ++i)
            {
                double ytrav = Math.Abs(i * (double)yDist / (double)xDist);
                (double, double) point = (xDist >= 0 ? asteroidOne.Item1 + i : asteroidOne.Item1 - i, yDist >= 0 ? asteroidOne.Item2 + ytrav : asteroidOne.Item2 - ytrav);
                if (asteroidMap.Count(x => Math.Abs(x.Item1 - point.Item1) < TOLERANCE && Math.Abs(x.Item2 - point.Item2) < TOLERANCE) >= 1)
                {
                    return false;
                }
            }

            // Check all the points along y
            for (int i = 1; i < Math.Abs(yDist); ++i)
            {
                double xtrav = Math.Abs(i * (double)xDist / (double)yDist);
                (double, double) point = (xDist >= 0 ? asteroidOne.Item1 + xtrav : asteroidOne.Item1 - xtrav, yDist >= 0 ? asteroidOne.Item2 + i : asteroidOne.Item2 - i);
                if (asteroidMap.Count(x => Math.Abs(x.Item1 - point.Item1) < TOLERANCE && Math.Abs(x.Item2 - point.Item2) < TOLERANCE) >= 1)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
