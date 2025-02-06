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
                return json.login_data().Select(data => new TestCaseData(data.username, data.password));
            }
        }

        [Test]
        [TestCaseSource(nameof(TestData))]
        public void IngresoCorrecto(string user, string pass)
        {
            test = reports.CreateTest("Validando ingreso correcto");

            try
            {
                login.IngresarCredenciales(user, pass);
                test.Log(Status.Pass, $"Se ingresaron las credenciales: {user}, {pass}");
                page.ElementoEsVisible(login.botonLogin);
                test.Log(Status.Pass, $"El boton login es visible");
                login.DarClickBotonLogin();
                test.Log(Status.Pass, $"Se le dió click al boton login");

                //Assertions
                //Assert.That(driver.Url.Equals(""));
                //Assert.That(login.botonLogout.Displayed);
                page.ElementoEsVisible(login.botonLogout);
                Assert.That(login.ValidarBoton());
                test.Log(Status.Pass, $"Se ingreso correctamente");

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
                Assert.Fail("Cayo en el catch");
            }
        }
    }
}