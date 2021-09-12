using System.Collections.Generic;
using Nest;

namespace ConsoleBot
{
    public class ElasticGifDatabase : IDatabase<Gif>
    {
        private ElasticClient _client;
        private string _indexName;

        public ElasticGifDatabase(ElasticClient client, string indexName)
        {
            _client = client;
            _indexName = indexName;
        }

        public void AddDocument(Gif doc)
        {
            var response = _client.Index(doc, idx => idx.Index(_indexName));

            throw new System.NotImplementedException();
        }

        private bool DoesGifExists(Gif gif)
        {
            var values = new[] {new Id(gif.UniqueId)};
            var result = _client.Search<Gif>(sd => 
                    sd.Index(_indexName).Query(qcd 
                        => qcd.Ids(iqc 
                            => iqc.Values(values))));
            return
                result.Hits.Count != 0;
        }

        public void RemoveDocument(Gif doc)
        {
            throw new System.NotImplementedException();
        }

        public void Create()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Gif> GetAllData()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Gif> MatchAll(string[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}