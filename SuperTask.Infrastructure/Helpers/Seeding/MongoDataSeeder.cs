using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using SuperTask.Data.DataModels;
using SuperTask.Infrastructure.Helpers.Constants;

namespace SuperTaskTracking.Helpers.Seeding;

public static class MongoDataSeeder
{
    public static async Task SeedDatabaseAsync(IMongoDatabase database)
    {
        try
        {
            Console.WriteLine("--> Mongo seeding started");
            
            var collection = database.GetCollection<TaskListDocument>(MongoCollections.TaskLists);
            
            var count = await collection.CountDocumentsAsync(FilterDefinition<TaskListDocument>.Empty);
            
            if (count == 0)
            {
                var primeUser = Guid.Parse("11111111-1111-1111-1111-111111111111");
                var colleagueUser = Guid.Parse("22222222-2222-2222-2222-222222222222");
                var familyUser = Guid.Parse("33333333-3333-3333-3333-333333333333");

                var testData = new List<TaskListDocument>
                {
                    new TaskListDocument
                    {
                        Id = Guid.NewGuid(),
                        Name = "Work Deliverables",
                        OwnerUserId = primeUser,
                        SharedUserIds = new List<Guid> { colleagueUser },
                        CreatedAt = DateTime.UtcNow.AddDays(-5)
                    },
                    new TaskListDocument
                    {
                        Id = Guid.NewGuid(),
                        Name = "Groceries & Home",
                        OwnerUserId = primeUser,
                        SharedUserIds = new List<Guid> { familyUser },
                        CreatedAt = DateTime.UtcNow.AddDays(-2)
                    },
                    new TaskListDocument
                    {
                        Id = Guid.NewGuid(),
                        Name = "Personal Fitness Routine",
                        OwnerUserId = primeUser,
                        SharedUserIds = new List<Guid>(),
                        CreatedAt = DateTime.UtcNow
                    },
                    new TaskListDocument
                    {
                        Id = Guid.NewGuid(),
                        Name = "Shared Project Architecture",
                        OwnerUserId = colleagueUser,
                        SharedUserIds = new List<Guid> { primeUser },
                        CreatedAt = DateTime.UtcNow.AddHours(-12)
                    }
                };

                await collection.InsertManyAsync(testData);
                
                Console.WriteLine("--> Seed inserted");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Error occurred during database seeding: {ex.Message}");
        }
    }
}