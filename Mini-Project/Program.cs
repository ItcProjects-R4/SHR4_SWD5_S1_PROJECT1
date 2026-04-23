using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Student_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=StudentDB;Integrated Security=True;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Show Students");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter student name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter student age: ");
                        int age = int.Parse(Console.ReadLine());
                        Console.Write("Enter student grade: ");
                        string grade = Console.ReadLine();
                        Student newStudent = new Student(name, age, grade);
                        newStudent.AddStudent(connection);
                        break;
                    case "2":
                        Student.ShowAllStudents(connection);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
