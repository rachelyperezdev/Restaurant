namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Delete (int id);
        Task Update (SaveViewModel vm, int id);
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task<List<SaveViewModel>> GetAllSaveViewModel();
    }
}
