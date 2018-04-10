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

            //CreateAlien(table, new Earthian("super_badger", "super_badger@localhost.earth"));
            //GetAlien(table, "Earth", "badger@localhost.earth");

            var awesome_badger = GetAlien(table, "Earth", "super_badger@localhost.earth");
            //awesome_badger.Name = "awesome_badger";

            //UpdateAlien(table, awesome_badger);

            DeleteAlien(table, awesome_badger);

            GetAllAliens(table);

            Console.ReadKey();
        }

        static void CreateAlien(CloudTable table, Earthian earthian)
        {
            TableOperation insert = TableOperation.Insert(earthian);

            table.Execute(insert);
        }

        static Earthian GetAlien(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrieve = TableOperation.Retrieve<Earthian>(partitionKey, rowKey);

            var result = table.Execute(retrieve);

            return (Earthian)result.Result;
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

        static void UpdateAlien(CloudTable table, Earthian earthian)
        {
            TableOperation update = TableOperation.Replace(earthian);

            table.Execute(update);
        }

        static void DeleteAlien(CloudTable table, Earthian earthian)
        {
            TableOperation delete = TableOperation.Delete(earthian);

            table.Execute(delete);
        }

    }
}
