using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        class Planet
        {
            public Planet(string code, Planet parent, int orbitLevel)
            {
                this.Parent = parent;
                this.Code = code;
                this.OrbitLevel = orbitLevel;
                PlanetsInOrbit = new List<Planet>();
            }

            public Planet Parent { get; set; }
            public int OrbitLevel { get; set; }
            public string Code { get; set; }
            public List<Planet> PlanetsInOrbit { get; set; }
        }

        static List<Planet> planets = new List<Planet>();
        static List<Tuple<string, string>> orbits = new List<Tuple<string, string>>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            
            foreach (var l in lines)
            {
                // Parse
                var p1 = l.Substring(0, 3);
                var p2 = l.Substring(4);

                orbits.Add(new Tuple<string, string>(p1, p2));
            }

            // Find COM, then work recursively from there
            var com = orbits.Single(o => o.Item1 == "COM");
            if (com == null)
                throw new InvalidOperationException("Map is corrupt, no COM");

            var comPlanet = new Planet(com.Item1, null, 0);
            planets.Add(comPlanet);
            MapPlanet(comPlanet, com.Item2);

            // Now we should have a full populated tree of planets and orbits
            Console.WriteLine($"Total {planets.Count} planets");

            // Count orbits
            var totalOrbits = planets.Sum(p => p.OrbitLevel);
            Console.WriteLine($"Total indirect and direct orbits {totalOrbits}");

            // Part 2
            var you = planets.Single(p => p.Code == "YOU");
            var yousPlanet = you.Parent;
            var santa = planets.Single(p => p.Code == "SAN");
            var santasPlanet = santa.Parent;

            // Find the orbit level where our path intersects
            var youCompleteOrbit = new List<Planet>();
            GetFullOrbit(youCompleteOrbit, yousPlanet);

            var santasCompleteOrbit = new List<Planet>();
            GetFullOrbit(santasCompleteOrbit, santasPlanet);

            var intersectingPlanet = youCompleteOrbit.Intersect(santasCompleteOrbit).FirstOrDefault();

            var distance = (yousPlanet.OrbitLevel - intersectingPlanet.OrbitLevel)
                         + (santasPlanet.OrbitLevel - intersectingPlanet.OrbitLevel);

            // Now get from yousPlanet to santasPlanet
            Console.WriteLine($"Orbital transfers required to move from YOU to SAN is {distance}");
            Console.Read();
        }

        // Recursively add 
        static void GetFullOrbit(List<Planet> fullOrbit, Planet p)
        {
            fullOrbit.Add(p);

            if (p.Parent != null)
            {
                GetFullOrbit(fullOrbit, p.Parent);
            }
        }

        static Planet FindPlanet(string code)
        {
            foreach (var p in planets)
            {
                if (p.Code == code)
                    return p;
            }

            return null; // Not in list yet
        }

        static void MapPlanet(Planet parent, string code)
        {
            var planet = FindPlanet(code);
            if (planet == null)
            {
                // Add new planet
                planet = new Planet(code, parent, parent.OrbitLevel + 1);
                planets.Add(planet);
            }

            // Add orbit
            parent.PlanetsInOrbit.Add(planet);

            // Find all orbits for this planet
            foreach (var o in orbits.Where(o => o.Item1 == code))
            {
                // Recursively traverse the galactic map
                MapPlanet(planet, o.Item2);
            }
        }
    }
}
