﻿using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 学生信息管理系统
{
    public partial class FrmDelClazz : Form
    {
        public FrmMain frmMain { get; set; }
        MySqlConnection sqlcon;
        public FrmDelClazz()
        {
            InitializeComponent();
            string strcon = "server = localhost; user = root; database = sms; password = 123456";
            //建立数据库连接
            sqlcon = new MySqlConnection(strcon);
            try
            {
                //开启连接           
                sqlcon.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("数据库连接失败!");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string clazzId = tbClazzId.Text.ToString();
            if (clazzId == "")
            {
                MessageBox.Show("请输入班级号！");
                return;
            }
            //反序列化对象
            string strSql1 = "select * from clazz";
            MySqlCommand sqlcmd = new MySqlCommand(strSql1, sqlcon);
            MySqlDataAdapter sqlad = new MySqlDataAdapter(sqlcmd);
            DataSet ds = new DataSet();
            sqlad.Fill(ds);
            string strInfo = JsonConvert.SerializeObject(ds.Tables[0]);
            List<Clazz> c = JsonConvert.DeserializeObject<List<Clazz>>(strInfo);
            int sign = 0;
            //查找对象
            foreach (Clazz i in c)
            {
                if (i.Clzno == clazzId)
                {
                    string strSql2 = "delete from sms.clazz where Clzno = '"+clazzId+"'";
                    MySqlCommand cmd = new MySqlCommand(strSql2, sqlcon);
                    cmd.ExecuteNonQuery();
                    sign++;
                }
            }
            if (sign != 0)
            {
                MessageBox.Show("删除成功");
            }
            else
                MessageBox.Show("未找到该班级，请重新输入学号");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            sqlcon.Close();
        }
    }
}
