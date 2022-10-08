using MasterAggregator.Desktop.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MasterAggregator.Desktop.Repositories;

internal interface IUserRepository
{
    Task<ObservableCollection<User>?> GetAllAsync(string AuthorizationPassword);
    Task<ObservableCollection<User>?> SaveAsync(User user, string AuthorizationPassword);

    Task<ObservableCollection<User>?> DeleteAsync(User user, string AuthorizationPassword);

    Task<ObservableCollection<User>?> EditAsync(User user, string AuthorizationPassword);
}



