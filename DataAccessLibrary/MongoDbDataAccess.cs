using DataAccessLibrary.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class MongoDbDataAccess
    {
        private IMongoDatabase db;  
        public MongoDbDataAccess(string dbName,string connectionAString)
        {
            var client = new MongoClient(connectionAString);
            db = client.GetDatabase(dbName);


        }
        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);

            collection.InsertOne(record);
        }
        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new MongoDB.Bson.BsonDocument()).ToList();
        }
        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).First();
        }


        public void UpsertRecord<T>(string table, string id, T record, string document)
        {
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                // new MongoDB.Bson.BsonDocument("_id", id),
                Builders<T>.Filter.Eq("Id", id),
                record,
                new ReplaceOptions { IsUpsert = true });
        }
        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }

     
    }
}
