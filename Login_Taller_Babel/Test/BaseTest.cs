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
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Login_Taller_Babel.Test
{
    public class BaseTest
    {
        //declarar variables globales para que la vean todos los métodos de esta clase
        public IWebDriver driver;
        public LoginPage login;
        public LeerJson json;
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
            //Se agrega lectura json para navegador
            json = new LeerJson();
            var configData = json.ReadJson<POCO.ConfigData>("config", "configuracion");
            String baseURL = configData[0].baseURL; //configData es un objeto por eso no se puede dar . seguido de la variable, hay que indicar la posición y luego . y variable
            String browser = configData[0].browser;
            int timeout = configData[0].timeout;
            driver = IniciarDriver(browser);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
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
            //Manejo de directorios, ruta dinámica del reporte
            String directorioActual = AppDomain.CurrentDomain.BaseDirectory;
            String directorioProyecto = Directory.GetParent(directorioActual).Parent.Parent.Parent.FullName;
            String directorioReporte = Path.Combine(directorioProyecto, "Reportes");
            String nombreReporte = Path.Combine(directorioReporte, "index.html");
            //Fin ruta dinámica del reporte

            reports = new ExtentReports(); //Inicializar reporte
            ExtentSparkReporter htmlreporter = new ExtentSparkReporter(nombreReporte);
            reports.AttachReporter(htmlreporter);
            htmlreporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
        }

        [OneTimeTearDown] //Solo una vez después de mi set de pruebas
        public void GenerarReporte()
        {
            reports.Flush(); //Generar el reporte html
        }
        
        //private porque solo este método debería leer esta función
        private IWebDriver IniciarDriver(String browser)
        {
            return browser.ToLower() switch
            {
                "chrome" => new ChromeDriver(ConfigurarChromeOpciones()),
                "edge" => new EdgeDriver(ConfigurarEdgeOpciones()),
                "firefox" => new FirefoxDriver(ConfigurarFirefoxOpciones()),
                _ => throw new Exception($"El navegador: {browser} no es compatible")
            };
        }

        private ChromeOptions ConfigurarChromeOpciones()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            return options;
        }

        private FirefoxOptions ConfigurarFirefoxOpciones()
        {
            var options = new FirefoxOptions();
            options.AddArgument("--headless");
            return options;
        }

        private EdgeOptions ConfigurarEdgeOpciones()
        {
            var options = new EdgeOptions();
            options.AddArgument("headless");
            return options;
        }
    }
}
