using MaratonValto.Models;
using MaratonValto.Models.DTOs;

namespace MaratonValto.Services.Library
{
    public interface IFutok
    {
        Task<object> GetAllRunners();
        Task<object> GetRunnerResults(int futoId);
        Task<object> UpdateRunner(int id, FutoDTO futo);
        Task<object> DeleteRunner(int id);
        Task<object> AddResult(int futoId, EredmenyDTO eredmeny);
        Task<object> GetFemaleRunners();
    }
}
