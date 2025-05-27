using WebApplication1.Core.Contracts;

namespace WebApplication1.Core.Services
{
    public class ProcessorService : IProcessorService
    {
        public ProcessorService() { }
        public Task<string>ProcessMessage(string id)
        {
            return Task.FromResult("This is result for id: aaaa");
        }
    }
}
