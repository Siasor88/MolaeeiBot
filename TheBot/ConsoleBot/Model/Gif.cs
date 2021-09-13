using System;
using Nest;

namespace ConsoleBot
{
    [ElasticsearchType(IdProperty = nameof(UniqueId))]
    public class Gif
    {
        public Gif(string uniqueId, string fileId, long ownerId)
        {
            UniqueId = uniqueId;
            FileId = fileId;
            OwnerId = ownerId;
        }

        public long OwnerId { get; }
        public string UniqueId { get; }
        public string FileId { get; }

        public string Data { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Gif)
            {
                return obj.GetHashCode() == this.GetHashCode();
            }
            return false;
        }

        public override int GetHashCode()
        {
            return UniqueId.GetHashCode();
        }
    }
}