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
            return Create<Polyline3d>();
        }
    }
}
