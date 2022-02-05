using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagerApi4.Services
{
    // Projelerde kullanılan tüm servislerin türetildiği base interface
    public interface IService<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
    {

        List<TModel> Get(Expression<Func<TModel, bool>> predicate = null);


        TModel GetById(int id);


        void  Add(TModel model, bool saveChanges = true);


        void  Update(TModel model, bool saveChanges = true);


        void  Delete(int id, bool saveChanges = true);
                
    }
}


