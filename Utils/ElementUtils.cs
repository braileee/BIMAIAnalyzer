using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using BIMAIAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BIMAIAnalyzer.Utils
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
