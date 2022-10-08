using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MasterAggregator.Desktop.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged //INotifyPropertyChanged - интерфейс используетс¤ дл¤ отслеживани¤ изменений в Property, определенных в ViewModel. 
{
    //через наследованный интерфейс INotifyPropertyChanged реализуем событие PropertyChanged помещаем PropertyChanged в сеттер (set) и сеттер передаст нашу переменную через PropertyChanged 
    //делегат PropertyChangedEventHandler ассоциирован с классом PropertyChangedEventArgs, определ¤ющим всего одно свойство:
    //PropertyName типа string. если класс реализует INotifyPropertyChanged, то при каждом изменении одного из его свойств инициируется событие PropertyChanged.
    public event PropertyChangedEventHandler PropertyChanged;

    //атрибут [CallerMemberName] позволяет не указывать имя свойства, если вызов происходит из Set метода этого свойства.
    //тоесть наличие этого атрибута присваивает значение параметру, к которому он применен, равное имени вызывающего метода. В случае геттеров и сеттеров свойств это имя свойства.
    protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(PropertyName);
        return true;
    }

}