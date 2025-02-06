using Login_Taller_Babel.Genericos;
using Login_Taller_Babel.PageObject.Login;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using Login_Taller_Babel.PageObject;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Login_Taller_Babel.Test
{
    public class BaseTest
    {
        //declarar variables globales para que la vean todos los métodos de esta clase
        public IWebDriver driver;
        public LoginPage login;
        public LeerJson json;
        public string baseURL = "https://the-internet.herokuapp.com/login";
        public WebDriverWait wait;
        public BasePage page;
        public TomarCaptura captura;

        //reportes
        public static ExtentTest test;
        public static ExtentReports reports;

        //Los hooks ejecutan ciertos trozos de código en ciertas partes de mi test, con esto no tengo duplicidad de código y reutilizo código
        [SetUp] //Ejecuta el trozo de código que esta en el SetUp y lo ejecuta antes de cada prueba
        public void IniciarNavegador() //void porque no retorna ningún parámetro
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();//Buena practica es maximizar el navegador
            driver.Navigate().GoToUrl(baseURL);

            login = new LoginPage(driver, wait); //El driver definido en el test va a viajar a LoginPage
            page = new BasePage(driver, wait);
            captura = new TomarCaptura();
        }

        [TearDown]
        public void CerrarNavegador()
        {
            driver.Close();//Cerrar el navegador
            driver.Quit();//Cerrar el ChromeDriver del administrador de tareas
        }

        [OneTimeSetUp] //Solo una vez antes de mi set de pruebas
        public void IniciarReporte()
        {
            reports = new ExtentReports(); //Inicializar reporte
            ExtentSparkReporter htmlreporter = new ExtentSparkReporter(@"..\..\Reportes\index.html"); //Vaya a crear el html en la ruta
            reports.AttachReporter(htmlreporter);
            htmlreporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
        }

        [OneTimeTearDown] //Solo una vez después de mi set de pruebas
        public void GenerarReporte()
        {
            reports.Flush(); //Generar el reporte html
        }
    }
}
