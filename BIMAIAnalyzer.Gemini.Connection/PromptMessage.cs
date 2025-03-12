using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIMAIAnalyzer.Gemini.Connection.Json;
using System.Text.RegularExpressions;

namespace BIMAIAnalyzer.Gemini.Connection
{
    public class PromptMessage
    {
        public PromptMessage(string geminiUrl, string apiKey)
        {
            GeminiUrl = geminiUrl;
            ApiKey = apiKey;
        }

        public string GeminiUrl { get; }
        public string ApiKey { get; }

        public ResponseMessage GetResponse(string input)
        {
            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest(GeminiUrl, Method.Post);

            request.AddHeader("Content-Type", "application/json");

            List<Part> parts = [
                                   new Part {
                                    Text = input
                                    }
                               ];

            Content content = new()
            {
                Parts =
                   parts
            };

            JsonInput jsonInput = new JsonInput
            {
                Contents = new List<Content> { content }
            };

            JsonSerializerSettings settings = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string json = JsonConvert.SerializeObject(jsonInput, settings);
            request.AddJsonBody(json);
            request.AddQueryParameter("key", ApiKey);

            RestResponse restResponse = restClient.Execute(request);

            string output = string.Empty;

            string code = string.Empty;

            if (restResponse.IsSuccessStatusCode)
            {
                JsonOutput jsonOutput = JsonConvert.DeserializeObject<JsonOutput>(restResponse.Content);
                output = string.Join(Environment.NewLine, jsonOutput.Candidates.SelectMany(candidate => candidate.Content.Parts.Select(item => item.Text)));

                string pattern = @"```csharp\s*(.*?)\s*```";
                Match match = Regex.Match(output, pattern, RegexOptions.Singleline);

                if (match.Success)
                {
                    code = match.Value.Replace("```csharp", "").Replace("```", "").Trim();
                }
            }
            else
            {
                output = "Error";
            }

            return new ResponseMessage { Code = code, Output = output };
        }
    }
}
