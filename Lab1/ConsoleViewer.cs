using System;
using System.Collections.Generic;
using System.Linq;
using BuildingsCollection;
using BuildingsCollection.Enums;

namespace ConsoleInterface
{
    public static class ConsoleViewer
    {
        public static void ShowAllBlocks(IEnumerable<Block> blocks)
        {
            Console.WriteLine("All Blocks:");

            foreach (var block in blocks)
                Console.WriteLine("\t" + block);
        }

        public static void ShowCityFullInfo(Dictionary<string, List<House>> info)
        {
            foreach(var block in info)
            {
                Console.WriteLine("Block: " + block.Key);

                foreach (var house in block.Value)
                    Console.WriteLine("\tHouse: " + house);
            }
        }

        public static void ShowCityArea(double value)
        {
            Console.WriteLine($"\tArea of City is {Math.Round(value, 2)} square km");
        }

        public static void ShowCityAreaByOnePerson(double value)
        {
            Console.WriteLine($"\tArea of City by a person is {Math.Round(value, 2)} square km");
        }

        public static void ShowHousesByTypes(IEnumerable<IGrouping<ProjectTypes, House>> info)
        {
            foreach (var type in info)
            {
                Console.WriteLine("Project type: " + type.Key);

                foreach (var house in type)
                    Console.WriteLine("\t" + house);
            }
        }

        public static void ShowOneAndNineStoryHouses(IEnumerable<IGrouping<int, House>> info)
        {
            foreach (var amount in info)
            {
                Console.WriteLine($"{amount.Key}-story houses: ");

                foreach (var house in amount)
                    Console.WriteLine("\t" + house);
            }
        }

        public static void ShowNewHouses(IEnumerable<House> houses)
        {
            Console.WriteLine("New houses:");
            foreach (var house in houses)
                Console.WriteLine("\t" + house);
        }

        public static void ShowHappyHouses(IEnumerable<House> houses)
        {
            Console.WriteLine("Happy houses:");
            foreach (var house in houses)
                Console.WriteLine("\t" + house);
        }

        public static void ShowSpecificTypes(IEnumerable<ProjectTypes> types)
        {
            Console.WriteLine("Project types which consists of 5 or more floats:");

            foreach(var type in types)
                Console.WriteLine("\t" + type);
        }

        public static void ShowAdministration(string houseCode, string result)
        {
            Console.WriteLine($"\tHouse code: {houseCode} - address of administration: {result}");
        }

        public static void ShowTheBiggestBlockInfo(IEnumerable<House> block)
        {
            Console.WriteLine("The biggest Block consists of:");

            foreach(var house in block)
                Console.WriteLine("\t" + house);
        }

        public static void ShowTopTenOldestHouses(IEnumerable<House> top)
        {
            Console.WriteLine("Top 10 consists of:");

            foreach (var house in top)
                Console.WriteLine("\t" + house);
        }

        public static void ShowPercentOfHighRise(string codeBlock, int percent)
        {
            Console.WriteLine($"\tBlock with {codeBlock} code has {percent}% of HighRise houses.");
        }

        public static void ShowBlocksWithMultiEntrencesHouses(IEnumerable<Block> blocks)
        {
            Console.WriteLine("Blocks with multi-entrences houses:");

            foreach(var block in blocks)
                Console.WriteLine("\t" + block);
        }

        public static void ShowBlocksGameResult(string result)
        {
            Console.WriteLine("\tResult: " + result);
        }
    }
}
