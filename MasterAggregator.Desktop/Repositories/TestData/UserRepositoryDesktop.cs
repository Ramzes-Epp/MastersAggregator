using MasterAggregator.Desktop.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MasterAggregator.Desktop.Repositories.TestData;

internal class UserRepositoryDesktop : IUserRepository
{
    static ObservableCollection<User>? Users { get; set; }
    //Все тестовые данные в классе TestData 
    internal UserRepositoryDesktop()
    {
        Users = TestDataUsers.Users;
    }

    public async Task<ObservableCollection<User>?> GetAllAsync(string AuthorizationPassword)
    {
        //если возращаем null то считаем что пользователь не авторизован
        return await Task.Run(() =>
        { 
            return Users;
        });
    }

    public async Task<ObservableCollection<User>?> SaveAsync(User user, string AuthorizationPassword)
    {
        user.Id = Users.Count;
        Users.Insert(0, user);
        return await GetAllAsync(AuthorizationPassword);
    }

    public async Task<ObservableCollection<User>?> DeleteAsync(User user, string AuthorizationPassword)
    {
        for (int i = 0; i < Users.Count; i++)
        {
            if (Users.ElementAt(i) == user)
                Users.RemoveAt(i);
        }

        return await GetAllAsync(AuthorizationPassword);
    }

    public async Task<ObservableCollection<User>?> EditAsync(User user, string AuthorizationPassword)
    {
        for (int i = 0; i < Users.Count; i++)
        {
            if (Users.ElementAt(i) == user)
            {
                Users.ElementAt(i).Id = user.Id;
                Users.ElementAt(i).Name = user.Name;
                Users.ElementAt(i).FirstName = user.FirstName;
                Users.ElementAt(i).Pfone = user.Pfone;
                return await GetAllAsync(AuthorizationPassword);
            }
        }

        return null;
    }
}



//Класс в котором храним входные тестовые данные для проекта 
internal static class TestDataUsers
{
    internal static ObservableCollection<User> Users = new ObservableCollection<User>
    {
        new User { Id = 0,Name = "Brock", FirstName ="Avye", Pfone ="(745)-582-8364"},
        new User { Id = 1, Name = "Hashim", FirstName ="Willa", Pfone ="(745)-273-5007"},
        new User { Id = 2,Name = "Harper", FirstName ="Tamekah", Pfone ="(895)-542-7744"},
        new User { Id = 3,Name = "Solomon", FirstName ="Jael", Pfone ="(415)-317-3189"},
        new User { Id = 4,Name = "Kennedy", FirstName ="Ava", Pfone ="(883)-730-2898"},
        new User { Id = 5,Name = "Oscar", FirstName ="Bryar", Pfone ="(745)-981-7472"},
        new User { Id = 6,Name = "Troy", FirstName ="Briar", Pfone ="(745)-304-7368"},
        new User { Id = 7,Name = "Aladdin", FirstName ="Rhonda", Pfone ="(982)-766-8454"},
        new User { Id = 8,Name = "Ryder", FirstName ="Brittany", Pfone ="(629)-392-2081"},
        new User { Id = 9,Name = "Paki", FirstName ="Yvonne", Pfone ="(745)-367-5834"},
        new User { Id = 10,Name = "Kibo", FirstName ="Meghan", Pfone ="(745)-844-9846"},
        new User { Id = 11,Name = "Barrett", FirstName ="Guinevere", Pfone ="(745)-445-8239"},
        new User { Id = 12,Name = "Brett", FirstName ="Jessamine", Pfone ="(745)-963-4862"},
        new User { Id = 13,Name = "Neville", FirstName ="Katelyn", Pfone ="(745)-549-3802"},
        new User { Id = 14,Name = "Kaseem", FirstName ="Brooke", Pfone ="(524)-641-0603"},
        new User { Id = 15,Name = "Fuller", FirstName ="Wyoming", Pfone ="(745)-515-8546"},
        new User { Id = 16,Name = "Thane", FirstName ="Ignacia", Pfone ="(255)-834-8247"},
        new User { Id = 17,Name = "Norman", FirstName ="Margaret", Pfone ="(745)-484-1842"},
        new User { Id = 18,Name = "Micah", FirstName ="Pascale", Pfone ="(349)-955-4354"},
        new User { Id = 19,Name = "Gabriel", FirstName ="Brenna", Pfone ="(967)-632-7815"},
        new User { Id = 20,Name = "Rahim", FirstName ="Joelle", Pfone ="(745)-365-3310"},
        new User { Id = 21,Name = "Harper", FirstName ="Courtney", Pfone ="(582)-202-5582"},
        new User { Id = 22,Name = "Anthony", FirstName ="Ella", Pfone ="(745)-868-6336"}
    };

}
