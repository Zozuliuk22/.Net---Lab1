using System;
using City;
using Application;
using Application.Properties;

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
                    case 1:
                        var result1 = city.GetAllBlocks();
                        ConsoleViewer.ShowAllBlocks(result1);
                        break;
                    case 2:
                        var result2 = city.GetFullInfo();
                        ConsoleViewer.ShowCityFullInfo(result2);
                        break;
                    case 3:
                        var result3 = city.GetCityArea();
                        ConsoleViewer.ShowCityArea(result3);
                        break;
                    case 4:
                        var result4 = city.GetAreaByOnePerson();
                        ConsoleViewer.ShowCityAreaByOnePerson(result4);
                        break;
                    case 5:
                        var result5 = city.GetHousesByTypes();
                        ConsoleViewer.ShowHousesByTypes(result5);
                        break;
                    case 6:
                        var result6 = city.GetOneAndNineStoryHouses();
                        ConsoleViewer.ShowOneAndNineStoryHouses(result6);
                        break;
                    case 7:
                        var result7 = city.GetNewHouses();
                        ConsoleViewer.ShowNewHouses(result7);
                        break;
                    case 8:
                        var result8 = city.GetHappyHouses();
                        ConsoleViewer.ShowHappyHouses(result8);
                        break;
                    case 9:
                        var result9 = city.GetSpecificTypes(5);
                        ConsoleViewer.ShowSpecificTypes(result9);
                        break;
                    case 10:
                        var index = new Random().Next(DataContext.Houses.Count);
                        var codeHouse = DataContext.Houses[index].Code;
                        var result10 = city.FindAdministrationAddress(codeHouse);
                        ConsoleViewer.ShowAdministrationAddress(codeHouse, result10);
                        break;
                    case 11:
                        var result11 = city.GetTheBiggestBlockInfo();
                        ConsoleViewer.ShowTheBiggestBlockInfo(result11);
                        break;
                    case 12:
                        var result12 = city.GetTopTenOldestHouses();
                        ConsoleViewer.ShowTopTenOldestHouses(result12);
                        break;
                    case 13:
                        index = new Random().Next(DataContext.Blocks.Count);
                        var codeBlock = DataContext.Blocks[index].Code;
                        var result13 = city.GetPercentOfHighRiseByBlock(codeBlock);
                        ConsoleViewer.ShowPercentOfHighRise(codeBlock, result13);
                        break;
                    case 14:
                        var result14 = city.GetBlocksWithMultiEntrencesHouses();
                        ConsoleViewer.ShowBlocksWithMultiEntrencesHouses(result14);
                        break;
                    case 15:
                        var result15 = city.PlayInBlocks();
                        ConsoleViewer.ShowBlocksGameResult(result15);
                        break;                    
                }

                Console.Write("\nEnter your choice: ");
            }       
        }
    }
}
