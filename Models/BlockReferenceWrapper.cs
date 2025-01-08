using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using BIMAIAnalyzer.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BIMAIAnalyzer.Models
{
    public class BlockReferenceWrapper
    {
        [JsonIgnore]
        public BlockReference Model { get; set; }

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
            List<BlockReference> blocks = ElementUtils.GetFromModel<BlockReference>();

            List<BlockReferenceWrapper> polylineWrappers = new List<BlockReferenceWrapper>();

            foreach (BlockReference block in blocks)
            {
                polylineWrappers.Add(new BlockReferenceWrapper
                {
                    Model = block
                });
            }

            return polylineWrappers;
        }
    }
}
