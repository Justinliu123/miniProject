using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace _1831050241
{
    public class DB
    {
        MySqlConnection con;
        MySqlCommand cmd;
        //数据库操作
        //1.连接数据库
        public MySqlConnection connect()
        {
            if (con == null)
            {
                String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456;database=book;Charset=utf8";
                con = new MySqlConnection(connetStr);

                con.Open();
            }
            return con;
        }
        //执行语句的数据库方法
        public MySqlCommand command(string sql)
        {
            cmd = new MySqlCommand(sql, connect());
            return cmd;

        }
        //行数影响的方法
        public int Execute(string sql)
        {
            return command(sql).ExecuteNonQuery();

        }

        //传入comd来执行影响行数的方法
        public int Execute(MySqlCommand msc)
        {
            return cmd.ExecuteNonQuery();
        }

        //返回查询结果的方法
        public MySqlDataReader read(string sql)
        {
            return command(sql).ExecuteReader();
        }
        //关闭连接的方法
        public void close()
        {
            con.Close();
        }

        public MySqlConnection getConn()
        {
            return con;
        }
        public MySqlCommand getCmd()
        {
            return cmd;
        }
    }
}
