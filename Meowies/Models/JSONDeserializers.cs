using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Meowies.Models;

public class JSONDeserializers
{
    public static async Task<MovieItem?>? GetBmAsync(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var bm = JsonConvert.DeserializeObject<MovieItem>(json);
            return bm;
        }

        throw new Exception($"HTTP request failed with status code {response.StatusCode}");
    }
    
    public static async Task<MovieList?>? GetBmListAsync(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var bmList = JsonConvert.DeserializeObject<MovieList>(json);
            return bmList;
        }

        throw new Exception($"HTTP request failed with status code {response.StatusCode}");
    }
    
    
    
    public static async Task<ActorList?>? GetAcListAsync(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var acList = JsonConvert.DeserializeObject<ActorList>(json);
            return acList;
        }

        throw new Exception($"HTTP request failed with status code {response.StatusCode}");
    }
    public static async Task<ActorItem?>? GetAcAsync(string apiUrl)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var ac = JsonConvert.DeserializeObject<ActorItem>(json);
            return ac;
        }

        throw new Exception($"HTTP request failed with status code {response.StatusCode}");
    }
}