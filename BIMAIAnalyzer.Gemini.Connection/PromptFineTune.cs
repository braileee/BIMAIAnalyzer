using BIMAIAnalyzer.Gemini.Connection.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;

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
                            new { text_input = "Add cogo points in Civil 3D using C#", output = "public static void AddCogoPoint()\r\n        {\r\n            Autodesk.AutoCAD.ApplicationServices.Document document = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;\r\n            Database database = document.Database;\r\n\r\n            using (document.LockDocument())\r\n            {\r\n                using (Transaction trans = database.TransactionManager.StartTransaction())\r\n\r\n                {\r\n                    CogoPointCollection cogoPoints = CivilApplication.ActiveDocument.CogoPoints;\r\n                    ObjectId pointId = cogoPoints.Add(new Point3d(1, 2, 3), useNextPointNumSetting: true);\r\n                    CogoPoint cogoPoint = pointId.GetObject(OpenMode.ForWrite) as CogoPoint;\r\n                    cogoPoint.PointName = \"Survey_Base_Point\";\r\n                    cogoPoint.RawDescription = \"This is Survey Base Point\";\r\n                    trans.Commit();\r\n                }\r\n            }\r\n        }" },
                        }
                        }
                    }
                }
            };

            request.AddJsonBody(requestBody);

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"Error creating tuned model: {response.ErrorMessage ?? response.Content}");
                return string.Empty;
            }

            var jsonResponse = JObject.Parse(response.Content);
            string operationName = jsonResponse["name"].ToString();

            // Step 2: Check Operation Status
            bool tuningDone = false;
            while (!tuningDone)
            {
                Thread.Sleep(5000); // Sleep for 5 seconds
                var operationRequest = new RestRequest($"{operationName}", Method.Get);
                operationRequest.AddQueryParameter("key", ApiKey);
                var operationResponse = client.Execute(operationRequest);

                if (!operationResponse.IsSuccessful)
                {
                    Console.WriteLine($"Error checking operation status: {operationResponse.ErrorMessage ?? operationResponse.Content}");
                    break;
                }

                var operationJsonResponse = JObject.Parse(operationResponse.Content);
                if (operationJsonResponse["metadata"] != null && operationJsonResponse["metadata"]["completedPercent"] != null)
                {
                    Console.WriteLine($"Tuning...{operationJsonResponse["metadata"]["completedPercent"]}%");
                }

                if (operationJsonResponse["done"] != null && operationJsonResponse["done"].Value<bool>())
                {
                    tuningDone = true;
                }
            }

            // Step 3: Get Tuned Model State
            if (tuningDone)
            {
                string tunedModelName = jsonResponse["metadata"]["tunedModel"].ToString();

                var modelRequest = new RestRequest(tunedModelName, Method.Get);
                modelRequest.AddQueryParameter("key", ApiKey);
                var modelResponse = client.Execute(modelRequest);

                if (!modelResponse.IsSuccessful)
                {
                    Console.WriteLine($"Error getting tuned model: {modelResponse.ErrorMessage ?? modelResponse.Content}");
                    return string.Empty;
                }
                var modelJsonResponse = JObject.Parse(modelResponse.Content);
                Console.WriteLine($"Model State: {modelJsonResponse["state"]}");
            }

            return string.Empty;
        }
    }
}
