using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

namespace ReadExcelData
{
    interface IResourceReader
    {
        // pre: true
        // post: Returns list of column names
        ArrayList getColList();

        // pre: true
        // post: Returns a sociological table
        // DataTable getSocialTable();
        // Shouldn't need the table outside the class

        // This method retrieves selected column data from the sociological data table
        // pre: tblSocialData is not null and selectedColName is not empty string
        // post: Returns the selected sociological column data
        ArrayList getSocioColumnData(String selectedColName);
    }
}
