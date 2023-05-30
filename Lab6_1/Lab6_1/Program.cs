using RestSharp;
using RestSharp.Extensions;
using RestSharp.Serializers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Lab6_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://api.open-meteo.com/v1/forecast?latitude=49.23&longitude=28.47&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset&current_weather=true&timezone=auto";
            var client = new RestClient(url);
            var request = new RestRequest(url,Method.Get);
            var response = client.Execute(request);
            JObject obj = JObject.Parse(response.Content);
            Console.WriteLine(response.Content);
            Console.WriteLine($"Latitude: {obj["latitude"]}\n" +
                $"Longitude: {obj["longitude"]}\nTemperature: {obj["current_weather"]["temperature"]}*C\n" +
                $"Time: {DateTime.Parse(obj["current_weather"]["time"].ToString())}");
            Console.ReadLine();
        }
    }
}
