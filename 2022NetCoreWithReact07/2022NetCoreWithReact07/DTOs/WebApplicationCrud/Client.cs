namespace _2022NetCoreWithReact07.DTOs.WebApplicationCrud
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ClientExecution 
    {
        public ClientExecution(Client client, long executionTime)
        {
            Client = client;
            ExecutionTime = executionTime;
        }

        public Client Client { get; set; }
        public long ExecutionTime { get; set; }
    }
}
