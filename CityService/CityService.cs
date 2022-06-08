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
            return from b in DataContext.Blocks
                   select b;
        }

        public Dictionary<string, List<House>> GetFullInfo()
        {
            return DataContext.Blocks
                              .GroupJoin(DataContext.Houses
                                                    .Join(DataContext.HouseToBlocks,
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
                                             Houses = houses.Select(h => h.HouseItem)
                                                            .ToList()
                                         })
                              .ToDictionary(b => b.BlockName, h => h.Houses);
        }

        public double GetCityArea()
        {
            return DataContext.Blocks.Aggregate(0d, (sum, b) => sum + b.Area);
        }

        public double GetAreaByOnePerson()
        {
            return DataContext.Blocks.Sum(b => b.InhabitantsNumber) / GetCityArea();
        }

        //IEnumerable<IGrouping<ProjectTypes, House>> -> Dictionary<ProjectTypes, IEnumerable<House>>
        public Dictionary<ProjectTypes, IEnumerable<House>> GetHousesByTypes()
        {
            var query = from h in DataContext.Houses
                        group h by h.ProjectType;

            return query.ToDictionary(k => k.Key, v => v.Select(h => h));
        }

        //IEnumerable<IGrouping<int, House>> -> Dictionary<int, IEnumerable<House>>
        public Dictionary<int, IEnumerable<House>> GetOneAndNineStoryHouses()
        {
            return DataContext.Houses.GroupBy(h => h.FloatsNumber)
                                     .Where(h => h.Key == 1 || h.Key == 9)
                                     .ToDictionary(k => k.Key, v => v.Select(h => h));
        }

        //TakeWhile() -> Where()
        public IEnumerable<House> GetNewHouses()
        {
            return DataContext.Houses.OrderByDescending(h => h.CreationDate)
                                     .Where(h => h.CreationDate.Year > 2015);
        }

        public IEnumerable<House> GetHappyHouses()
        {
            return DataContext.Houses.Where(h => h.Code.Contains("7"));
        }

        public IEnumerable<ProjectTypes> GetSpecificTypes(int floats)
        {
            var result = from h in DataContext.Houses
                         where h.FloatsNumber >= floats
                         select h.ProjectType;   

            return result.Distinct();
        }

        public string FindAdministrationAddress(string houseCode)
        {
            var house = DataContext.HouseToBlocks.FirstOrDefault(hb => hb.HouseCode == houseCode);

            if (house is null) return $"No house with {houseCode} code.";

            var block = DataContext.Blocks.FirstOrDefault(b => b.Code == house.BlockCode);

            return  block is null ? "Block is unknown." : block.AdministrationAddress;
        }

        //First() -> FirstOrDefault()
        public IEnumerable<House> GetTheBiggestBlockInfo()
        {
            if (!DataContext.Blocks.Any()) return Enumerable.Empty<House>();

            var blockCodeAndHouses = from h in DataContext.Houses
                                     join hb in DataContext.HouseToBlocks 
                                             on h.Code equals hb.HouseCode
                                     group h by hb.BlockCode into models
                                     select new
                                     {
                                         BlockCode = models.Key,
                                         Houses = models
                                     };            

            var biggestBlock = (from b in DataContext.Blocks
                                orderby b.InhabitantsNumber descending
                                select b)
                               .FirstOrDefault();

            if(biggestBlock is null) return Enumerable.Empty<House>();

            var result = blockCodeAndHouses
                        .FirstOrDefault(m => m.BlockCode == biggestBlock.Code);

            if(result is null) return Enumerable.Empty<House>();

            return result.Houses.ToList();
        }

        public IEnumerable<House> GetTopTenOldestHouses()
        {
            var result = from h in DataContext.Houses
                         orderby h.CreationDate.Year
                         select h;

            return result.Take(10).ToList();
        }
        
        //First() -> FirstOrDefault()
        public int GetPercentOfHighRiseByBlock(string blockCode)
        {
            var blockCodeAndHouses = from h in DataContext.Houses
                                     join hb in DataContext.HouseToBlocks
                                             on h.Code equals hb.HouseCode
                                     group h by hb.BlockCode into models
                                     select new
                                     {
                                         BlockCode = models.Key,
                                         Houses = models
                                     };

            var result = blockCodeAndHouses.FirstOrDefault(m => m.BlockCode == blockCode);

            if(result is null) return 0;

            var housesIsHighRise = from h in result.Houses
                                   where h.ProjectType == ProjectTypes.HighRise
                                   select h;

            var part = (double)housesIsHighRise.Count() / blockCodeAndHouses.Count();
            return (int)(part * 100);
        }
        
        //Two queries in one
        public IEnumerable<Block> GetBlocksWithMultiEntrencesHouses()
        {
            var result = from b in DataContext.Blocks
                         join hb in DataContext.HouseToBlocks on b.Code equals hb.BlockCode
                         where (from h in DataContext.Houses
                                where h.EntrencesNumber > 1
                                select h.Code)
                               .Contains(hb.HouseCode)
                         select b;

            return result.Distinct();
        }

        //Return type - Block
        public Block PlayInBlocks()
        {
            var collection1 = GetRandomHouses(DataContext.Houses.Count / 2);
            var collection2 = GetRandomHouses(DataContext.Houses.Count / 2);

            var result = collection1.Intersect(collection2).ToList();

            var block = DataContext.HouseToBlocks
                                   .GroupJoin(result, 
                                              hb => hb.HouseCode, 
                                              h => h.Code, 
                                              (hb, houses) => new
                                              {
                                                  BlockCode = hb.BlockCode,
                                                  Houses = houses

                                              })
                                   .OrderByDescending(m => m.Houses.Count())
                                   .FirstOrDefault();

            if(block is null) return null;

            return DataContext.Blocks.First(b => b.Code == block.BlockCode);
        }

        private IEnumerable<House> GetRandomHouses(int count)
        {
            var list = new List<House>();

            for (int i = 0; i < count; i++)
            {
                var index = new Random().Next(DataContext.Houses.Count);
                list.Add(DataContext.Houses[index]);
            }               

            return list;
        }
    }
}
