using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Heroes.DB.Model
{
    public class NewHero
    {

        [BsonElement("HeroId")]
        public int HeroId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Created")]
        public DateTime Created { get; set; }

        [BsonElement( "Updated" )]
        public DateTime Updated { get; set; }
    }
}
