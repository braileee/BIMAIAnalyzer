using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BIMAIAnalyzer.Civil3D.Models.Wrappers
{
    public class Solid3dWrapper : BaseElementWrapper<Solid3dWrapper>
    {
        public Solid3dWrapper(Entity entity) : base(entity)
        {
            Model = entity as Solid3d;
        }

        [JsonIgnore]
        public Solid3d Model { get; set; }

        public double Volume
        {
            get
            {
                return Model.MassProperties.Volume;
            }
        }

        public double Area
        {
            get
            {
                return Model.Area;
            }
        }

        public Point3d Centroid
        {
            get
            {
                return Model.MassProperties.Centroid;
            }
        }

        public static List<Solid3dWrapper> Create()
        {
            return Create<Solid3d>();
        }
    }
}
