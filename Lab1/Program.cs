using System;
using ConsoleInterface;
using ConsoleInterface.Properties;
using City;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ConsoleTexts.Title);
            Console.WriteLine(ConsoleTexts.Menu);

            Console.Write("\nEnter your choice: ");            
            int choice = 0;

            CityService city = new CityService();

            while (Int32.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1: ConsoleViewer.ShowAllBlocks(city.GetAllBlocks());
                        break;
                    case 2: ConsoleViewer.ShowCityFullInfo(city.GetFullInfo());
                        break;
                    case 3: ConsoleViewer.ShowCityArea(city.GetCityArea());
                        break;
                    case 4: ConsoleViewer.ShowCityAreaByOnePerson(city.GetAreaByOnePerson());
                        break;
                    case 5: ConsoleViewer.ShowHousesByTypes(city.GetHousesByTypes());
                        break;
                    case 6: ConsoleViewer.ShowOneAndNineStoryHouses(city.GetOneAndNineStoryHouses());
                        break;
                    case 7: ConsoleViewer.ShowNewHouses(city.GetNewHouses());
                        break;
                    case 8: ConsoleViewer.ShowHappyHouses(city.GetHappyHouses());
                        break;
                    case 9: ConsoleViewer.ShowSpecificTypes(city.GetSpecificTypes(5));
                        break;
                    case 10:
                        var codeHouse = DataSeeder.Houses[new Random().Next(DataSeeder.Houses.Count)].Code;
                        ConsoleViewer.ShowAdministration(codeHouse, city.FindAdministration(codeHouse));
                        break;
                    case 11: ConsoleViewer.ShowTheBiggestBlockInfo(city.GetTheBiggestBlockInfo());
                        break;
                    case 12: ConsoleViewer.ShowTopTenOldestHouses(city.GetTopTenOldestHouses());
                        break;
                    case 13:
                        var codeBlock = DataSeeder.Blocks[new Random().Next(DataSeeder.Blocks.Count)].Code;
                        ConsoleViewer.ShowPercentOfHighRise(codeBlock, city.GetPercentOfHighRiseByBlock(codeBlock));
                        break;
                    case 14: ConsoleViewer.ShowBlocksWithMultiEntrencesHouses(city.GetBlocksWithMultiEntrencesHouses());
                        break;
                    case 15: ConsoleViewer.ShowBlocksGameResult(city.PlayInBlocks());
                        break;                    
                }

                Console.Write("\nEnter your choice: ");
            }       
        }
    }
}
