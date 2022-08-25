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
    public partial class FrmFindCourse : Form
    {
        public FrmMain frmMain { get; set; }
        MySqlConnection sqlcon;
        MySqlCommand sqlcmd;
        MySqlDataAdapter sqlad;
        public FrmFindCourse()
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

        private void btnFind_Click(object sender, EventArgs e)
        {
                string com = tbCom.Text.ToString();
                if (com == "")
                {
                    MessageBox.Show("请输入查询条件！");
                    return;
                }
                string strSql1 = "select * from course";
                sqlcmd = new MySqlCommand(strSql1, sqlcon);
                sqlad = new MySqlDataAdapter(sqlcmd);
                DataSet ds = new DataSet();
                sqlad.Fill(ds);
                string strInfo = JsonConvert.SerializeObject(ds.Tables[0]);
                List<Course> cs = JsonConvert.DeserializeObject<List<Course>>(strInfo);
                List<Course> obj = (from i in cs
                                   where i.Cno.Contains(com)
                                   select i).ToList();
                dataGridView2.DataSource = obj;

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            sqlcon.Close();
        }
    }
}
