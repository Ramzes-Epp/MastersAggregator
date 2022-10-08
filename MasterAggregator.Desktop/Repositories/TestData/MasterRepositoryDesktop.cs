using MasterAggregator.Desktop.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MasterAggregator.Desktop.Repositories.TestData;

internal class MasterRepositoryDesktop : IMasterRepository
{
    static ObservableCollection<Master>? Masters { get; set; }
    //Все тестовые данные в классе TestData  
    internal MasterRepositoryDesktop()
    {
        Masters = TestDataMaster.Masters;
    }

    public async Task<ObservableCollection<Master>?> GetAllAsync(string AuthorizationPassword)
    {
        return await Task.Run(() =>
        {
            return Masters;
        });
    }

    public async Task<ObservableCollection<Master>?> SaveAsync(Master master, string AuthorizationPassword)
    {
        master.Id = Masters.Count;
        Masters.Insert(0, master);
        return await GetAllAsync(AuthorizationPassword);
    }

    public async Task<ObservableCollection<Master>?> DeleteAsync(Master master, string AuthorizationPassword)
    {
        for (int i = 0; i < Masters.Count; i++)
        {
            if (Masters.ElementAt(i) == master) 
                Masters.RemoveAt(i); 
            
        }

        return await GetAllAsync(AuthorizationPassword);
    }

    public async Task<ObservableCollection<Master>?> EditAsync(Master master, string AuthorizationPassword)
    {
        for (int i = 0; i < Masters.Count; i++)
        {
            if (Masters.ElementAt(i) == master)
            {
                Masters.ElementAt(i).Id = master.Id;
                Masters.ElementAt(i).MastersName = master.MastersName;
                Masters.ElementAt(i).IsActive = master.IsActive;
                return await GetAllAsync(AuthorizationPassword);
            }
        }

        return null;
    }
}



//Класс в котором храним входные тестовые данные для проекта 
internal static class TestDataMaster
{
    internal static ObservableCollection<Master> Masters = new ObservableCollection<Master>
    {
        new Master { Id = 0, MastersName = "Андрей Иванов", IsActive = true },
        new Master { Id = 1, MastersName = "Сергей Степанов", IsActive = true},
        new Master { Id = 2, MastersName = "Вася Кузькин", IsActive = false},
        new Master { Id = 3, MastersName = "Петя Малый", IsActive = true},
        new Master { Id = 4, MastersName = "Антон Быстрый", IsActive = true},
        new Master { Id = 5, MastersName = "Игорь Клуша", IsActive = false},
        new Master { Id = 6, MastersName = "Вова Супер", IsActive = true},
        new Master { Id = 7, MastersName = "Семен Лобанов", IsActive = true},
        new Master { Id = 8, MastersName = "Вадим Куцый", IsActive = false},
        new Master { Id = 9, MastersName = "Федор Имельяненко", IsActive = true},
        new Master { Id = 10, MastersName = "Арнольд Шварц", IsActive = false},
        new Master { Id = 11, MastersName = "Николай Басков", IsActive = true},
        new Master { Id = 12, MastersName = "Михаил Чакров", IsActive = true},
        new Master { Id = 13, MastersName = "Олег Тусов", IsActive = true},
        new Master { Id = 14, MastersName = "Ваня Руков", IsActive = true}
    }; 
}