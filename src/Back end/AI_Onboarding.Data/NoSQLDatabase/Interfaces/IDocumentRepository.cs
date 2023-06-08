using AI_Onboarding.Data.Models;

namespace AI_Onboarding.Data.NoSQLDatabase.Interfaces
{
    public interface IDocumentRepository
    {
        List<Document> GetAll();
        Document Find(string id);
        void Add(Document document);
        void Update(string id, Document document);
        void Remove(string id);
    }
}

