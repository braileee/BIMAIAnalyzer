using Autodesk.AutoCAD.DatabaseServices;
using BIMAIAnalyzer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMAIAnalyzer.Models
{
    public class Solid3dWrapper
    {
        [JsonIgnore]
        public Solid3d Model { get; set; }

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

        public static List<Solid3dWrapper> Create()
        {
            List<Solid3d> solids = ElementUtils.GetFromModel<Solid3d>();

            List<Solid3dWrapper> solidWrappers = new List<Solid3dWrapper>();

            foreach (Solid3d solid in solids)
            {
                solidWrappers.Add(new Solid3dWrapper
                {
                    Model = solid
                });
            }

            return solidWrappers;
        }
    }
}
