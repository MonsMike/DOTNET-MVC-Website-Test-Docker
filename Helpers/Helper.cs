using System.Text.Json;
using MVCWebsiteTest_Docker.Models;

namespace MVCWebsiteTest_Docker.Helpers;

public static class Helper
{
    public static string GetApi(string baseUrl = "", string endpoint = "")
    {
        if (string.IsNullOrEmpty(baseUrl))
            return "";

        using var client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl + endpoint);
        
        var response = client.GetAsync("").Result;
        var result = response.Content.ReadAsStringAsync().Result;

        return result;
        
        // API KEY EXAMPLE
        // client.DefaultRequestHeaders.Add("api-key", "11111111");
        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // POST EXAMPLE:
        // var bodyContent = new StringContent("{\"someProperty\":\"someValue\"}", Encoding.UTF8, "application/json"); // can instead serialize custom obj
        // var response = client.PostAsync(baseUrl + endpoint, bodyContent).Result;
    }
}