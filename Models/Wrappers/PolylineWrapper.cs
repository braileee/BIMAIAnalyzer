using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using BIMAIAnalyzer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BIMAIAnalyzer.Models.Wrappers
{
    public class PolylineWrapper : BaseElementWrapper<PolylineWrapper>
    {
        public PolylineWrapper(Entity entity) : base(entity)
        {
            Model = entity as Polyline;
        }

        [JsonIgnore]
        public Polyline Model { get; set; }

        public double? Length
        {
            get
            {
                return Model?.Length;
            }
        }

        public double Area
        {
            get
            {
                return Model.Area;
            }
        }

        public static List<PolylineWrapper> Create()
        {
            return Create<Polyline>();
        }
    }
}
