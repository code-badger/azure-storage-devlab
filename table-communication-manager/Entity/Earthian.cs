using Microsoft.WindowsAzure.Storage.Table;

namespace table_communication_manager.Entity
{
    class Earthian : TableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Earthian() {}

        public Earthian(string name, string email)
        {
            this.Name = name;
            this.Email = email;
            this.PartitionKey = "Earth";
            this.RowKey = email;
        }
    }
}
