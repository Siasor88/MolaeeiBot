using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleBot
{
    public class GifController
    {
        private IDatabase<Gif> _database;

        public GifController(IDatabase<Gif> database)
        {
            _database = database;
        }

        public void Add(Gif gif)
        {
            _database.AddDocument(gif);
        }

        public void Remove(Gif gif)
        {
         _database.RemoveDocument(gif);   
        }
        
        public void Modify(Gif gif)
        {
            _database.Update(gif);
        }
        
        public IEnumerable<Gif> Search(string data)
        {
            string[] parsedData = ParseData(data);
            var  a =_database.MatchAll(parsedData);
            return a;
        }

        public string[] ParseData(string data)
        {
            var parsedData = data.Trim().Split(" ").Where(s => s != "").ToList();
            var result = CreateNewListFromTheExistingListWithAppendingNeighbors(parsedData);
            return result.ToArray();
        }

        private static List<string> CreateNewListFromTheExistingListWithAppendingNeighbors(List<string> parsedData)
        {
            List<string> result = new();
            for (int i = 0; i < parsedData.Count; i++)
            {
                result.Add(parsedData[i]);
                if (i != parsedData.Count - 1)
                {
                    result.Add(parsedData[i] + parsedData[i + 1]);
                }
            }
            return result;
        }
    }
    
}