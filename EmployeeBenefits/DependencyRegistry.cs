﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBenefits.Persistence;
using EmployeeBenefits.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EmployeeBenefits
{
    public static class DependencyRegistry
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoDbEmployeeRepository, MongoDbEmployeeRepository>();
            services.AddSingleton<IBenefitsCalculatorService, BenefitsCalculatorService>();
            services.AddSingleton(GetMongoDatabase());
        }

        private static IMongoDatabase GetMongoDatabase()
        {
            var mongoConnection = "mongodb://employee-benefits-app-user:odGLjzZgz3TxaeLU@cluster0-shard-00-01-z7xz4.azure.mongodb.net:27017,cluster0-shard-00-02-z7xz4.azure.mongodb.net:27017,cluster0-shard-00-00-z7xz4.azure.mongodb.net:27017/employee-benefits?authSource=admin&replicaSet=Cluster0-shard-0&readPreference=primary&appname=MongoDB%20Compass&ssl=true";

            var mongoClient = new MongoClient(mongoConnection);
            var databaseName = new MongoUrlBuilder(mongoConnection).DatabaseName;
            return mongoClient.GetDatabase(databaseName);
        }
    }
}