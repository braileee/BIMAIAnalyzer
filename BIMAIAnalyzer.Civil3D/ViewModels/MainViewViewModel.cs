using Prism.Commands;
using Prism.Mvvm;
using System;
using Newtonsoft.Json;
using BIMAIAnalyzer.Civil3D.Models;
using BIMAIAnalyzer.Core;
using BIMAIAnalyzer.Gemini.Connection;
using Microsoft.Extensions.Configuration;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace BIMAIAnalyzer.Civil3D.ViewModels
{
    public class MainViewViewModel : BindableBase
    {
        private string input;
        private string output;

        public MainViewViewModel()
        {
            ProcessInputCommand = new DelegateCommand(OnProcessInputCommand);
        }

        private void OnProcessInputCommand()
        {
            try
            {
                ElementCollection elementCollection = ElementCollection.Create();

                string elementJsonContent = JsonConvert.SerializeObject(elementCollection);

                IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Main>().Build();
                string apiKey = config["apikey"];

                PromptRequest promptRequest = new PromptRequest(Constants.GeminiUrl, apiKey);
                string inputWithElementData = $"{Input}{Environment.NewLine}{elementJsonContent}";
                Output = promptRequest.GetResponse(inputWithElementData);
            }
            catch (Exception)
            {
            }


            Database database = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Database;
            using (Transaction trans = database.TransactionManager.StartTransaction())

            {
                CogoPointCollection cogoPoints = CivilApplication.ActiveDocument.CogoPoints;

                ObjectId pointId = cogoPoints.Add(new Point3d(x: 1, y: 2, z: 3), useNextPointNumSetting: true);
                CogoPoint cogoPoint = pointId.GetObject(OpenMode.ForWrite) as CogoPoint;
                cogoPoint.PointName = "Survey_Base_Point";
                cogoPoint.RawDescription = "This is Survey Base Point";
                trans.Commit();
            }
        }

        public string Input
        {
            get
            {
                return input;
            }
            set
            {
                input = value;
                RaisePropertyChanged();
            }
        }
        public string Output
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand ProcessInputCommand { get; }
    }
}
