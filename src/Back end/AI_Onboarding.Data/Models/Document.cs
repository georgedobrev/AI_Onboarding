using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AI_Onboarding.Data.Models
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ExtractedText { get; set; }
        public bool IsForTraining { get; set; } = true;
    }
}

