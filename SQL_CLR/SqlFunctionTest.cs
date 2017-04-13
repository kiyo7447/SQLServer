using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlInt32 SqlFunctionTest(int p1 =12, int p2=13)
    {
        // コードをここに記述してください
        //return new SqlString (string.Empty);
        int ret = p1 + p2;
        return new SqlInt32(ret);
    }
}
