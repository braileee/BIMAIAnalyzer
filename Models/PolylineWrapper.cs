using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using BIMAIAnalyzer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BIMAIAnalyzer.Models
{
    public class PolylineWrapper
    {
        [JsonIgnore]
        public Polyline Model { get; set; }

        public double? Length
        {
            get
            {
                return Model?.Length;
            }
        }

        public string Layer
        {
            get
            {
                return Model?.Layer;
            }
        }

        public string Type
        {
            get
            {
                return Model?.GetType().Name;
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
            List<Polyline> polylines = ElementUtils.GetFromModel<Polyline>();

            List<PolylineWrapper> polylineWrappers = new List<PolylineWrapper>();

            foreach (Polyline polyline in polylines)
            {
                polylineWrappers.Add(new PolylineWrapper
                {
                    Model = polyline
                });
            }

            return polylineWrappers;
        }
    }
}
