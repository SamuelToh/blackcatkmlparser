using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

namespace BlackCat
{
    public interface IResourceReader
    {

        // pre: true
        // post: Returns a sociological table
        DataTable getSocialTable();

    }
}
