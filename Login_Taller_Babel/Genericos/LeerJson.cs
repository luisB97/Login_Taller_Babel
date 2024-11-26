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
        public POCO.LoginData login_data()
        {
            var json = JsonConvert.DeserializeObject<POCO.LoginData>(File.ReadAllText(@"..\..\..\Data\login.json"));
            return json;
        }
    }
}
