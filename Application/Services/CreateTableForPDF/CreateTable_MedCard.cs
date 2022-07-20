using Core.Entities;
using Core.Interfaces.Services;
using Core.Paginator;
using System.Data;

namespace Application.Services.CreateTableForPDF
{
    public class CreateTable_MedCard : ICreateTable<Appointment>
    {
        public DataTable CreateMyTable(PagedList<Appointment> list)
        {
            DataTable dataTable = new DataTable();
            //Add columns to the DataTable
            dataTable.Columns.Add("Data");
            dataTable.Columns.Add("Disease");
            //Add rows to the DataTable
            foreach (var x in list)
            {
                dataTable.Rows.Add(new object[] { $"{x.Date}", $"{x.Disease}" });
            }

            return dataTable;
        }
    }
}
