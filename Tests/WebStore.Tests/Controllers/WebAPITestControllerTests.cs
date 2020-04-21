using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Controllers;
using WebStore.Interfaces.Api;

using Assert = Xunit.Assert;


namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class WebAPITestControllerTests
    {
        [TestMethod]
        public async Task Index_Retutns_View_Values()
        {
            var expectedResult = new[] { "1", "2", "3" };

            var valueServiceMock = new Mock<IValuesService>();

            valueServiceMock
                .Setup(x => x.GetAsync())
                .ReturnsAsync(expectedResult);

            var controller = new WebAPITestController(valueServiceMock.Object);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.Model);

            Assert.Equal(expectedResult.Length, model.Count());
        }
    }
}
