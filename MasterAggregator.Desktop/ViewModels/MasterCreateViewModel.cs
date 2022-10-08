using MasterAggregator.Desktop.Commands;
using MasterAggregator.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MasterAggregator.Desktop.ViewModels;

internal class MasterCreateViewModel : ViewModelBase
{
    private Master? _Master;
    public MasterCreateViewModel(Master Master)
    {
        _Master = Master;
    }

    /// <summary>
    ///словарь для сохраниения промежуточных данных <string, object> string-поле свойства Master, object-измененные значения
    ///<summary>
    private readonly Dictionary<string, object> _Values = new();

    /// <summary>
    /// запись в словарь
    /// </summary> 
    protected virtual bool SetValue(object value, [CallerMemberName] string Property = null)
    {
        if (_Values.TryGetValue(Property!, out var old_value) && Equals(value, old_value))
            return false;
        _Values[Property] = value;
        OnPropertyChanged(Property);
        return true;
    }
    /// <summary>
    /// чтение из словаря
    /// </summary> 
    protected virtual T GetValue<T>(T Default, [CallerMemberName] string Property = null)
    {
        if (_Values.TryGetValue(Property!, out var value))//если удплось извлечь по ключу значение
            return (T)value;
        return Default;
    }

    public string? MastersName { get => GetValue(_Master.MastersName); set => SetValue(value); }
    public bool IsActive { get => GetValue(_Master.IsActive); set => SetValue(value); }
    public Action<object, bool> Complete { get; internal set; }

    /// <summary>
    /// устанавливаем состояние Master занят или нет
    /// </summary>
    public void ChekBysyMasterCommandTrue() => IsActive = true;
    public void ChekBysyMasterCommandFalse() => IsActive = false;
    
    #region Command CancelCommand - Отменить изменения 
    /// <summary>
    /// Отменить изменения
    /// </summary>
    private ICommand _CancelCommand;

    /// <summary>
    /// Отменить изменения
    /// </summary>
    public ICommand CancelCommand => _CancelCommand
        ??= new LambdaCommand(OnCancelCommandExecuted, CanCancelCommandExecute);

    /// <summary>
    /// Проверка возможности выполнения - Отменить изменения
    /// </summary>
    private bool CanCancelCommandExecute(object p) => true;

    /// <summary>
    /// Логика выполнения - Отменить изменения
    /// </summary>
    private void OnCancelCommandExecuted(object p)
    {
        Reject();
        Complete?.Invoke(this, false);
    }

    /// <summary>
    /// отмена изменений
    /// </summary>
    public void Reject()
    {
        var properties = _Values.Keys.ToArray();
        _Values.Clear();
        //очищаем временный словарь
        foreach (var property in properties)
            OnPropertyChanged(property);
    }
    #endregion




    #region Command CommitCommand - Принять изменения 
    /// <summary>
    /// Принять изменения
    /// </summary>
    private ICommand _CommitCommand;

    /// <summary>
    /// Принять изменения
    /// </summary>
    public ICommand CommitCommand => _CommitCommand
        ??= new LambdaCommand(OnCommitCommandExecuted, CanCommitCommandExecute);

    /// <summary>
    /// Проверка возможности выполнения - Принять изменения
    /// </summary>
    private bool CanCommitCommandExecute(object p) => true;

    /// <summary>
    /// вносили изменение данных в форме
    /// </summary>
    private bool ChengeValuesBool = false;

    /// <summary>
    /// Логика выполнения - Принять изменения
    /// </summary>
    private void OnCommitCommandExecuted(object p)
    {
        Commit();
        Complete?.Invoke(this, ChengeValuesBool);//генерируем событие и передаем параметр true в окно откуда вызывали void
    }
    public void Commit()
    {
        if (MastersName != "" && IsActive != null)
        {
            ChengeValuesBool = true;
            _Master.MastersName = (string)_Values["MastersName"];
            _Master.IsActive = IsActive;  
        }
    }
    #endregion
}
