using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Model
{
    public class GenericJson<T> where T : class
    {
        /// <summary>
        /// Referencia : https://stackoverflow.com/questions/36070123/c-sharp-httpwebrequest-returns-error-403 | Configuracion HTTPWebRequest
        ///  Metodo 'GET' APIREST
        /// </summary>
        /// <param name="url">Es la primera parte de la cadena de la url.</param>
        /// <param name="token">El token que va incrustado en la url.</param>
        /// <param name="urlend">Es la ultima parte de la cadena de la url.</param>
        /// <returns>Retorna la informacion en JSON.</returns>
        public T Get(string url, string token, string urlend)
        {
            T obj = null;
            var request = (HttpWebRequest)WebRequest.Create(url + token + urlend);
            request.ContentType = "application/json";
            request.UserAgent = "[AnyWordThatIsMoreThan5Char]";
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        var json = reader.ReadToEnd();
                        obj = JsonConvert.DeserializeObject<T>(json);
                    }
            return obj;
        }

        /// <summary>
        ///  Metodo 'GETAsync' API REST
        /// </summary>
        /// <param name="url">Es la primera parte de la cadena de la url.</param>
        /// <param name="token">El token que va incrustado en la url.</param>
        /// <param name="urlend">Es la ultima parte de la cadena de la url.</param>
        /// <returns>Retorna la informacion en JSON de forma asíncrona.</returns>
        public async Task<T> GetAsync(string url, string token, string urlend)
        {
            T obj = null;
            var request = (HttpWebRequest)WebRequest.Create(url + token + urlend);
            request.ContentType = "application/json";
            request.UserAgent = "[AnyWordThatIsMoreThan5Char]";
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        var json = reader.ReadToEndAsync();
                        obj = JsonConvert.DeserializeObject<T>(await json);
                    }
            return obj;
        }
    }
}
