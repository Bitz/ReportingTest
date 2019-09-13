using System.Collections.Generic;
using System.Web.Mvc;
using GenericParsing;
using NonFactors.Mvc.Grid;

namespace GridTest.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            using (GenericParserAdapter parser = new GenericParserAdapter(Server.MapPath("~/data.csv")))
            {
                parser.FirstRowHasHeader = true;
                Models.ReportGrid grid = new Models.ReportGrid
                {
                    Data = parser.GetDataSet().Tables[0],
                    Columns = new List<Models.ReportGrid.Column>
                    {
                        new Models.ReportGrid.Column
                        {
                            DataTableName = "Location",
                            FilterType = GridFilterType.Multi,
                            UseFilterOptions = true,
                            Filterable = true
                        },
                        new Models.ReportGrid.Column
                        {
                            DataTableName = "Copay",
                            FilterType = GridFilterType.Double,
                            Filterable = true
                        }
                    }
                    
                };
                return View(grid);
            }
        }
    }
}