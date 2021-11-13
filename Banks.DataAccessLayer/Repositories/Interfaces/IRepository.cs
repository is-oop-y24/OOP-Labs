namespace Banks.DataAccessLayer.Interfaces
{
    public interface IRepository<TModel>
    {
        void Add(TModel model);
        void Update(TModel model);
        void Get(int id);
        void Delete(int id);
    }
}