using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.NoSQLDatabase.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AI_Onboarding.Data.NoSQLDatabase
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMongoCollection<Document> _documents;

        public DocumentRepository(IConfiguration configuration, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(configuration["MongoDBSettings:DatabaseName"]);
            _documents = database.GetCollection<Document>(configuration["MongoDBSettings:CollectionName"]);
        }

        public void Add(Document document)
        {
            _documents.InsertOne(document);
        }

        public Document Find(string id)
        {
            return _documents.Find(d => d.Id == id).FirstOrDefault();
        }

        public List<Document> GetAll()
        {
            return _documents.Find(d => true).ToList();
        }

        public void Remove(string id)
        {
            _documents.DeleteOne(d => d.Id == id);
        }

        public void Update(string id, Document document)
        {
            _documents.ReplaceOne(d => d.Id == id, document);
        }
    }
}

