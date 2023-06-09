﻿using System.Data;
using System.Data.SqlClient;

namespace ChartExample.Models
{
    public class DB
    {
        public SqlConnection con { get; set; }
        public DB()
        {
            string conStr = "Data Source=addYourDeviceName;Initial Catalog=addYourDatabase;Integrated Security=True";
            con = new SqlConnection(conStr);
        }

       /* public StudentInfo StudentInfoById(string id)
        {
            StudentInfo student = new StudentInfo();
            string query = "Select * from student_info where id= " + id;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                dt.Load(cmd.ExecuteReader());

                foreach(DataRow row in dt.Rows)
                {
                    student.Fname = row["First Name"].ToString();
                    student.Lname = row["Last Name"].ToString();
                    student.Email = row["Email"].ToString();
                    student.Section = Int16.Parse(row["section"].ToString());
                    student.CodeEditor = row["code_editor"].ToString();
                }
                

            }
            catch (Exception e)
            {
                throw e;
            }
            finally { con.Close(); }

            return student;
        }*/

        public List<string> getCodeEditors()
        {
            DataTable dt = new DataTable();
            List<string> codeEditorsList = new List<string>();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetCodeEditors", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader sdr = cmd.ExecuteReader();

                while ( sdr.Read())
                {
                    codeEditorsList.Add(sdr["code_editor"].ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { con.Close(); }

            return codeEditorsList;
        }
    }
}
