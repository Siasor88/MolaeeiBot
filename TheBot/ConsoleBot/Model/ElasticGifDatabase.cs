using System.Collections.Generic;
using System.Linq;
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
            if (DoesGifExists(doc))
            {
                Update(doc);
            }
            else
            {
                IndexGif(doc);
            }
        }

        private void IndexGif(Gif doc)
        {
            _client.Index(doc, idx => idx.Index(_indexName));
        }

        public void Update(Gif doc)
        {
            var currentGifOnDatabase = Get(doc.UniqueId);
            doc.Data = doc.Data + " " + currentGifOnDatabase?.Data;
            IndexGif(doc);
            
        }

        private bool DoesGifExists(Gif gif)
        {
            var result = SearchForGif(gif.UniqueId);
            return
                result.Hits.Count != 0;
        }

        private ISearchResponse<Gif> SearchForGif(string uniqueId)
        {
            var values = new[] {new Id(uniqueId)};
            var result = _client.Search<Gif>(sd =>
                sd.Index(_indexName).Query(qcd
                    => qcd.Ids(iqc
                        => iqc.Values(values))));
            return result;
        }

        public void RemoveDocument(Gif doc)
        {
            _client.Delete<Gif>(doc.UniqueId);
        }

        public void ConfigureDefaultSetting()
        {
            if (!DoesIndexExists(_indexName))
            {
                _client.Indices.Create(_indexName,
                    s => s.Settings(sc => sc
                                                            .NumberOfShards(3)
                                                            .NumberOfReplicas(3)));
            }
        }

        public bool DoesIndexExists(string indexName)
        {
            return _client.Indices.Exists(indexName).Exists;
        }
        
        public Gif? Get(string uniqueId)
        {
            var searchResult = SearchForGif(uniqueId);
            if (searchResult.Hits.Count == 0)
            {
                return null;
            }

            return searchResult.Hits.First().Source;
        }

        public IEnumerable<Gif> MatchAll(string[] allData)
        {
            var query = CreateQuery(allData);
            var searchResult = Search(query);
            var results = searchResult.Hits.Select(hit => hit.Source);
            return results;
        }

        private ISearchResponse<Gif> Search(QueryBase query)
        {
            return _client.Search<Gif>(sc => sc.Index(_indexName).Size(10).Query(_ => query));
        }

        private QueryBase CreateQuery(string[] allData)
        {
            BoolQuery query = new BoolQuery();
            var allOfQueries = GetAllOfQueries(allData);
            query.Must = allOfQueries;
            return query;
        }

        private IEnumerable<QueryContainer> GetAllOfQueries(string[] allData)
        {
            var allQueries = new List<QueryContainer>();
            foreach (var data in allData)
            {
                var matchQuery = new MatchQuery()
                {
                    Field = "Data",
                    Fuzziness = Fuzziness.Ratio(GetRatio(data)),
                };
                allQueries.Add(matchQuery);
            }

            return allQueries;
        }

        private int GetRatio(string data)
        {
            int lenght = data.Length;
            if (lenght < 3)
            {
                return 0;
            }
            if (lenght < 4)
            {
                return 1;
            }
            return 2;
        }

        
    }
}