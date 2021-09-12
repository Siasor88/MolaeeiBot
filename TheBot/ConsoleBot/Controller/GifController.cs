using System;
using System.Collections.Generic;

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
            
        }

        public void Remove(Gif gif)
        {
            
        }

        public void Modify(Gif gif)
        {
            
        }
        
        public IEnumerable<Gif> Search(string data, int lenght = 10)
        {
            throw new NotImplementedException();
        }
    }
    
}