using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Heroes.DB.Model
{
    [BsonIgnoreExtraElements]
    public class Hero
    {
        
        //public ObjectId Id { get; set; }

        [BsonElement("HeroId")]
        public int? HeroId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Created")]
        public DateTime? Created { get; set; }

        [BsonElement( "Updated" )]
        public DateTime? Updated { get; set; }
    }
}
