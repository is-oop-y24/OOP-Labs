using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Banks.DataAccessLayer.Interfaces
{
    public interface IRepository<TModel> where TModel : IDbModel
    {
        void Add(TModel model);
        void Update(TModel model);
        TModel GetModel(int id);
        void Delete(int id);
    }
}