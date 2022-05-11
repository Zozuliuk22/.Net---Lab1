using System;
using System.Collections.Generic;
using System.Linq;
using BuildingsCollection;
using BuildingsCollection.Enums;

namespace City
{
    public class CityService
    {
        public IEnumerable<Block> GetAllBlocks()
        {
            return from b in DataSeeder.Blocks
                   select b;
        }

        public Dictionary<string, List<House>> GetFullInfo()
        {
            return DataSeeder.Blocks.GroupJoin(DataSeeder.Houses.Join(DataSeeder.HouseToBlocks,
                                                                      h => h.Code,
                                                                      hb => hb.HouseCode,
                                                                      (h, hb) => new
                                                                      {
                                                                          BlockCode = hb.BlockCode,
                                                                          HouseItem = h
                                                                      }),
                                               b => b.Code,
                                               bh => bh.BlockCode,
                                               (b, houses) => new
                                               {
                                                   BlockName = b.Name,
                                                   Houses = houses.Select(h => h.HouseItem).ToList()
                                               })
                                      .ToDictionary(b => b.BlockName, h => h.Houses);
        }

        public double GetCityArea()
        {
            return DataSeeder.Blocks.Aggregate(0d, (sum, b) => sum + b.Area);
        }

        public double GetAreaByOnePerson()
        {
            return DataSeeder.Blocks.Sum(b => b.InhabitantsNumber) / GetCityArea();
        }

        public IEnumerable<IGrouping<ProjectTypes, House>> GetHousesByTypes()
        {
            return from h in DataSeeder.Houses
                   group h by h.ProjectType;
        }

        public IEnumerable<IGrouping<int, House>> GetOneAndNineStoryHouses()
        {
            return DataSeeder.Houses.GroupBy(h => h.FloatsNumber)
                                    .Where(h => h.Key == 1 || h.Key == 9)
                                    .ToList();
        }

        public IEnumerable<House> GetNewHouses()
        {
            return DataSeeder.Houses.OrderByDescending(h => h.CreationDate)
                                    .TakeWhile(h => h.CreationDate.Year > 2015);
        }

        public IEnumerable<House> GetHappyHouses()
        {
            return DataSeeder.Houses.Where(h => h.Code.Contains("7"));
        }

        public IEnumerable<ProjectTypes> GetSpecificTypes(int floats)
        {
            var result = from h in DataSeeder.Houses
                         where h.FloatsNumber >= floats
                         select h.ProjectType;   

            return result.Distinct();
        }

        public string FindAdministration(string houseCode)
        {
            var house = DataSeeder.HouseToBlocks.FirstOrDefault(hb => hb.HouseCode == houseCode);

            if (house is null) return $"No house with {houseCode} code.";

            var block = DataSeeder.Blocks.FirstOrDefault(b => b.Code == house.BlockCode);

            return  block is null ? "Block is unknown." : block.AdministrationAddress;
        }

        public IEnumerable<House> GetTheBiggestBlockInfo()
        {
            if (!DataSeeder.Blocks.Any())
                return Enumerable.Empty<House>();

            var result = from h in DataSeeder.Houses
                         join hb in DataSeeder.HouseToBlocks on h.Code equals hb.HouseCode
                         group h by hb.BlockCode into models
                         select new
                         {
                             BlockCode = models.Key,
                             Houses = models
                         };            

            var biggestBlockCode = (from b in DataSeeder.Blocks
                                orderby b.InhabitantsNumber descending
                                select b)
                               .First().Code;

            return result.First(m => m.BlockCode == biggestBlockCode).Houses.ToList();            
        }

        public IEnumerable<House> GetTopTenOldestHouses()
        {
            var result = from h in DataSeeder.Houses
                         orderby h.CreationDate.Year
                         select h;

            return result.Take(10).ToList();
        }

        public int GetPercentOfHighRiseByBlock(string blockCode)
        {
            var result = from h in DataSeeder.Houses
                         join hb in DataSeeder.HouseToBlocks on h.Code equals hb.HouseCode
                         group h by hb.BlockCode into models
                         select new
                         {
                             BlockCode = models.Key,
                             Houses = models
                         };

            var housesIsHighRise = from h in result.First(m => m.BlockCode == blockCode).Houses
                                   where h.ProjectType == ProjectTypes.HighRise
                                   select h;
            
            return (int)((double)housesIsHighRise.Count() / result.Count() * 100);
        }

        public IEnumerable<Block> GetBlocksWithMultiEntrencesHouses()
        {
            var multiEntrencesHouses = from h in DataSeeder.Houses
                                       where h.EntrencesNumber > 1
                                       select h.Code;

            var result = from b in DataSeeder.Blocks
                         join hb in DataSeeder.HouseToBlocks on b.Code equals hb.BlockCode
                         where multiEntrencesHouses.Contains(hb.HouseCode)
                         select b;

            return result.Distinct();
        }

        public string PlayInBlocks()
        {
            var collection1 = GetRandomHouses(DataSeeder.Houses.Count / 2);
            var collection2 = GetRandomHouses(DataSeeder.Houses.Count / 2);

            var result = collection1.Intersect(collection2).ToList();

            var block = DataSeeder.HouseToBlocks.GroupJoin(result, 
                                                           hb => hb.HouseCode, 
                                                           h => h.Code, 
                                                           (hb, houses) => new
                                                           {
                                                               BlockCode = hb.BlockCode,
                                                               Houses = houses

                                                           })
                                                .OrderByDescending(m => m.Houses.Count())
                                                .FirstOrDefault();

            return block is null ? "No one" : DataSeeder.Blocks.First(b => b.Code == block.BlockCode).ToString();
        }

        private IEnumerable<House> GetRandomHouses(int count)
        {
            var list = new List<House>();

            for (int i = 0; i < count; i++)
                list.Add(DataSeeder.Houses[new Random().Next(DataSeeder.Houses.Count)]);

            return list;
        }
    }
}
