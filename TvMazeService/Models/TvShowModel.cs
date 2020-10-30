using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TvMazeService.Models
{
  
    public class TvShowModel
    {
        //[BsonId]
        //public ObjectId Id { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("cast")]
        public List<CastModel> Cast { get; set; }
    }
   
}
