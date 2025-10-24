

namespace Application.Interface.Repository
{
    public interface IStateRepository
    {
        Task<IEnumerable<State>> GetAllStatesAsync();
        Task<State?> GetStateAsync(int id);
    }
}
