namespace MastersAggregatorService.Models;

public class Master : BaseModel
{
    public string MastersName { get; init; }
    public bool IsActive { get; init; }

    public override bool Equals(object? obj)
    {
        // возвращает false если объект null
        if (obj == null)
            return false;

        // возвращает false если объект нельзя привести к типу Master
        Master master = obj as Master; 
        if (master as Master == null)
            return false;
        //сравниваем по имени и Id нашего Master, т.к в БД уже есть повторяющиеся имена работать будет не кооректно
        return master.MastersName == this.MastersName && master.Id == this.Id;
    }
    // вместе с методом Equals следует реализовать метод GetHashCode
    public override int GetHashCode() => MastersName.GetHashCode();
}