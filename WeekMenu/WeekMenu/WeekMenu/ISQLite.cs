using System;
using System.Collections.Generic;
using System.Text;

namespace WeekMenu
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
