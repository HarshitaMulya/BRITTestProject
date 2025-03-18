using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace BRITTestProject;

public class APITest
{
    private RestClient client;
    private const string BaseUrl = "https://api.restful-api.dev/";
    [SetUp]
    public void Setup()
    {
        client = new RestClient(BaseUrl);
    }

    [Test]
    public void VerifyPatchRequest()
    {
        int objectId = 7; 

        // Prepare the PATCH request
        var request = new RestRequest($"objects/{objectId}", Method.PATCH);
        request.AddJsonBody(new { name = "Apple MacBook Pro 16 (Updated Name)" }); 

        // Send the request
        var response = client.Execute(request);

        // Assert the response status code is 200 OK
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        // Deserialize the response and verify if the object is updated
        dynamic responseData = JsonConvert.DeserializeObject(response.Content);
        Assert.AreEqual("Apple MacBook Pro 16 (Updated Name)", responseData.name.ToString());
    }
}
