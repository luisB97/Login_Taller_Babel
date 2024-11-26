using Login_Taller_Babel.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Login_Taller_Babel.Test
{
    public class Tests
    {

        [Test]
        public void IngresoCorrecto()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();//Buena practica es maximizar el navegador
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

            LoginPage login = new LoginPage(driver); //El driver definido en el test va a viajar a LoginPage

            login.IngresarCredenciales();

            driver.Close();//Cerrar el navegador
            driver.Quit();//Cerrar el ChromeDriver del administrador de tareas

        }

        [Test]
        public void IngresoIncorrecto()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();//Buena practica es maximizar el navegador
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

            driver.FindElement(By.Id("username")).SendKeys("tomsmith1");
            driver.FindElement(By.Id("password")).SendKeys("SuperSecretPassword!");
            driver.FindElement(By.CssSelector("#login > button")).Click();

            driver.Close();//Cerrar el navegador
            driver.Quit();//Cerrar el ChromeDriver del administrador de tareas

        }
    }
}