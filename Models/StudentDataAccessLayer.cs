using StudentManagementSystem.Utility;
using System.Data;
using System.Data.SqlClient;


namespace StudentManagementSystem.Models
{
    public class StudentDataAccessLayer
    {
        string connectionString = ConnectionString.CName;

        public string AddStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return "Successfull 200";
        }

        public IEnumerable<Student> GetAllStudent()
        {
            List<Student> lstStudent = new List<Student>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Student student = new Student();
                    student.Id = Convert.ToInt32(rdr["Id"]);
                    student.FirstName = rdr["FirstName"].ToString();
                    student.LastName = rdr["LastName"].ToString();
                    student.Email = rdr["Email"].ToString();
                    student.Mobile = rdr["Mobile"].ToString();
                    student.Address = rdr["Address"].ToString();

                    lstStudent.Add(student);
                }
                con.Close();
            }
            return lstStudent;
        }

        public void EditStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", student.Id);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public string DeleteStudent(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spDeleteStudent", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    con.Close();

                    if (rowsAffected > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Student not found";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as required
                return "Error: " + ex.Message;
            }
        }

        public Student GetStudentDetails(int id)
        {
            Student student = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetStudentDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    student = new Student
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        FirstName = rdr["FirstName"].ToString(),
                        LastName = rdr["LastName"].ToString(),
                        Email = rdr["Email"].ToString(),
                        Mobile = rdr["Mobile"].ToString(),
                        Address = rdr["Address"].ToString()
                    };
                }

                con.Close();
            }
            return student;
        }
    }
}
