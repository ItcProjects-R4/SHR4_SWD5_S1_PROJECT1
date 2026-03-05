using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Student_Management_System
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }

        public Student(string name, int age, string grade)
        {
            Name = name;
            Age = age;
            Grade = grade;
        }

        public void AddStudent(SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand("AddStudent", connection))
            {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Age", Age);
                    cmd.Parameters.AddWithValue("@Grade", Grade);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine("Student added to the database successfully.");
            }
        }
        public static void ShowAllStudents(SqlConnection connection)
        {
            List<Student> students = new List<Student>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM View_Students", connection))
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student s = new Student(
                        reader["Name"].ToString(),
                        Convert.ToInt32(reader["Age"]),
                        reader["Grade"].ToString()
                    );

                    students.Add(s);
                }
                connection.Close();
            }
            foreach (var student in students)
            {
                Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
            }
        }
        public static void PrintInfo(Student student)
        {
            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
        }
    }
}