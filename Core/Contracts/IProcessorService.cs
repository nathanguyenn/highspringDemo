namespace WebApplication1.Core.Contracts
{
    public interface IProcessorService
    {
        Task<string> ProcessMessage(string id);
    }
}
