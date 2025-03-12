using BIMAIAnalyzer.Gemini.Connection.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMAIAnalyzer.Gemini.Connection
{
    public class PromptFineTune
    {
        public PromptFineTune(string fineTuneUrl, string apiKey)
        {
            FineTuneUrl = fineTuneUrl;
            ApiKey = apiKey;
        }

        public string FineTuneUrl { get; }
        public string ApiKey { get; }

        public string GetResponse(string input)
        {
            var client = new RestClient(FineTuneUrl);
            var request = new RestRequest("tunedModels", Method.Post);
            request.AddQueryParameter("key", ApiKey);
            request.AddHeader("Content-Type", "application/json");

            var requestBody = new
            {
                display_name = "Civil3D Code Generator Model",
                base_model = "models/gemini-1.5-flash-001-tuning",
                tuning_task = new
                {
                    hyperparameters = new
                    {
                        batch_size = 2,
                        learning_rate = 0.001,
                        epoch_count = 5,
                    },
                    training_data = new
                    {
                        examples = new
                        {
                            examples = new[]
                        {
                            new { text_input = "1", output = "2" },
                        }
                        }
                    }
                }
            };
        }
    }
}
