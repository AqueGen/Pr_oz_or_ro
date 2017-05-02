using System.Web.Mvc;
using Kapitalist.Web.Client.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Kapitalist.Web.Client.ControllerTests {
	[TestClass]
	public class HomeControllerTest {
		[TestMethod]
		public void Index() {
			// Arrange
			HomeController controller = new HomeController();

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}
