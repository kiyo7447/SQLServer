using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

public partial class Triggers
{        
    // ターゲットの既存のテーブルまたはビューを入力して、属性行のコメントを解除します
    [Microsoft.SqlServer.Server.SqlTrigger (Name="SqlTriggerTest", Target="Table1", Event="FOR UPDATE")]
    public static void SqlTriggerTest ()
    {
        SqlTriggerContext tricon =  SqlContext.TriggerContext;

        SqlConnection con = new SqlConnection("context connection=true");
        con.Open();
        if (tricon.TriggerAction == TriggerAction.Update)
        {
            
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "insert into [table1history] (id, name)values((select count(*) from inserted),@name)";
            com.Parameters.Add("@id", SqlDbType.Int);
            com.Parameters.Add("@name", SqlDbType.NVarChar, 30);
            com.Parameters["@id"].Value = 1;
            com.Parameters["@name"].Value = "更新後";
            com.ExecuteNonQuery();
        }
        else
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "insert into [table1history] (id, name)values(@id, @name)";
            com.Parameters.Add("@id", SqlDbType.Int);
            com.Parameters.Add("@name", SqlDbType.NVarChar, 30);
            com.Parameters["@id"].Value = 1;
            com.Parameters["@name"].Value = "更新以外"; 
            com.ExecuteNonQuery();
        }
        con.Close();
        // ユーザーのコードで置き換えてください
        SqlContext.Pipe.Send("トリガーの起動");
    }
}

