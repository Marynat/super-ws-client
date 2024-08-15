namespace super_ws.database.Entity;

public interface ITable
{
    T GetRow<T>(int Id);
    void AddRow();
}
