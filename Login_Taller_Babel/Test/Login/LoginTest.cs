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
        //El tipo de retorno va a ser una interfaz que va a representar una colecci�n de objetos la que esta en credenciales.
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
                test.AddScreenCaptureFromBase64String(captura.CapturarPantalla(driver));
                Assert.Fail($"No se encontr� el elemento: {ex.Message}");
            }
            catch (AssertionException ex)
            {
                test.Log(Status.Fail, $"Fallo de aserci�n: {ex.Message}");
                test.AddScreenCaptureFromBase64String(captura.CapturarPantalla(driver));
                Assert.Fail($"Fallo de aserci�n: {ex.Message}");
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"Error en la ejecuci�n del test: {ex.Message}");
                test.AddScreenCaptureFromBase64String(captura.CapturarPantalla(driver));
                Assert.Fail($"Error en la ejecuci�n del test: {ex.Message}");
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
            test.Log(Status.Pass, $"Se le di� click al boton login");
        }

        private void ValidarIngreso()
        {
            //Assertions
            Assert.That(driver.Url.Equals("https://the-internet.herokuapp.com/secure"), "La URL no corresponde a la p�gina de inicio esperada");
            Assert.That(login.botonLogout.Displayed, "El bot�n de logout no se mostr� correctamente");
            Assert.That(login.ValidarBoton());
            test.Log(Status.Pass, $"Se ingreso correctamente");
        }

        private void RealizarLogout()
        {
            //Assertions
            page.ElementoEsVisible(login.botonLogout);
            login.ClickBotonLogout();
            test.Log(Status.Pass, "Se di� click en el bot�n Logout");
        }
    }
}