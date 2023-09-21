using Newtonsoft.Json;
using SeatManagement.Models;
using SeatManagement.DTO;
using SeatManagementConsole.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SeatManagementConsole.Implementation
{
    public class APICall<T> : IAPICall<T> where T : class
    {
        private readonly HttpClient _client;
        private readonly string _endPoint;

        public APICall(string endPoint)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7064/");
            _endPoint = endPoint;
        }
        public int Add(T item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(_endPoint, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                if (int.TryParse(responseContent, out int result))
                {
                    return result; // Return the integer value from the API response
                }
                else
                    Console.WriteLine($"Response content : {responseContent}");
            }
            return 0;
        }

        public void BulkAdd(List<T> items)
        {
            var json= JsonConvert.SerializeObject(items);
            var content = new StringContent(json, Encoding.UTF8 , "application/json");
            var response = _client.PostAsync(_endPoint, content).Result;
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Bulk insertion successful.");
            }
            else
            {
                Console.WriteLine("Bulk insertion failed with status code: " + response.StatusCode);
            }

        }

        public List<T> GetAll()
        {
            var response = _client.GetAsync(_endPoint).Result;
            if(response.StatusCode==HttpStatusCode.OK)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var getResponse = JsonConvert.DeserializeObject<List<T>>(responseContent);
                return getResponse.ToList();
            }
            else if(response.StatusCode==HttpStatusCode.NoContent)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Response content : {responseContent}");
                return new List<T>();
            }
            else if(response.StatusCode==HttpStatusCode.BadRequest)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Response content : {responseContent}");
                return new List<T>();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Response content : {responseContent}");
                return new List<T>();
            }
            return new List<T>();
        }

        public T? GetById(int id)
        {
            var response = _client.GetAsync(_endPoint + "/id?id="+id).Result;
            if(response.StatusCode==HttpStatusCode.OK)
            {
                var responseContent=response.Content.ReadAsStringAsync().Result;
                var getResponse=JsonConvert.DeserializeObject<T>(responseContent);
                return getResponse;
            }
            else if(response.StatusCode==HttpStatusCode.NotFound)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Response Content: {responseContent}");
            }
            return default;
        }

        public void Update(T item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PatchAsync(_endPoint, content).Result; 
           
            var responseContent = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine($"Response content : {responseContent}");
        }

        public int Update()
        {
            var response = _client.PatchAsync(_endPoint, null).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            if(response.StatusCode==HttpStatusCode.Conflict )
            {
                Console.WriteLine($"Response content : {responseContent}");
                return -1;
            }
            else if(response.StatusCode==HttpStatusCode.NotFound ) 
            {
                Console.WriteLine($"Response content : {responseContent}");
                return -1;
            }
            return 0;
        }
    }
}
