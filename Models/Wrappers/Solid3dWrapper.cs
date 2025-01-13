using Autodesk.AutoCAD.DatabaseServices;
using BIMAIAnalyzer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMAIAnalyzer.Models.Wrappers
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

        public static List<Solid3dWrapper> Create()
        {
            return Create<Solid3d>();
        }
    }
}
