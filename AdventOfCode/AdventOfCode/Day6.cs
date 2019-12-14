using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode
{
    class Planet
    {
        public string Name { get; set; }
        public Planet Parent { get; set; }

        public List<Planet> Children { get; set; }

        public int SantaCount { get; set; } = -1;
    }

    class Day6
    {
        public static int Run()
        {
            var lines = File.ReadAllLines("Inputs/Day6Input.txt");

            Dictionary<string, Planet> planets = new Dictionary<string, Planet>();

            foreach (var item in lines)
            {
                var bits = item.Split(')');
                
                var parent = bits[0];
                var child = bits[1];
                if (!planets.ContainsKey(parent))
                {
                    planets.Add(parent, new Planet() { Name = parent, Children = new List<Planet>() });
                }
                if (!planets.ContainsKey(child))
                {
                    planets.Add(child, new Planet() { Name = child, Children = new List<Planet>() });
                }

                planets[parent].Children.Add(planets[child]);
                planets[child].Parent = planets[parent];
            }


            planets["SAN"].SantaCount = 0;
            AssignSantaCount(planets["SAN"]);

            return planets["YOU"].SantaCount - 2;
        }

        private static void AssignSantaCount(Planet planet)
        {
            if (planet.SantaCount == -1)
            {
                throw new ArgumentException("Wrong");
            }
            if (planet.Parent != null && planet.Parent.SantaCount == -1)
            {
                planet.Parent.SantaCount = planet.SantaCount + 1;
                AssignSantaCount(planet.Parent);
            }

            foreach (var child in planet.Children.Where(x => x.SantaCount == -1))
            {
                child.SantaCount = planet.SantaCount + 1;
                AssignSantaCount(child);
            }
        }
    }
}
