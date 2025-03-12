using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using BIMAIAnalyzer.Civil3D.Services;
using System.Collections.Generic;
using System.Linq;

namespace BIMAIAnalyzer.Civil3D.Utils
{
    public class ElementUtils
    {
        public static List<T> GetFromModel<T>()
        {
            List<T> dbObjects = new List<T>();

            using (Transaction transaction = AutocadDocumentService.TransactionManager.StartOpenCloseTransaction())
            {
                ObjectId blockModelSpaceId = SymbolUtilityServices.GetBlockModelSpaceId(AutocadDocumentService.ActiveDocument.Database);

                BlockTableRecord modelspace = (BlockTableRecord)transaction.GetObject(blockModelSpaceId, OpenMode.ForRead, false, true);

                List<ObjectId> objectIds = modelspace.Cast<ObjectId>().Where(id => id.ObjectClass.DxfName == RXObject.GetClass(typeof(T)).DxfName).ToList();

                foreach (ObjectId objectId in objectIds)
                {
                    T dbObject = (T)(object)transaction.GetObject(objectId, OpenMode.ForRead, false, true);
                    dbObjects.Add(dbObject);
                }

                transaction.Commit();
            }

            return dbObjects;
        }
    }
}
