using System;
using System.Data;
using System.Text.RegularExpressions;
using NonFactors.Mvc.Grid;

namespace GridTest
{
    public class Grid
    {
        static readonly Regex square = new Regex("\\[(.*?)\\]");
        static readonly Regex curly = new Regex("{(.*?)}");

        public static IGridColumn<DataRow, object> FormatGridColumn(IGridColumnsOf<DataRow> columns, DataColumn dataColumn, Models.ReportGrid.Column columnInfo)
        {
            IGridColumn<DataRow, object> thisColumn;
            //If the column name specifies formats, we parse the values.
            if (square.IsMatch(dataColumn.ColumnName))
            {
                var match = square.Match(dataColumn.ColumnName).Groups[0].Value;

                switch (match.ToLowerInvariant())
                {
                    case "[date]":
                        DateTime dateTime;
                        thisColumn = columns.Add(t => DateTime.TryParse(t.ItemArray[dataColumn.Ordinal].ToString(), out dateTime) ? dateTime : null as object).FilteredAs("date");
                        break;
                    case "[time]":
                        DateTime dateTimeTime;
                        thisColumn = columns.Add(t => DateTime.TryParse(t.ItemArray[dataColumn.Ordinal].ToString(), out dateTimeTime) ? dateTimeTime : null as object).FilteredAs("date")
                            .Formatted("{0:hh:mm tt}");
                        break;
                    case "[int]":
                        int intTemp;
                        thisColumn = columns.Add(t => int.TryParse(t.ItemArray[dataColumn.Ordinal].ToString(), out intTemp) ? intTemp : null as object).FilteredAs("number")
                        ;
                        break;
                    case "[double]":
                        double doubleTemp;
                        thisColumn = columns.Add(t => double.TryParse(t.ItemArray[dataColumn.Ordinal].ToString(), out doubleTemp) ? doubleTemp : null as object).FilteredAs("number")
                            ;
                        break;
                    case "[currency]":
                        decimal currencyTemp;
                        thisColumn = columns.Add(t => decimal.TryParse(t.ItemArray[dataColumn.Ordinal].ToString(), out currencyTemp) ? currencyTemp : null as object).FilteredAs("number")
                            .Formatted("{0:C}");
                        break;
                    default:
                        if (match.ToLowerInvariant().Contains("date") && curly.IsMatch(match))
                        {
                            var matchedSq = curly.Match(match).Groups[0].Value;
                            DateTime dateTimeTemp;
                            thisColumn = columns.Add(t => DateTime.TryParse(t.ItemArray[dataColumn.Ordinal].ToString(), out dateTimeTemp) ? dateTimeTemp : null as object).FilteredAs("date")
                            .Formatted(matchedSq);
                        }
                        else
                        {
                            thisColumn = columns.Add(t => t.ItemArray[dataColumn.Ordinal].ToString() as object).FilteredAs("text");
                        }
                        break;
                }
            }
            else
            {
                thisColumn = columns.Add(t => t.ItemArray[dataColumn.Ordinal].ToString() as object).FilteredAs("text");
            }

            thisColumn.Filterable(columnInfo.Filterable)
                .Sortable(columnInfo.Sortable)
                .Titled(!string.IsNullOrEmpty(columnInfo.Title) ? columnInfo.Title : square.Replace(dataColumn.ColumnName, ""))
                .Named(square.Replace(dataColumn.ColumnName, "").GetSanitizedName());

            if (columnInfo.Filterable && columnInfo.FilterType.HasValue)
            {
                thisColumn.Filterable(columnInfo.FilterType.Value);

                if (columnInfo.UseFilterOptions)
                {
                    thisColumn.UsingFilterOptions();
                }

            }

            return thisColumn;
        }
        
    }
}