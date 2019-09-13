using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NonFactors.Mvc.Grid;

namespace GridTest
{
    public class Models
    {
        public class ReportGrid
        {
            public DataTable Data { get; set; }
            public List<Column> Columns { get; set; } = new List<Column>();
            public string EmptyText { get; set; } = "No data found";
            public string RunInformation { get; set; }
            public int RowsPerPage { get; set; } = 100;

            public class Column
            {
                public string DataTableName { get; set; }
                public string Title { get; set; }
                public bool Sortable { get; set; } = true;
                public bool Filterable { get; set; } = false;
                public bool UseFilterOptions { get; set; } = false;
                public GridFilterType? FilterType { get; set; } = null;
            }
        }
    }
}