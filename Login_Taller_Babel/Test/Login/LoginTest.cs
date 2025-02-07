using AventStack.ExtentReports;
using Login_Taller_Babel.Genericos;
using Login_Taller_Babel.PageObject.Login;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections;

namespace Login_Taller_Babel.Test.Test
{
    public class Tests : BaseTest //Herencia
    {
        //El tipo de retorno va a ser una interfaz que va a representar una colección de objetos la que esta en credenciales.
        //Lo que va hacer NUnit va a tomar este objeto de tipo IEnumerable y lo va a transformar en una lista de datos.
        public static IEnumerable TestData
        {
            get
            {
                var json = new LeerJson();
                var loginData = json.ReadJson<POCO.LoginData>("login", "credenciales");
                return loginData.Select(data => new TestCaseData(data.username, data.password));
            }
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        public void IngresoCorrecto(string user, string pass)
        {
            test = reports.CreateTest("Validando ingreso correcto");

            try
            {
                //Paso 1
                IngresarCredenciales(user, pass);

                //Paso 2
                ValidarIngreso();

                //Paso3
                RealizarLogout();

            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"No se encontro el elemento: {ex.Message}");
                captura.CapturarPantalla(driver);
                test.Log(Status.Fail, "Fallo la prueba: " + ex);
                Assert.Fail(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la ejecución: " + ex);
                captura.CapturarPantalla(driver);
                test.Log(Status.Fail, "Fallo la prueba: " + ex);
                Assert.Fail("Cayo en el catch");
            }
        }

        private void IngresarCredenciales(String user, String pass)
        {
            login.IngresarCredenciales(user, pass);
            test.Log(Status.Pass, $"Se ingresaron las credenciales: {user}, {pass}");
            //diferentes esperas
            page.ElementoEsVisible(login.botonLogin);
            test.Log(Status.Pass, $"El boton login es visible");
            login.DarClickBotonLogin();
            test.Log(Status.Pass, $"Se le dió click al boton login");
        }

        private void ValidarIngreso()
        {
            //Assertions
            Assert.That(driver.Url.Equals("https://the-internet.herokuapp.com/secure"), "La URL no corresponde a la página de inicio esperada");
            Assert.That(login.botonLogout.Displayed, "El botón de logout no se mostró correctamente");
            Assert.That(login.ValidarBoton());
            test.Log(Status.Pass, $"Se ingreso correctamente");
        }

        private void RealizarLogout()
        {
            //Assertions
            page.ElementoEsVisible(login.botonLogout);
            login.ClickBotonLogout();
            test.Log(Status.Pass, "Se dió click en el botón Logout");
        }
    }
}