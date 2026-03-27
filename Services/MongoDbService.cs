using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MoviesManager.Models;

namespace MoviesManager.Services
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string MoviesCollectionName { get; set; } = string.Empty;
        public string UsersCollectionName { get; set; } = string.Empty;
    }

    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);

            Movies = _database.GetCollection<Movie>(settings.Value.MoviesCollectionName);
            Users = _database.GetCollection<AppUser>(settings.Value.UsersCollectionName);
        }

        public IMongoCollection<Movie> Movies { get; }
        public IMongoCollection<AppUser> Users { get; }
    }
}