﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Taller_Babel.Genericos
{
    public class LeerJson
    {
        public List<POCO.LoginData> login_data()
        {
            var json = JsonConvert.DeserializeObject<Dictionary<String,List<POCO.LoginData>>>(File.ReadAllText(@"..\..\..\Data\login.json"));
            //return json; //Sino es un diccionario
            return json["credenciales"]; //Se puede ser mas especifico, decir que en credenciales se va a mandar un objeto.
        }
    }
}