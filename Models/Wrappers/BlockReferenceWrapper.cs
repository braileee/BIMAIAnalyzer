using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using BIMAIAnalyzer.Civil3D.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BIMAIAnalyzer.Civil3D.Models.Wrappers
{
    public class BlockReferenceWrapper : BaseElementWrapper<BlockReferenceWrapper>
    {
        public BlockReferenceWrapper(Entity entity) : base(entity)
        {
            Model = entity as BlockReference;
        }

        [JsonIgnore]
        public BlockReference Model { get; set; }

        public Point3d Position
        {
            get
            {
                return Model.Position;
            }
        }

        public string Name
        {
            get
            {
                return Model.IsDynamicBlock ?
                            ((BlockTableRecord)Model.DynamicBlockTableRecord.GetObject(OpenMode.ForRead)).Name :
                            Model.Name;
            }
        }

        public static List<BlockReferenceWrapper> Create()
        {
            return Create<BlockReference>();
        }
    }
}
