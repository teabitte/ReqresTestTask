using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace ReqresTestTask
{
    class GetProductsShould
    {
        
        public const string Url = "https://reqres.in/api/products";
        public IRestClient Client { get; set; }


        [SetUp]
        public void Setup()
        {
            Client = new RestClient(Url);
        }

        
        [Test]
        [Category("Positive")]
        [TestCase("")]
        [TestCase("?page=2")]
        
        public void ReturnAllProductsOf2000OrNewer(string pageNum)
        {
            var request = new RestRequest(pageNum, Method.GET);
            var response = Client.Execute(request);
            ProductsResponseBody responseBody = JsonConvert.DeserializeObject<ProductsResponseBody>(response.Content);
            
            Assert.That(responseBody.data, Has.Exactly(responseBody.data.Length).Matches<Data>(
                d => d.year >= 2000), "Not all products of year 2000 or later!");

        }
    }
}
