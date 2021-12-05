using NUnit.Framework;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace ReqresTestTask
{
    public class OnLoginShould
    {

        public IRestClient Client { get; set; }
        public const string Url = "https://reqres.in/api/login";

        [SetUp]
        public void Setup()
        {
            Client = new RestClient(Url);
        }

        [Test]
        [Category("Positive")]
        public void GetToken()
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(
                new
                {
                    email = "eve.holt@reqres.in",
                    password = "cityslicka"
                }
            );

            var response = Client.Execute(request);
            LoginResponse responseBody = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            Assert.That(responseBody.token, Is.EqualTo("QpwL5tke4Pnpja7X4"));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ContentType, Is.EqualTo("application/json; charset=utf-8"));
        }

        [Test]
        [Category("Negative")]
        public void Return400WhenPasswordMissing()
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(
                new
                {
                    email = "eve.holt@reqres.in"
                }
            );

            var response = Client.Execute(request);
            LoginResponse responseBody = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBody.error, Is.EqualTo("Missing password"));
        }

        [Test]
        [Category("Negative")]
        public void Return400WhenEmailMissing()
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(
                new
                {
                    password = "cityslicka"
                }
            );

            var response = Client.Execute(request);
            LoginResponse responseBody = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBody.error, Is.EqualTo("Missing email or username"));

        }

        [Test]
        [Category("Negative")]
        public void Return400WhenUserNotFound()
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(
                new
                {
                    email = "missing",
                    password = "cityslicka"
                }
            );

            var response = Client.Execute(request);
            LoginResponse responseBody = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBody.error, Is.EqualTo("user not found"));

        }

        [Test]
        [Category("Negative")]
        public void NotReturnTokenForGETRequest()
        {
            var request = new RestRequest(Method.GET);

            var response = Client.Execute(request);
            LoginResponse responseBody = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            Assert.That(responseBody.token, Is.Null);
            
        }


    }
}