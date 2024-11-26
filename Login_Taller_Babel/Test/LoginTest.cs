using Login_Taller_Babel.Genericos;
using Login_Taller_Babel.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections;

namespace Login_Taller_Babel.Test
{
    public class Tests
    {
        //declarar variables globales para que la vean todos los m�todos de esta clase
        public IWebDriver driver;
        public LoginPage login;
        public LeerJson json;
        public String baseURL = "https://the-internet.herokuapp.com/login";

        //Los hooks ejecutan ciertos trozos de c�digo en ciertas partes de mi test, con esto no tengo duplicidad de c�digo y reutilizo c�digo
        [SetUp] //Ejecuta el trozo de c�digo que esta en el SetUp y lo ejecuta antes de cada prueba
        public void IniciarNavegador() //void porque no retorna ning�n par�metro
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();//Buena practica es maximizar el navegador
            driver.Navigate().GoToUrl(baseURL);

            login = new LoginPage(driver); //El driver definido en el test va a viajar a LoginPage

            //json = new LeerJson();
        }

        [TearDown]
        public void CerrarNavegador() 
        {
            driver.Close();//Cerrar el navegador
            driver.Quit();//Cerrar el ChromeDriver del administrador de tareas
        }

        //El tipo de retorno va a ser una interfaz que va a representar una colecci�n de objetos la que esta en credenciales.
        //Lo que va hacer NUnit va a tomar este objeto de tipo IEnumerable y lo va a transformar en una lista de datos.
        public static IEnumerable TestData
        {
            get
            {
                var json = new LeerJson();
                return json.login_data().Select(data => new TestCaseData(data.username, data.password));
            }
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        public void IngresoCorrecto(String user, String pass)
        {
            var data = json.login_data();
            //String user = data.username;
            //String pass = data.password;

            login.IngresarCredenciales(user, pass);
        }
    }
}