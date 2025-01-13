using Autodesk.Aec.Modeler;
using AcadDb = Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using BIMAIAnalyzer.Services;
using BIMAIAnalyzer.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;

namespace BIMAIAnalyzer.Models.Wrappers
{
    public class Polyline3dWrapper : BaseElementWrapper<Polyline3dWrapper>
    {
        public Polyline3dWrapper(AcadDb.Entity entity) : base(entity)
        {
            Model = entity as AcadDb.Polyline3d;
        }

        [JsonIgnore]
        public AcadDb.Polyline3d Model { get; set; }

        public double? Length
        {
            get
            {
                return Model?.Length;
            }
        }

        public static List<Polyline3dWrapper> Create()
        {
            return Create<Polyline3d>();
        }
    }
}
