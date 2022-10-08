using Avalonia.Controls;
using MasterAggregator.Desktop.Models;
using MasterAggregator.Desktop.Repositories;
using MasterAggregator.Desktop.Repositories.WebApiJson;
using MasterAggregator.Desktop.Views;
using System.Collections.ObjectModel;

namespace MasterAggregator.Desktop.ViewModels;

 
internal class MainWindowViewModel : ViewModelBase
{
    private UserRepositoryDesktopWebApi? _userRrepository;
    private IMasterRepository? _masterRrepository;
     
    public MainWindowViewModel(UserRepositoryDesktopWebApi userRrepository, MasterRepositoryDesktopWebApi masterRrepository)
    {
        _userRrepository = userRrepository;
        _masterRrepository = masterRrepository;
    }

    string AuthorizationKey { get; set; }// ТЕСТОВЫЙ API ключ в бд сервера- Keyfwsefso987kcxfv

    /// <summary>
    /// устанавлмваем авторизованы или нет
    /// </summary>
    bool IsAuthorization { get; set; } = false;
     
    /// <summary>
    /// устанавлмвает показывать коллекцию заказчиков UserItems
    /// </summary>
    internal bool IsVisibleUserItemsbool { get; set; } = false;

    /// <summary>
    /// устанавлмвает показывать коллекцию ЗАКАЗОВ OrderItems
    /// </summary>
    internal bool IsVisibleOrderItemsbool { get; set; } = false;

    /// <summary>
    /// устанавлмвает показывать коллекцию МАСТЕРОВ MasterItems
    /// </summary>
    internal bool IsVisibleMasterItemsbool { get; set; } = false;

    /// <summary>
    /// вывод названия коллекции с которой работаем на MainWindow
    /// </summary> 
    internal string TitleList { get; set; } = "ВЫ Не авторизованы - введите API ключ";

    /// <summary>
    /// список рубрик которые присутствуют в программе
    /// </summary>
    enum changeModel
    {
        Master,
        Order, 
        User,
        NoAuthorization
    }


    #region авторизация на сайте  
    /// <summary>
    /// включаем видимость элементов на MainWindow в зависимости от выбраной категории 
    /// </summary>
    void ChangeIsVisibleModel (changeModel enumModel)
    { 
        switch (enumModel)
        {
            case changeModel.Master:
                IsVisibleMasterItemsbool = true;
                IsVisibleUserItemsbool = false;
                IsVisibleOrderItemsbool = false; 
                TitleList = "СПИСОК ВСЕХ МАСТЕРОВ";
                break;
            case changeModel.Order:
                IsVisibleMasterItemsbool = false;
                IsVisibleUserItemsbool = false;
                IsVisibleOrderItemsbool = true; 
                TitleList = "СПИСОК ЗАКАЗОВ";
                break;
            case changeModel.User:
                IsVisibleMasterItemsbool = false;
                IsVisibleUserItemsbool = true;
                IsVisibleOrderItemsbool = false; 
                TitleList = "СПИСОК ВСЕХ ЗАКАЗЧИКОВ";
                break; 
        }
        //если не авторизовын пользователь
        if (IsAuthorization == false)
        { 
            IsVisibleMasterItemsbool = false;
            IsVisibleUserItemsbool = false;
            IsVisibleOrderItemsbool = false;
            TitleList = "НЕ ПРАВИЛЬНО ВВЕДЕН API ключ";
            MasterItems = null;
            UserItems = null;
        }
         
        //обновляем отображение данных на странице MainWindow.axaml
        OnPropertyChanged("IsVisibleMasterItemsbool");
        OnPropertyChanged("IsVisibleUserItemsbool");
        OnPropertyChanged("IsVisibleOrderItemsbool");
        OnPropertyChanged("TitleList");
        OnPropertyChanged("UserItems");
        OnPropertyChanged("MasterItems");
        OnPropertyChanged("IsAuthorization"); 
    }

    /// <summary>
    /// вход в учетную запись
    /// </summary>
    public async void ChekAuthorization()
    {
        if (await _userRrepository.GetAllAsync(AuthorizationKey) != null)
        {
            IsAuthorization = true;
            GetAllUser();
        } else
            TitleList = "НЕ ПРАВИЛЬНО ВВЕДЕН API ключ \n повторите попытку";
    }
     
    /// <summary>
    /// выход из учетной записи
    /// </summary>
    public async void ExitFromTheService()
    {
        AuthorizationKey = "";
        IsAuthorization = false;
        ChangeIsVisibleModel(changeModel.NoAuthorization);
    }
    #endregion



    #region работаем с обектом ЗАКАЗЧИКИ (User)  
    private ObservableCollection<User>? _UserItems;
    /// <summary>
    /// получить всех User по API
    /// </summary> 
    public ObservableCollection<User>? UserItems { get => _UserItems; set => Set(ref _UserItems, value); }

    /// <summary>
    /// получить список User в UserItems по клику кнопки заказчики
    /// </summary>
    public async void GetAllUser()
    { 
        UserItems = await _userRrepository.GetAllAsync(AuthorizationKey);
        if (UserItems != null)
        {
            IsAuthorization = true;
            ChangeIsVisibleModel(changeModel.User);//включаем видимость UserItems на MainWindow.axaml   
        }
        else
        {
            IsAuthorization = false;
            ChangeIsVisibleModel(changeModel.NoAuthorization);
        }
    }

    /// <summary>
    /// Выбранный User из формы
    /// </summary>
    private User _SelectedUser;
    public User SelectedUser { get => _SelectedUser; set => Set(ref _SelectedUser, value); }
     
