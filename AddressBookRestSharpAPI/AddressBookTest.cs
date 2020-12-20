using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        /// <summary>
        /// Addeds the name of the multiple employee should return employee.
        /// </summary>
        [TestMethod]
        public void AddedMultipleEmployeeShouldReturnEmployeeName()
        {
            List<Person> person = new List<Person>();
            person.Add(new Person { firstname = "Badarinath", lastname = "sabbisetti", address = "VyjayanthiTraders", city = "Bantumilli", state = "Andharpradesh", zip = 521324, mobileNumber = 9295702642 });
            person.Add(new Person { firstname = "Akhilesh", lastname = "sabbisetti", address = "Perugudi", city = "Chennai", state = "Tamilnadu", zip = 521724, mobileNumber = 7207321696});

            foreach(Person personData in person)
            {
                RestRequest request = new RestRequest("/person", Method.POST);
                JObject jObjectBody = new JObject();
                jObjectBody.Add("firstname", personData.firstname);
                jObjectBody.Add("lastname", personData.lastname);
                jObjectBody.Add("address", personData.address);
                jObjectBody.Add("city", personData.city);
                jObjectBody.Add("state", personData.state);
                jObjectBody.Add("zip", personData.zip);
                jObjectBody.Add("mobileNumber", personData.mobileNumber);
                request.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);
                //act
                IRestResponse response = client.Execute(request);
                //assert
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                Person dataResponse = JsonConvert.DeserializeObject<Person>(response.Content);
                Assert.AreEqual(personData.firstname, dataResponse.firstname);
            }

        }

        /// <summary>
        /// Updates the person should return updated data.
        /// </summary>
        [TestMethod]
        public void UpdatePersonShouldReturnUpdatedData()
        {
            RestRequest request = new RestRequest("/person/1", Method.PUT);
            JObject jObjectBody = new JObject();
            jObjectBody.Add("firstname", "sravani");
            jObjectBody.Add("lastname", "sabbisetti");
            jObjectBody.Add("address", "GandhiChowk");
            jObjectBody.Add("city", "Machilipatnam");
            jObjectBody.Add("state", "AndhraPradesh");
            jObjectBody.Add("zip", 521324);
            jObjectBody.Add("mobilenumber", 8712443377);
            request.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);
            //act
            IRestResponse response = client.Execute(request);
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Person dataResponse = JsonConvert.DeserializeObject<Person>(response.Content);
            Assert.AreEqual(dataResponse.city, "Machilipatnam");

        }
    }
}
