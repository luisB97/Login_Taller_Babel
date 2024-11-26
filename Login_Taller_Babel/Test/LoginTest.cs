using Login_Taller_Babel.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Login_Taller_Babel.Test
{
    public class Tests
    {
        //variables globales para que la vean todos los m�todos de esta clase
        public IWebDriver driver;
        public LoginPage login;
        public String baseURL = "https://the-internet.herokuapp.com/login";

        //Los hooks ejecutan ciertos trozos de c�digo en ciertas partes de mi test, con esto no tengo duplicidad de c�digo y reutilizo c�digo
        [SetUp] //Ejecuta el trozo de c�digo que esta en el SetUp y lo ejecuta antes de cada prueba
        public void IniciarNavegador() //void porque no retorna ning�n par�metro
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();//Buena practica es maximizar el navegador
            driver.Navigate().GoToUrl(baseURL);

            login = new LoginPage(driver); //El driver definido en el test va a viajar a LoginPage
        }

        [TearDown]
        public void CerrarNavegador() 
        {
            driver.Close();//Cerrar el navegador
            driver.Quit();//Cerrar el ChromeDriver del administrador de tareas
        }

        [Test]
        public void IngresoCorrecto()
        {
            login.IngresarCredenciales();

        }

        [Test]
        public void IngresoIncorrecto()
        {
            login.IngresarCredenciales();
        }
    }
}