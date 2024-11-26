using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Taller_Babel.PageObject
{
    public class LoginPage
    {
        public IWebDriver _driver; // _ significa que esta variable es privada solo de esta clase, nomenclatura de C#

        public LoginPage(IWebDriver driver) //Esta pagina que es la misma va a recibir un driver de alguien más
        {
            _driver = driver;
        }

        //Privados para que solo esta clase tenga acceso a estos localizadores. Solo lectura porque la idea es que el valor de esos elementos no cambie durante el test
        private readonly By _txtUserName = By.Id("username");
        private readonly By _txtPassword = By.Id("password");
        private readonly By _btnLogin = By.CssSelector("#login > button");

        public IWebElement username => _driver.FindElement(_txtUserName);
        public IWebElement password => _driver.FindElement(_txtPassword);
        public IWebElement botonLogin => _driver.FindElement(_btnLogin);

        public void IngresarCredenciales(String user, String pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            botonLogin.Click();
        }

    }
}
