using System.Collections.Generic;
using TourHelper.Base.Model.Entity;

namespace TourHelper.Base.Repository
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseModel, new()
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        int Update(TEntity entityModel);

        int UpdateRange(IEnumerable<TEntity> entitiesModel);

        TEntity Insert(TEntity entityModel);

        int Delete(TEntity entityModel);

        int DeleteRange(IEnumerable<TEntity> entitiesModel);
    }
}
