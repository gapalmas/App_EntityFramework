using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    ///  Interfaz de clase generica para el manejo de Entidades utilizando un Tipo(T) de Entidad Generica.
    /// </summary>
    /// <typeparam name="T">Es el tipo de Entidad que utilizara la clase generica.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        #region 'CREATE'

        T Create(T obj);
        Task<T> CreateAsync(T obj);
        IEnumerable<T> AddRange(IEnumerable<T> obj);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> obj);

        #endregion
        #region 'READ'

        IEnumerable<T> Read();
        Task<IEnumerable<T>> ReadAsync();

        #endregion
        #region 'UPDATE'

        T Update(T obj, int id);
        Task<T> UpdateAsync(T obj, int id);

        #endregion
        #region 'DELETE'

        T Delete(T obj, int id);
        Task<T> DeleteAsync(T obj, int id);
        IEnumerable<T> DeleteRange(IEnumerable<T> obj);
        Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> obj);

        #endregion

        #region 'COUNT'

        int Count();
        Task<int> CountAsync();

        #endregion
        #region 'EXIST'

        Boolean Exist(Expression<Func<T, bool>> matchitem);
        Task<Boolean> ExistAsync(Expression<Func<T, bool>> matchitem);

        #endregion
        #region 'FIND'

        T Find(Expression<Func<T, bool>> matchitem);
        Task<T> FindAsync(Expression<Func<T, bool>> matchitem);
        ICollection<T> FindAll(Expression<Func<T, bool>> matchitem);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> matchitem);

        #endregion
        #region 'GET'

        T Get(int id);
        Task<T> GetAsync(int id);


        #endregion
    }
}
