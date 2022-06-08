using System;
using System.Collections.Generic;
using System.Linq;
using BuildingsCollection;
using BuildingsCollection.Enums;
using Application.Properties;

namespace Application
{
    public static class ConsoleViewer
    {
        public static void ShowAllBlocks(IEnumerable<Block> blocks)
        {
            if (blocks != null && blocks.Any())
            {
                Console.WriteLine("All Blocks:");

                foreach (var block in blocks)
                    Console.WriteLine($"\t{block}");
            } 
            else
                Console.WriteLine("Result: No one.");
        }

        public static void ShowCityFullInfo(Dictionary<string, List<House>> info)
        {
            if(info != null && info.Any())
            {
                foreach (var block in info)
                {
                    Console.WriteLine($"Block: {block.Key}");

                    foreach (var house in block.Value)
                        Console.WriteLine($"\tHouse: {house}");
                }
            }
            else
                Console.WriteLine("Result: No one.");
        }

        public static void ShowCityArea(double value)
        {
            Console.WriteLine($"\tArea of City is {Math.Round(value, 2)} square km");
        }

        public static void ShowCityAreaByOnePerson(double value)
        {
            Console.WriteLine($"\tArea of City by a person " +
                              $"is {Math.Round(value, 2)} square m");
        }

        public static void ShowHousesByTypes(Dictionary<ProjectTypes, IEnumerable<House>> info)
        {
            if(info != null && info.Any())
            {
                foreach (var type in info)
                {
                    Console.WriteLine($"Project type: {type.Key}");

                    foreach (var house in type.Value)
                        Console.WriteLine($"\t{house}");
                }
            }
            else
                Console.WriteLine("Result: No one.");
        }

        public static void ShowOneAndNineStoryHouses(Dictionary<int, IEnumerable<House>> info)
        {
            if(info != null && info.Any())
            {
                foreach (var amount in info)
                {
                    Console.WriteLine($"{amount.Key}-story houses: ");

                    foreach (var house in amount.Value)
                        Console.WriteLine($"\t{house}");
                }
            }
            else
                Console.WriteLine("Result: No one.");
        }

        public static void ShowNewHouses(IEnumerable<House> houses)
        {
            if(houses != null && houses.Any())
            {
                Console.WriteLine("New houses:");
                foreach (var house in houses)
                    Console.WriteLine($"\t{house}");
            }   
            else
                Console.WriteLine("Result: No one.");
        }

        public static void ShowHappyHouses(IEnumerable<House> houses)
        {
            if(houses!= null && houses.Any())
            {
                Console.WriteLine("Happy houses:");
                foreach (var house in houses)
                    Console.WriteLine($"\t{house}");
            }
            else
                Console.WriteLine("Result: No one.");
        }

        public static void ShowSpecificTypes(IEnumerable<ProjectTypes> types)
        {
            if(types != null && types.Any())
            {
                Console.WriteLine(ConsoleTexts.SpecificTypesLabel);

                foreach (var type in types)
                    Console.WriteLine($"\t{type}");
            }
            else
                Console.WriteLine("Result: No one.");
        }

        public static void ShowAdministrationAddress(string houseCode, string result)
        {
            Console.WriteLine($"\tHouse code: {houseCode} - " +
                              $"address of administration: {result}");
        }

        public static void ShowTheBiggestBlockInfo(IEnumerable<House> block)
        {
            if(block!= null && block.Any())
            {
                Console.WriteLine(ConsoleTexts.TheBiggestBlockLabel);

                foreach (var house in block)
                    Console.WriteLine($"\t{house}");
            }
            else
                Console.WriteLine("Result: No one.");
        }

        public static void ShowTopTenOldestHouses(IEnumerable<House> top)
        {
            if(top!= null && top.Any())
            {
                Console.WriteLine("Top 10 consists of:");

                foreach (var house in top)
                    Console.WriteLine($"\t{house}");
            }
            else
                Console.WriteLine("Result: No one.");
        }

        public static void ShowPercentOfHighRise(string codeBlock, int percent)
        {
            Console.WriteLine($"\tBlock with {codeBlock} code " +
                              $"has {percent}% of HighRise houses.");
        }

        public static void ShowBlocksWithMultiEntrencesHouses(IEnumerable<Block> blocks)
        {
            if(blocks != null && blocks.Any())
            {
                Console.WriteLine(ConsoleTexts.MultiEntrencesHousesLabel);

                foreach (var block in blocks)
                    Console.WriteLine($"\t{block}");
            }            
        }

        public static void ShowBlocksGameResult(Block block)
        {
            if(block is null)
                Console.WriteLine($"\tResult: No one");
            else
                Console.WriteLine($"\tResult: {block}");
        }
    }
}
