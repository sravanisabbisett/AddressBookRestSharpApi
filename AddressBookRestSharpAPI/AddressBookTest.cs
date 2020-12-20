using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace AddressBookRestSharpAPI
{
    [TestClass]
    public class AddressBookTest
    {

        RestClient client;
        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:3000");
        }

        private IRestResponse GetPersonList()
        {
            RestRequest request = new RestRequest("/person", Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }
        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestMethod]
        public void GetPersonCount_ShouldTeturnCount()
        {
            IRestResponse response = GetPersonList();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Person> dataResponse = JsonConvert.DeserializeObject<List<Person>>(response.Content);
            Assert.AreEqual(5, dataResponse.Count);
        }
    }
}
