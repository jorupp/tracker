namespace RightpointLabs.Tracker.Infrastructure.Persistence
{
    using MongoDB.Driver;

    public interface IMongoConnectionHandler
    {
        MongoDatabase Database { get; }
    }
}