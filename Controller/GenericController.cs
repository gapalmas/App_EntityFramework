using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    /// <summary>
    /// Controlador de clase generica 'GenericRepository' para el manejo de Entidades utilizando un Tipo(T) de Entidad.
    /// </summary>
    /// <typeparam name="T">Es el tipo de Entidad que utilizara para el controlador.</typeparam>
    public class GenericController<T> where T : class
    {
        #region 'Instancia Repositorio'
        // Instancia para manejar el GenericRepository
        private static readonly IGenericRepository<T> Repo = new GenericRepository<T>();

        #endregion

        #region 'Controlador CREATE'
        /// <summary>
        /// Método para CREAR un objeto/registro en la base de datos.
        /// </summary>
        /// <param name="obj">Entidad para ser tratada como objeto.</param>
        /// <returns>Retorna el elemeto creado.</returns>
        public static T Create(T obj)
        {
            return Repo.Create(obj);
        }

        /// <summary>
        /// Método para CREAR un objeto/registro en la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="obj">Entidad para ser tratado como objeto.</param>
        /// <returns>Retorna el elemeto creado de forma asíncrona.</returns>
        public static Task<T> CreateAsync(T obj)
        {
            return Repo.CreateAsync(obj);
        }

        /// <summary>
        /// Método para CREAR un rango de objetos/registros en la base de datos.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <returns>Retorna los objetos creados.</returns>
        public static IEnumerable<T> AddRange(IEnumerable<T> obj)
        {
            return Repo.AddRange(obj);
        }

        /// <summary>
        /// Método para CREAR un Rango de registros en la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto de forma asíncrona.</param>
        /// <returns>Retorna los objetos creados de forma asíncrona.</returns>
        public static Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> obj)
        {
            return Repo.AddRangeAsync(obj);
        }
        #endregion
        #region 'Controlador READ'
        /// <summary>
        /// Método para obtener TODOS los objetos/registros.
        /// </summary>
        /// <returns>Retorna una lista que contiene todos los elementos de una tabla.</returns>
        public static IEnumerable<T> Read()
        {
            return Repo.Read();
        }

        /// <summary>
        /// Método para obtener TODOS los objetos/registros de forma asíncrona.
        /// </summary>
        /// <returns>Retorna una lista que contiene todos los elementos de una tabla de forma asíncrona.</returns>
        public static Task<IEnumerable<T>> ReadAsync()
        {
            return Repo.ReadAsync();
        }
        #endregion
        #region 'Controlador UPDATE'
        /// <summary>
        /// Método para ACTUALIZAR el objeto/registro en una tabla.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el objeto ingresado y/o el elemento existente; depende la condicion que se ejecute.</returns>
        public static T Update(T obj, int id)
        {
            return Repo.Update(obj, id);
        }

        /// <summary>
        /// Método para ACTUALIZAR el objeto/registro en una tabla de forma asíncrona.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el objeto ingresado y/o el elemento existente; depende la condicion que se ejecute de forma asíncrona.</returns>
        public static Task<T> UpdateAsync(T obj, int id)
        {
            return Repo.UpdateAsync(obj, id);
        }
        #endregion
        #region 'Controlador DELETE'
        /// <summary>
        /// Método para BORRAR un objeto/registro.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el elemento que ha sido borrado de la base de datos.</returns>
        public static T Delete(T obj, int id)
        {
            return Repo.Delete(obj, id);
        }

        /// <summary>
        /// Método para BORRAR un objeto/registro de forma asíncrona..
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el elemento que ha sido borrado de la base de datos de forma asíncrona.</returns>
        public static Task<T> DeleteAsync(T obj, int id)
        {
            return Repo.DeleteAsync(obj, id);
        }

        /// <summary>
        /// Método para BORRAR un rango de objetos/registros en la base de datos.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <returns>Retorna los objetos creados.</returns>
        public static IEnumerable<T> DeleteRange(IEnumerable<T> obj)
        {
            return Repo.DeleteRange(obj);
        }

        /// <summary>
        /// Método para BORRAR un Rango de registros en la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto de forma asíncrona.</param>
        /// <returns>Retorna los objetos creados de forma asíncrona.</returns>
        public static Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> obj)
        {
            return Repo.DeleteRangeAsync(obj);
        }
        #endregion

        #region 'Controlador COUNT'
        /// <summary>
        /// Método para Contar el numero de objeto/registro en cada tabla.
        /// </summary>
        /// <returns>Retorna el numero de elementos de una tabla.</returns>
        public static int Count()
        {
            return Repo.Count();
        }

        /// <summary>
        /// Método para Contar el numero de objeto/registro en cada tabla de forma asíncrona.
        /// </summary>
        /// <returns>Retorna el numero de elementos de una tabla de forma asíncrona.</returns>
        public static Task<int> CountAsync()
        {
            return Repo.CountAsync();
        }
        #endregion
        #region 'Controlador EXIST'

        /// <summary>
        /// Método para verificar la EXISTENCIA de un objeto/registro por medio de expresiones LAMBDA.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar un elemento.</param>
        /// <returns>Retorna una respuesta booleana.</returns>
        public static bool Exist(Expression<Func<T, bool>> matchitem)
        {
            return Repo.Exist(matchitem);
        }

        /// <summary>
        /// Método para verificar la EXISTENCIA de un objeto/registro por medio de expresiones LAMBDA de forma asíncrona.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar un elemento.</param>
        /// <returns>Retorna una respuesta booleana.</returns>
        public static async Task<bool> ExistAsync(Expression<Func<T, bool>> matchitem)
        {
            return await Repo.ExistAsync(matchitem);
        }
        
        #endregion
        #region 'Controlador FIND'
        /// <summary>
        /// Método para UBICAR un objeto/registro por medio de expresiones LAMBDA.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar un elemento.</param>
        /// <returns>Retorna el elemento que coincide con la expresion lambda.</returns>
        public static T Find(Expression<Func<T, bool>> matchitem)
        {
            return Repo.Find(matchitem);
        }

        /// <summary>
        /// Método para UBICAR un objeto/registro por medio de expresiones LAMBDA de forma asíncrona.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar un elemento.</param>
        /// <returns>Retorna el elemento que coincide con la expresion lambda de forma asíncrona.</returns>
        public static Task<T> FindAsync(Expression<Func<T, bool>> matchitem)
        {
            return Repo.FindAsync(matchitem);
        }

        /// <summary>
        /// Método para UBICAR todos objetos/registros por medio de expresiones LAMBDA.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar todos los elementos.</param>
        /// <returns>Retorna todos los elementos que coinciden con la expresion lambda.</returns>
        public static ICollection<T> FindAll(Expression<Func<T, bool>> matchitem)
        {
            return Repo.FindAll(matchitem);
        }

        /// <summary>
        /// Método para UBICAR todos objetos/registros por medio de expresiones LAMBDA de forma asíncrona.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar todos los elementos.</param>
        /// <returns>Retorna todos los elementos que coinciden con la expresion lambda de forma asíncrona.</returns>
        public static Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> matchitem)
        {
            return Repo.FindAllAsync(matchitem);
        }
        #endregion
        #region 'Controlador GET'
        /// <summary>
        /// Método para obtener un solo objeto/registro mediante el ID.
        /// </summary>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el elemento que coincide con el identificador.</returns>
        public static T Get(int id)
        {
            return Repo.Get(id);
        }

        /// <summary>
        /// Método para obtener un solo objeto/registro mediante el ID de forma asíncrona.
        /// </summary>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el elemento que coincide con el identificador de forma asíncrona.</returns>
        public static Task<T> GetAsync(int id)
        {
            return Repo.GetAsync(id);
        }
        #endregion
    }
}
