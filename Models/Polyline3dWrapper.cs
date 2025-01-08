using Autodesk.Aec.Modeler;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using BIMAIAnalyzer.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BIMAIAnalyzer.Models
{
    public class Polyline3dWrapper
    {
        [JsonIgnore]
        public Polyline3d Model { get; set; }

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

        public List<Point3d> Points
        {
            get
            {
                List<Point3d> points = new List<Point3d>();

                using (Transaction transaction = AutocadDocumentService.TransactionManager.StartTransaction())
                {
                    foreach (ObjectId objectId in Model)
                    {
                        object polylineObject = transaction.GetObject(objectId, OpenMode.ForRead, false, true);

                        if (polylineObject is PolylineVertex3d vertex)
                        {
                            Point3d point = new Point3d(vertex.Position.X, vertex.Position.Y, vertex.Position.Z);
                            points.Add(point);
                        }
                    }

                    transaction.Commit();
                }

                return points;
            }
        }

        public static List<Polyline3dWrapper> Create()
        {
            List<Polyline3d> polylines = ElementUtils.GetFromModel<Polyline3d>();

            List<Polyline3dWrapper> polylineWrappers = new List<Polyline3dWrapper>();

            foreach (Polyline3d polyline in polylines)
            {
                polylineWrappers.Add(new Polyline3dWrapper
                {
                    Model = polyline
                });
            }

            return polylineWrappers;
        }
    }
}
