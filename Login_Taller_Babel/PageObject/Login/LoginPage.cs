using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Taller_Babel.PageObject.Login
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { } //Inicializar clase dentro de la clase page para poder utilizar los elementos de esa clase

        //Privados para que solo esta clase tenga acceso a estos localizadores. Solo lectura porque la idea es que el valor de esos elementos no cambie durante el test
        private readonly By _txtUserName = By.Id("username");
        private readonly By _txtPassword = By.Id("password");
        private readonly By _btnLogin = By.CssSelector("#login > button");
        private readonly By _btnLogout = By.CssSelector("#content > div > a > i");

        public IWebElement username => _driver.FindElement(_txtUserName);
        public IWebElement password => _driver.FindElement(_txtPassword);
        public IWebElement botonLogin => _driver.FindElement(_btnLogin);
        public IWebElement botonLogout => _driver.FindElement(_btnLogout);

        public void IngresarCredenciales(string user, string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            botonLogin.Click();
        }

        public bool ValidarBoton() 
        {
            bool seMuestra = botonLogout.Displayed;
            return seMuestra;
        }

    }
}
