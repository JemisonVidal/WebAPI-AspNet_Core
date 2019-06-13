using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web;
using WebAPI_Aspnetcore.Controllers;
using WebAPI_Aspnetcore.Models;
namespace Movies_Test
{
    [TestClass]
    public class MoviesTest
    {
        [TestMethod]
        public void GetMoviesTest()
        {

            MoviesController moviesController = new MoviesController();
            var result = moviesController.Get("Waterworld").Result;

            Assert.IsNotNull(result);
           // Assert.IsInstanceOfType(result, typeof(MovieResponse));
            Assert.AreEqual(11, result.Total);




        }
    }
}

