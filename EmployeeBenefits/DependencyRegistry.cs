using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBenefits.Models;
using EmployeeBenefits.Persistence;
using EmployeeBenefits.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace EmployeeBenefits
{
    public static class DependencyRegistry
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton<IBenefitsCalculatorService, BenefitsCalculatorService>();
            var useInMemoryPersistence = false;
            var useInMemoryPersistenceSection = configuration.GetSection("useInMemoryPersistence");
            if (useInMemoryPersistenceSection != null)
                useInMemoryPersistence = Convert.ToBoolean(useInMemoryPersistenceSection.Value);
            if (useInMemoryPersistence)
            {
                services.AddSingleton<IEmployeeRepository, InMemoryPersistenceRepository>();
            }
            else
            {
                services.AddSingleton<IEmployeeRepository, MongoDbEmployeeRepository>();
                services.AddSingleton(GetMongoDatabase(configuration));

            }
        }

        private static IMongoDatabase GetMongoDatabase(IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetSection("mongoConnectionString").Value;

            BsonClassMap.RegisterClassMap<Employee>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
            var mongoClient = new MongoClient(mongoConnectionString);
            var databaseName = new MongoUrlBuilder(mongoConnectionString).DatabaseName;
            return mongoClient.GetDatabase(databaseName);
        }
    }
}
