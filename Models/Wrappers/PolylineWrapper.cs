using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using BIMAIAnalyzer.Civil3D.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BIMAIAnalyzer.Civil3D.Models.Wrappers
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

        public List<Point3d> Points
        {
            get
            {
                List<Point3d> points = new List<Point3d>();

                for (var i = 0; i < Model.NumberOfVertices; i++)
                {
                    Point3d point = Model.GetPoint3dAt(i);
                    points.Add(point);
                }

                return points;
            }
        }

        public static List<PolylineWrapper> Create()
        {
            return Create<Polyline>();
        }
    }
}
