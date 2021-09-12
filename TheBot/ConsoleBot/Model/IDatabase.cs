using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleBot
{
    public interface IDatabase<T>
    {
        public void AddDocument(T doc);
        public void RemoveDocument(T doc);
        public void Create();
        public IEnumerable<T> GetAllData();
        public IEnumerable<T> MatchAll(string[] data);
    }
}