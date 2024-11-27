using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Taller_Babel.PageObject
{
    public class BasePage
    {
        public IWebDriver _driver; // _ significa que esta variable es privada solo de esta clase, nomenclatura de C#

        public BasePage(IWebDriver driver) //Esta pagina que es la misma va a recibir un driver de alguien más
        {
            _driver = driver;
        }
    }
}
