using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace rimsha
{
    public partial class sname : Form
    {
        public sname()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NAH79MM\\SQLEXPRESS;Initial Catalog=student.text;Integrated Security=True;Encrypt=False");
        public int StudentID;

        private void sname_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }

        private void GetStudentRecord()
        {
           
            SqlCommand cmd = new SqlCommand("select * from studenttable", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            studentrecorddatagridview.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Isvalid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO studenttable values (@name, @fathername, @roll, @mobile, @address)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtstudentname.Text);
                cmd.Parameters.AddWithValue("@fathername", txtfathername.Text);
                cmd.Parameters.AddWithValue("@roll", txtrollnumber.Text);
                cmd.Parameters.AddWithValue("@mobile", txtcontact.Text);
                cmd.Parameters.AddWithValue("@address", txtaddress.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New student is successfully saved in the database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormcontrols();
            }




    }

        private bool Isvalid()
        {
            if (txtstudentname.Text == string.Empty)
            { 
             MessageBox.Show("student name is required", "failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true; 
        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormcontrols();

        }

        private void ResetFormcontrols()
        {
            StudentID = 0;
            txtstudentname.Clear();
            txtfathername.Clear();
            txtaddress.Clear();
            txtcontact.Clear();
            txtrollnumber.Clear();

            txtstudentname.Focus();
        }

        private void studentrecorddatagridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(studentrecorddatagridview.SelectedRows[0].Cells[0].Value);
            txtstudentname.Text = studentrecorddatagridview.SelectedRows[0].Cells[1].Value.ToString();
            txtfathername.Text = studentrecorddatagridview.SelectedRows[0].Cells[2].Value.ToString();
            txtaddress.Text = studentrecorddatagridview.SelectedRows[0].Cells[3].Value.ToString();
            txtcontact.Text = studentrecorddatagridview.SelectedRows[0].Cells[4].Value.ToString();
            txtrollnumber.Text = studentrecorddatagridview.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE studenttable SET name = @name, fathername = @fathername, rollnumber = @roll, contact = @mobile, address = @address WHERE studentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtstudentname.Text);
                cmd.Parameters.AddWithValue("@fathername", txtfathername.Text);
                cmd.Parameters.AddWithValue("@roll", txtrollnumber.Text);
                cmd.Parameters.AddWithValue("@mobile", txtcontact.Text);
                cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("student information is updated successfully", "updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormcontrols();

            }
            else
            {
                MessageBox.Show("please select a student to update his information", "select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           if(StudentID > 0) 
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM studenttable WHERE studentID = @ID", con);
               
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("student is deleted from the system", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormcontrols();
            }
            else
            {
             MessageBox.Show("please select a student to delete", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        }
    }

