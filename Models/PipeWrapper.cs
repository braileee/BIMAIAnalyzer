using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.Civil.DatabaseServices;
using BIMAIAnalyzer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMAIAnalyzer.Models
{
    public class PipeWrapper
    {
        [JsonIgnore]
        public Pipe Model { get; set; }

        public string Layer
        {
            get
            {
                return Model?.Layer;
            }
        }

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

        public string Type
        {
            get
            {
                return Model?.GetType().Name;
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

        public static List<PipeWrapper> Create()
        {
            List<Pipe> pipes = ElementUtils.GetFromModel<Pipe>();

            List<PipeWrapper> pipeWrappers = new List<PipeWrapper>();

            foreach (Pipe pipe in pipes)
            {
                pipeWrappers.Add(new PipeWrapper
                {
                    Model = pipe
                });
            }

            return pipeWrappers;
        }
    }
}