    /// <summary>
    /// Удаление ЗАКАЗЧИКА (User)
    /// </summary> 
    public async void RemoveUser()
    {
        if (SelectedUser != null)
            UserItems = await _userRrepository.DeleteAsync(SelectedUser, AuthorizationKey);
    }
     
    /// <summary>
    /// Создание нового ЗАКАЗЧИКА (User)
    /// </summary> 
    public async void CreateUser()
    {
        if (UserItems != null)
        {
            User NewUser = new User() { Id = 0, FirstName = "", Name = "", Pfone = "" };
            var view_model = new UserCreateViewModel(NewUser);
            var view = new UserCreateWindow
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            view_model.Complete += async(_, e) =>
            {
                view.Close();
                //пререзаписываем в бд если корректно заполнили данные для User (передали в e true)
                if (e == true)
                {
                    //добавляем в бд NewUser
                    ObservableCollection<User>? updateUsers = await _userRrepository.SaveAsync(NewUser, AuthorizationKey);

                    //Если удачно сохранили в БД то обновляем список UserItems
                    if (updateUsers != null)
                        UserItems = updateUsers;

                    //обновляем список на странице MainWindow
                    OnPropertyChanged("UserItems");
                }
            };

            view.Show();
        }
    }
     
    /// <summary>
    /// Редактирование ЗАКАЗЧИКА (User)
    /// </summary> 
    public async void EditUser()
    {
        if (SelectedUser != null)
        {
            var view_model = new UserEditorViewModel(SelectedUser);
            var view = new UserEditorWindow
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            view_model.Complete += async(_, e) =>
            {
                view.DialogResult = e;
                view.Close();
                //пререзаписываем в бд если есть изменеия в User (передали в e true)
                if (e == true)
                {
                    //пререзаписываем в бд
                    await _userRrepository.EditAsync(SelectedUser, AuthorizationKey);
                    //TODO проверить на опубликованном приложение если будет обновление списка при редактирование то убрать
                    UserItems.Insert(0, SelectedUser);
                    UserItems.RemoveAt(0);
                    //обновляем список на странице MainWindow
                    OnPropertyChanged("UserItems");
                }
            };

            view.Show();
        }
    }
    #endregion



    #region работаем с обектом МАСТЕРА (Master)  
    private ObservableCollection<Master>? _MasterItems;
    /// <summary>
    /// получить всех Master по API
    /// </summary> 
    public ObservableCollection<Master>? MasterItems { get => _MasterItems; set => Set(ref _MasterItems, value); }

    /// <summary>
    /// получить список Master в MasterItems по клику кнопки мастера
    /// </summary>
    public async void GetAllMaster()
    {  
        MasterItems = await _masterRrepository.GetAllAsync(AuthorizationKey);
        if (MasterItems != null)
        {
            IsAuthorization = true;
            ChangeIsVisibleModel(changeModel.Master);//включаем видимость MasterItems на MainWindow.axaml  
        }
        else
        {
            IsAuthorization = false;
            ChangeIsVisibleModel(changeModel.NoAuthorization);
        } 
    }

    /// <summary>
    /// Выбранный Master из формы
    /// </summary>
    private Master _SelectedMaster;
    public Master SelectedMaster { get => _SelectedMaster; set => Set(ref _SelectedMaster, value); }
     
    /// <summary>
    /// Удаление Master
    /// </summary> 
    public async void RemoveMaster()
    {
        if (SelectedMaster != null)
            MasterItems = await _masterRrepository.DeleteAsync(SelectedMaster, AuthorizationKey);
        //MasterItems = _masterRrepository.Delete(SelectedMaster);
    }
     
    /// <summary>
    /// Создание нового ЗАКАЗЧИКА (User)
    /// </summary> 
    public async void CreateMaster()
    {
        if (MasterItems != null)
        {
             Master NewMaster = new Master() { Id = 0, MastersName = "", IsActive = true };
            var view_model = new MasterCreateViewModel(NewMaster);
            var view = new MasterCreateWindow
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            view_model.Complete += async (_, e) =>
            {
                view.Close();
                //пререзаписываем в бд если корректно заполнили данные для Master (передали в e true)
                if (e == true)
                {
                    //добавляем в бд NewMaster
                    ObservableCollection<Master>? updateMasters = await _masterRrepository.SaveAsync(NewMaster, AuthorizationKey);

                    //Если удачно сохранили в БД то обновляем список MasterItems
                    if (updateMasters != null)
                        MasterItems = updateMasters;

                    //обновляем список на странице MainWindow
                    OnPropertyChanged("MasterItems");
                }
            };

            view.Show();
        }
    }
     
    /// <summary>
    /// Редактирование Master
    /// </summary> 
    public void EditMaster()
    {
        if (SelectedMaster != null)
        {
            var view_model = new MasterEditorViewModel(SelectedMaster);
            var view = new MasterEditorWindow
            {
                DataContext = view_model,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            view_model.Complete += async (_, e) =>
            {
                view.DialogResult = e;
                view.Close();
                //пререзаписываем в бд если есть изменеия в Master (передали в e true)
                if (e == true)
                {
                    //пререзаписываем в бд
                    MasterItems = await _masterRrepository.EditAsync(SelectedMaster, AuthorizationKey);
                    //TODO проверить на опубликованном приложение если будет обновление списка при редактирование то убрать
                /*    MasterItems.Insert(0, null);
                    MasterItems.RemoveAt(0);*/
                    //обновляем список на странице MainWindow
                    OnPropertyChanged("MasterItems");
                }
            };

            view.Show();
        }
    }
    #endregion

}