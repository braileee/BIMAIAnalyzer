using Autodesk.AutoCAD.Geometry;
using Autodesk.Civil.DatabaseServices;
using AcadDb = Autodesk.AutoCAD.DatabaseServices;
using BIMAIAnalyzer.Civil3D.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMAIAnalyzer.Civil3D.Models.Wrappers
{
    public class PipeWrapper : BaseElementWrapper<PipeWrapper>
    {
        public PipeWrapper(AcadDb.Entity entity) : base(entity)
        {
            Model = entity as Pipe;
        }

        [JsonIgnore]
        public Pipe Model { get; set; }

        public string NetworkName
        {
            get
            {
                return Model?.NetworkName;
            }
        }

        public string Name
        {
            get
            {
                return Model?.Name;
            }
        }

        public Point3d StartPoint
        {
            get
            {
                return Model.StartPoint;
            }
        }

        public Point3d EndPoint
        {
            get
            {
                return Model.EndPoint;
            }
        }

        public double Length
        {
            get
            {
                return Model.Length3D;
            }
        }

        public string PartSizeName
        {
            get
            {
                return Model?.PartSizeName;
            }
        }

        public double Slope
        {
            get
            {
                return Model.Slope;
            }
        }

        public static List<PipeWrapper> Create()
        {
            return Create<Pipe>();
        }
    }
}
