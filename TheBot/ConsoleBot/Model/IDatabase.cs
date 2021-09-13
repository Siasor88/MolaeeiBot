using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleBot
{
    public interface IDatabase<T>
    {
        public void AddDocument(T doc);
        public void RemoveDocument(T doc);
        public void ConfigureDefaultSetting();
        public IEnumerable<T> MatchAll(string[] data);

        public T Get(string id);
    }
}