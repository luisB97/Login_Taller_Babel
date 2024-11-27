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
            var data = json.login_data();
            //String user = data.username;
            //String pass = data.password;

            login.IngresarCredenciales(user, pass);
            page.ElementoEsVisible(login.botonLogin);
            login.DarClickBotonLogin();
            page.ElementoEsVisible(login.botonLogout);
            //Assertions
            //Assert.That(login.botonLogout.Displayed);
            Assert.That(login.ValidarBoton());
        }
    }
}