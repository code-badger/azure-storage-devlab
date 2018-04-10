using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using table_communication_manager.Entity;

namespace table_communication_manager
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("aliens");

            table.CreateIfNotExists();

            //CreateAlien(table, new AlienEarth("badger", "badger@localhost.earth"));

            //GetAlien(table, "Earth", "badger@localhost.earth");

            GetAllAliens(table);

            Console.ReadKey();
        }

        static void CreateAlien(CloudTable table, Earthian earthian)
        {
            TableOperation insert = TableOperation.Insert(earthian);

            table.Execute(insert);
        }

        static void GetAlien(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrieve = TableOperation.Retrieve<Earthian>(partitionKey, rowKey);

            var result = table.Execute(retrieve);

            Console.WriteLine(((Earthian)result.Result).Name);
        }

        static void GetAllAliens(CloudTable table)
        {
            TableQuery<Earthian> query = new TableQuery<Earthian>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Earth"));

            foreach (Earthian earthian in table.ExecuteQuery(query))
            {
                Console.WriteLine(earthian.Name);
            }
        }
    }
}
