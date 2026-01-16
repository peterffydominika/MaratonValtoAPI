namespace MaratonValto.Services.Library
{
    public interface IFutok
    {
        Task<object> GetAllRunners();
        Task<object> GetRunnerResults(int futoId);
    }
}
