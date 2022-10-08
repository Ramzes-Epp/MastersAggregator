using MasterAggregator.Desktop.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MasterAggregator.Desktop.Repositories;

internal interface IMasterRepository
{
    //если возращаем null то считаем что пользователь не авторизован
    Task<ObservableCollection<Master>?> GetAllAsync(string AuthorizationPassword);
    Task<ObservableCollection<Master>?> SaveAsync(Master master, string AuthorizationPassword); 
    Task<ObservableCollection<Master>?> DeleteAsync(Master master, string AuthorizationPassword); 
    Task<ObservableCollection<Master>?> EditAsync(Master master, string AuthorizationPassword); 
}
