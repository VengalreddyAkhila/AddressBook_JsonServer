using AddressBookJsonServer;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace NunitUsingRest.test
{
    public class Tests
    {
        RestClient client;
        [SetUp]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }
        private IRestResponse GetResponse()
        {
            //arrange
            RestRequest request = new RestRequest("/Contacts", Method.GET);

            //act
            IRestResponse response = client.Execute(request);
            return response;

        }
        [Test]
        public void GETRequest_ReturnsContactList()
        {
            IRestResponse response = GetResponse();
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<Contacts> contactsList = JsonConvert.DeserializeObject<List<Contacts>>(response.Content);
            Assert.AreEqual(4, contactsList.Count);

            foreach (Contacts contact in contactsList)
            {
                System.Console.WriteLine(contact.FirstName + " " + contact.LastName + " " + contact.Address + " " + contact.City + " " + contact.State + " " + contact.ZipCode + " " + contact.PhoneNumber + " " + contact.Email);
            }
        }
    }
}