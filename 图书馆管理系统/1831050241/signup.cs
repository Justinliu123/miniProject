using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace _1831050241
{
    public partial class signup : Form
    {
        string genderText = "男";//当前选择的性别

        login f = new login();
        public signup(login f)
        {
            InitializeComponent();
            this.f = f;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            genderText = "女";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void signup_Load(object sender, EventArgs e)
        {

        }

        private void signup_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void signup_FormClosing(object sender, FormClosingEventArgs e)
        {
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //点击注册按钮事件
        private void button2_Click(object sender, EventArgs e)
        {
            //获取填入的内容
            string usernameText = usernameBox.Text;
            string emailText = emailBox.Text;
            string password1 = passwordBox1.Text;
            string password2 = passwordBox2.Text;

            //提示信息\\\\\\\\\\\\\\\\\\\\\\\分界线\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            //两次密码必须相同
            if (password1.Length <= 4)
            {
                MessageBox.Show("密码不能小于5位！");
                return;
            }
            else if (password1.Length > 20)
            {
                MessageBox.Show("密码过长，注意小于20位！");
                return;
            }
            if (!password1.Equals(password2))
            {
                MessageBox.Show("两次密码不一致！");
                return;
            }
            //用户名效验
            if (usernameText.Length <= 0)
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            else if (usernameText.Length != 10)
            {
                MessageBox.Show("用户名长度必须是10位，请检查！");
                return;
            }
            //如果输入验证邮箱
            if (emailText.Length >= 1)
            {
                string patten = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                if (!Regex.IsMatch(emailText, patten))
                {
                    MessageBox.Show("邮箱格式错误！");
                    return;
                }
                else if (emailText.Length > 30)
                {
                    MessageBox.Show("邮箱过长，注意长度应小于30位！");
                    return;
                }
            }

            //插入数据库
            DB db = new DB();
            MySqlCommand cmd = new MySqlCommand("insert ignore into user(username, password, gender, email) values(@un,@pwd,@gender,@email)", db.connect());
            cmd.Parameters.AddWithValue("un", usernameText);//添加值
            cmd.Parameters.AddWithValue("pwd", password1);//添加值
            cmd.Parameters.AddWithValue("gender", genderText);//添加值
            cmd.Parameters.AddWithValue("email", emailText);//添加值

            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("注册成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名已存在！");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            genderText = "男";
        }
    }
}
