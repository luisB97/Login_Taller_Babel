using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Taller_Babel.Genericos
{
    public class TomarCaptura
    {
        public String CapturarPantalla(IWebDriver driver)
        {
			String captura = "";
			try
			{
				ITakesScreenshot screenshotdriver = driver as ITakesScreenshot;
				Screenshot screenshot = screenshotdriver.GetScreenshot();
				//screenshot.SaveAsFile("captura1");
				captura = screenshot.AsBase64EncodedString;
			}
			catch (Exception ex)
			{
				Console.WriteLine("No se pudo guardar la imagen: " + ex);
			}
			return captura;
        }
    }
}
