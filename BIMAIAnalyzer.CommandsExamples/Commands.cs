using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;


namespace BIMAIAnalyzer.CommandsExamples
{
    public class Commands
    {
        [CommandMethod("PSV", "AddCogoPoint", CommandFlags.Modal)]
        public static void AddCogoPoint()
        {
            Autodesk.AutoCAD.ApplicationServices.Document document = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Database database = document.Database;

            using (document.LockDocument())
            {
                using (Transaction trans = database.TransactionManager.StartTransaction())

                {
                    CogoPointCollection cogoPoints = CivilApplication.ActiveDocument.CogoPoints;
                    ObjectId pointId = cogoPoints.Add(new Point3d(100, 200, 300), useNextPointNumSetting: true);
                    CogoPoint cogoPoint = pointId.GetObject(OpenMode.ForWrite) as CogoPoint;
                    cogoPoint.PointName = "Survey_Base_Point";
                    cogoPoint.RawDescription = "This is Survey Base Point";
                    trans.Commit();
                }
            }
        }
    }
}
