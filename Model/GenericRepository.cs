using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    ///  Método de clase generica para el manejo de Entidades utilizando un Tipo(T) de Entidad Generica.
    /// </summary>
    /// <typeparam name="T">Es el tipo de Entidad que utilizara la clase generica.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region 'Instancia de la Entidad'
        /// <summary>
        ///  Aqui se recibe las Entidades del Modelo que se haya sincronizado.
        /// </summary>
        readonly Entities _entity = new Entities();
        #endregion
        #region 'Var Global'
        /// <summary>
        /// Variable Global
        /// </summary>
        string _errorMessage = string.Empty;
        #endregion

        #region 'Método CREATE'
        /// <summary>
        ///  Método para CREAR un objeto/registro en la base de datos.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <returns>Retorna el elemeto creado.</returns>
        public T Create(T obj)
        {
            try
            {
                //Verificar validacion del error; tal vez no sea necesaria ya que con las anotaciones podria ser suficiente.
                if (!Validar(obj))
                _entity.Set<T>().Add(obj);
                _entity.SaveChanges();
                return obj;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        /// <summary>
        ///  Método para CREAR un objeto/registro en la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <returns>Retorna el elemeto creado de forma asíncrona.</returns>
        public async Task<T> CreateAsync(T obj)
        {
            try
            {
                if (!Validar(obj))
                    _entity.Set<T>().Add(obj);
                await _entity.SaveChangesAsync();
                return obj;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }
        
        /// <summary>
        ///  Método para CREAR un rango de objetos/registros en la base de datos.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <returns>Retorna los objetos creados.</returns>
        public IEnumerable<T> AddRange(IEnumerable<T> obj)
        {
            try
            {
                if (!ValidarR(obj))
                _entity.Set<T>().AddRange(obj);
                _entity.SaveChanges();
                return obj;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }


        /// <summary>
        ///  Método para CREAR un Rango de objetos/registros en la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto de forma asíncrona.</param>
        /// <returns>Retorna los objetos creados de forma asíncrona.</returns>
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> obj)
        {
            try
            {
                if (!ValidarR(obj))
                _entity.Set<T>().AddRange(obj);
                await _entity.SaveChangesAsync();
                return obj;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }
        #endregion
        #region 'Método READ'
        /// <summary>
        ///  Método para obtener TODOS los objetos/registros.
        /// </summary>
        /// <returns>Retorna una lista que contiene todos los elementos de una tabla.</returns>
        public IEnumerable<T> Read()
        {
            return _entity.Set<T>().ToList();
        }

        /// <summary>
        ///  Método para obtener TODOS los objetos/registros de forma asíncrona.
        /// </summary>
        /// <returns>Retorna una lista que contiene todos los elementos de una tabla de forma asíncrona.</returns>
        public async Task<IEnumerable<T>> ReadAsync()
        {
            return await _entity.Set<T>().ToListAsync();
        }
        #endregion
        #region 'Método UPDATE'
        /// <summary>
        ///  Método para ACTUALIZAR el objeto/registro en una tabla.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el objeto ingresado y/o el elemento existente; depende la condicion que se ejecute.</returns>
        public T Update(T obj, int id)
        {
            T exist = _entity.Set<T>().Find(id);
            try
            {
                if (exist == null) return obj;
                if (!Validar(obj))
                    _entity.Entry(exist).CurrentValues.SetValues(obj);
                _entity.SaveChanges();
                return exist;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        /// <summary>
        ///  Método para ACTUALIZAR el objeto/registro en una tabla de forma asíncrona.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el objeto ingresado y/o el elemento existente; depende la condicion que se ejecute de forma asíncrona.</returns>
        public async Task<T> UpdateAsync(T obj, int id)
        {
            T exist = _entity.Set<T>().Find(id);
            try
            {
                if (exist == null) return obj;
                if (!Validar(obj))
                    _entity.Entry(exist).CurrentValues.SetValues(obj);
                await _entity.SaveChangesAsync();
                return exist;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }
        #endregion
        #region 'Método DELETE'
        /// <summary>
        ///  Método para BORRAR un objeto/registro.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el elemento que ha sido borrado de la base de datos.</returns>
        public T Delete(T obj, int id)
        {
            T exist = _entity.Set<T>().Find(id);
            try
            {
                if (exist == null) return obj;
                if (Validar(obj)) return obj;
                _entity.Set<T>().Remove(obj);
                _entity.SaveChanges();
                return obj;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        /// <summary>
        ///  Método para BORRAR un objeto/registro de forma asíncrona..
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el elemento que ha sido borrado de la base de datos de forma asíncrona.</returns>
        public async Task<T> DeleteAsync(T obj, int id)
        {
            T exist = _entity.Set<T>().Find(id);
            try
            {
                if (exist == null) return obj;
                if (Validar(obj)) return obj;
                _entity.Set<T>().Remove(obj);
                await _entity.SaveChangesAsync();
                return obj;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        /// <summary>
        ///  Método para ELIMINAR un rango de objetos/registros en la base de datos.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <returns>Retorna los objetos creados.</returns>
        public IEnumerable<T> DeleteRange(IEnumerable<T> obj)
        {
            try
            {
                if (!ValidarR(obj))
                    _entity.Set<T>().RemoveRange(obj);
                _entity.SaveChanges();
                return obj;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        /// <summary>
        ///  Método para ELIMINAR un Rango de registros en la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto de forma asíncrona.</param>
        /// <returns>Retorna los objetos creados de forma asíncrona.</returns>
        public async Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> obj)
        {
            try
            {
                if (!ValidarR(obj))
                    _entity.Set<T>().RemoveRange(obj);
                await _entity.SaveChangesAsync();
                return obj;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationError in dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage +=
                        $"\nPropiedad: {validationError.PropertyName} \nError: {validationError.ErrorMessage}" + Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }
        #endregion

        #region 'Método COUNT'
        /// <summary>
        ///  Método para Contar el numero de objeto/registro en cada tabla.
        /// </summary>
        /// <returns>Retorna el numero de elementos de una tabla.</returns>
        public int Count()
        {
            return _entity.Set<T>().Count();
        }

        /// <summary>
        ///  Método para Contar el numero de objeto/registro en cada tabla de forma asíncrona.
        /// </summary>
        /// <returns>Retorna el numero de elementos de una tabla de forma asíncrona.</returns>
        public async Task<int> CountAsync()
        {
            return await _entity.Set<T>().CountAsync();
        }
        #endregion
        #region 'Método EXIST'

        /// <summary>
        ///  Método para verificar la EXISTENCIA de un objeto/registro por medio de expresiones LAMBDA.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar un elemento.</param>
        /// <returns>Retorna una respuesta booleana.</returns>
        public bool Exist(Expression<Func<T, bool>> matchitem)
        {
            return _entity.Set<T>().SingleOrDefault(matchitem) != null;
        }

        /// <summary>
        ///  Método para verificar la EXISTENCIA de un objeto/registro por medio de expresiones LAMBDA de forma asíncrona.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar un elemento.</param>
        /// <returns>Retorna una respuesta booleana.</returns>
        public async Task<bool> ExistAsync(Expression<Func<T, bool>> matchitem)
        {
            return await _entity.Set<T>().SingleOrDefaultAsync(matchitem) != null;
        }

        #endregion
        #region 'Método FIND'
        /// <summary>
        ///  Método para UBICAR un objeto/registro por medio de expresiones LAMBDA.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar un elemento.</param>
        /// <returns>Retorna el elemento que coincide con la expresion lambda.</returns>
        public T Find(Expression<Func<T, bool>> matchitem)
        {
            return _entity.Set<T>().SingleOrDefault(matchitem);
        }

        /// <summary>
        ///  Método para UBICAR un objeto/registro por medio de expresiones LAMBDA de forma asíncrona.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar un elemento.</param>
        /// <returns>Retorna el elemento que coincide con la expresion lambda de forma asíncrona.</returns>
        public async Task<T> FindAsync(Expression<Func<T, bool>> matchitem)
        {
            return await _entity.Set<T>().SingleOrDefaultAsync(matchitem);
        }

        /// <summary>
        ///  Método para UBICAR todos objetos/registros por medio de expresiones LAMBDA.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar todos los elementos.</param>
        /// <returns>Retorna todos los elementos que coinciden con la expresion lambda.</returns>
        public ICollection<T> FindAll(Expression<Func<T, bool>> matchitem)
        {
            return _entity.Set<T>().Where(matchitem).ToList();
        }

        /// <summary>
        ///  Método para UBICAR todos objetos/registros por medio de expresiones LAMBDA de forma asíncrona.
        /// </summary>
        /// <param name="matchitem">Expresion lambda para ubicar todos los elementos.</param>
        /// <returns>Retorna todos los elementos que coinciden con la expresion lambda de forma asíncrona.</returns>
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> matchitem)
        {
            return await _entity.Set<T>().Where(matchitem).ToListAsync();
        }
        #endregion
        #region 'Método GET'
        /// <summary>
        ///  Método para obtener un solo objeto/registro mediante el ID.
        /// </summary>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el elemento que coincide con el identificador.</returns>
        public T Get(int id)
        {
            return _entity.Set<T>().Find(id);
        }

        /// <summary>
        ///  Método para obtener un solo objeto/registro mediante el ID de forma asíncrona.
        /// </summary>
        /// <param name="id">identificador para cotejar el elemento.</param>
        /// <returns>Retorna el elemento que coincide con el identificador de forma asíncrona.</returns>
        public async Task<T> GetAsync(int id)
        {
            return await _entity.Set<T>().FindAsync(id);
        }
        #endregion


        #region 'Validaciones'
        /// <summary>
        ///  Método para  VALIDAR(1) la recepción de los objetos/registros .
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <returns>Retorna una respuesta booleana 'TRUE'o 'FALSE'.</returns>
        internal bool Validar(T obj)
        {
            return obj == null;
            throw new ArgumentNullException("obj");
            //Revisar si es mejor la Salida: Excepcion
        }

        /// <summary>
        ///  Método para  VALIDAR(n) la recepción de los objetos/registros .
        /// </summary>
        /// <param name="obj">Entidad generica para ser tratado como objeto.</param>
        /// <returns>Retorna una respuesta booleana 'TRUE'o 'FALSE'.</returns>
        internal bool ValidarR(IEnumerable<T> obj)
        {
            return obj == null;
            throw new ArgumentNullException("obj");
            //Revisar si es mejor la Salida: Excepcion
        }
        #endregion
    }
}