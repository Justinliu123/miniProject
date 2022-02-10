using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _1831050241
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        
        //注册按钮
        private void button2_Click(object sender, EventArgs e)
        {
            signup f2 = new signup(this);    //注册窗体
            this.Hide();
            f2.Show();

        }

        private void login_Load(object sender, EventArgs e)
        {
        }
        //登录按钮
        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            string un = username.Text;      //输入框中的用户名
            string pw = password.Text;      //输入框中的密码
            //检查输入内容不能为空
            if (un.Length <= 0)
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            else if (pw.Length <= 0)
            {
                MessageBox.Show("密码不能为空！");
                return;
            }
            //查询是否存在该用户名
            
            MySqlCommand cmd = new MySqlCommand("select * from user where username=@un", db.connect());
            cmd.Parameters.AddWithValue("un", un);//添加值
            MySqlDataReader mdr = cmd.ExecuteReader();
            
            //如果没有查到，返回不存在用户
            if (!mdr.HasRows)
            {
                MessageBox.Show("不存在该用户，请注册！");
                return;
            }
            mdr.Read();
            string pword = mdr.GetString(1);
            if (pword == pw)        //登录成功
            {
                this.Hide();
                index f1 = new index();  //首页窗体
                f1.Show();
                db.close();                 //关闭数据库连接  
            }
            else {
                MessageBox.Show("密码错误！");
            }
        }

    }
}
