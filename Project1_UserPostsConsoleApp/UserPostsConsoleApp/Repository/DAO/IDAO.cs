namespace UserPostsConsoleApp.DAO;

public interface IDAO<T>
{
    public void Create(T item);

    public T GetById(int ID);

    public ICollection<T> GetAll();

    public void Update(T item);

    public void Delete(T item);
}