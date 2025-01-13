using BIMAIAnalyzer.Models.Wrappers;
using System.Collections.Generic;

namespace BIMAIAnalyzer.Models
{
    public class ElementCollection
    {
        private ElementCollection()
        {
        }

        public List<PolylineWrapper> PolylineWrappers { get; set; } = [];
        public List<Polyline3dWrapper> Polyline3dWrappers { get; set; } = [];
        public List<Solid3dWrapper> Solid3dWrappers { get; private set; } = [];
        public List<BlockReferenceWrapper> BlockReferenceWrappers { get; set; } = [];
        public List<PipeWrapper> Pipes { get; private set; } = [];

        public static ElementCollection Create()
        {
            return new ElementCollection
            {
                PolylineWrappers = PolylineWrapper.Create(),
                Polyline3dWrappers = Polyline3dWrapper.Create(),
                Solid3dWrappers = Solid3dWrapper.Create(),
                BlockReferenceWrappers = BlockReferenceWrapper.Create(),
                Pipes = PipeWrapper.Create()
            };
        }
    }
}
