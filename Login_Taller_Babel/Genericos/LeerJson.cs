using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Taller_Babel.Genericos
{
    public class LeerJson
    {
        public List<T> ReadJson<T>(String nombreJson, String jsonKey) //T indica que va a ser generico, NO puede ser otra letra porque es del lenguaje C#
        {
            /// <summary>
            /// Método que lee el json con una estructura generica
            /// </summary>
            /// <returns>El archivo json deserailizado</returns>
            try
            {
                var json = JsonConvert.DeserializeObject<Dictionary<String, List<T>>>(File.ReadAllText($@"..\..\..\Data\{nombreJson}.json"));
                //return json; //Sino es un diccionario
                return json[jsonKey]; //Se puede ser mas especifico, decir que en credenciales se va a mandar un objeto.
            }
            catch (FileNotFoundException)
            {
                throw new Exception($"No se encontró el archivo Json en la ruta: Data/{nombreJson}");
            }
            catch (JsonException ex)
            {
                throw new JsonException("El archivo Json esta corrupto: " + ex);
            }
        }
    }
}
