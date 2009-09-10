using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BlackCat
{
    interface IExcelReader
    {
        // pre: true
        // post: Returns list of column names
        ArrayList getColList();

        // pre: true
        // post: Returns a sociological table
        DataTable getSocialTable();

        // pre: true
        // post: Returns first preferences party names
        ArrayList getFirstPrefParties();

        // pre: true
        // post: Returns TPP party names
        ArrayList getTPPNames();
        
        //List<String> getColumnHeaders(;
        //List<String> getDataRow(String columnName, String columnValue);
    }
}
