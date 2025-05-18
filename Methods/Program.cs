using System;
using System.Collections.Generic;

namespace StudentManagementSystem
    {
        class Student
        {
            public int StudentId;
            public string Name;
            public int Age;
            public List<Course> Courses = new List<Course>();

            public bool Enroll(Course course)
            {
                if (!Courses.Contains(course))
                {
                    Courses.Add(course);
                    return true;
                }
                return false;
            }

            public string PrintDetails()
            {
                string courseNames =  "";
                foreach (var course in Courses)
                {
                    courseNames += course.Title + ", ";
                }
                return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {courseNames}";
            }
        }

        class Instructor
        {
            public int InstructorId;
            public string Name;
            public string Specialization;

            public string PrintDetails()
            {
                return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
            }
        }

        class Course
        {
            public int CourseId;
            public string Title;
            public Instructor Instructor;

            public string PrintDetails()
            {
                return $"ID: {CourseId}, Title: {Title}, Instructor: {(Instructor != null ? Instructor.Name : "None")}";
            }
        }

        class SchoolStudentManager
        {
            public List<Student> Students = new List<Student>();
            public List<Course> Courses = new List<Course>();
            public List<Instructor> Instructors = new List<Instructor>();

            public bool AddStudent(Student student)
            {
                Students.Add(student);
                return true;
            }

            public bool AddInstructor(Instructor instructor)
            {
                Instructors.Add(instructor);
                return true;
            }

            public bool AddCourse(Course course)
            {
                Courses.Add(course);
                return true;
            }

            public Student FindStudent(int id)
            {
                foreach (var s in Students)
                {
                    if (s.StudentId == id)
                        return s;
                }
                return null;
            }

            public Course FindCourse(int id)
            {
                foreach (var c in Courses)
                {
                    if (c.CourseId == id)
                        return c;
                }
                return null;
            }

            public Instructor FindInstructor(int id)
            {
                foreach (var i in Instructors)
                {
                    if (i.InstructorId == id)
                        return i;
                }
                return null;
            }

            public bool EnrollStudentInCourse(int studentId, int courseId)
            {
                var student = FindStudent(studentId);
                var course = FindCourse(courseId);
                if (student != null && course != null)
                {
                    return student.Enroll(course);
                }
                return false;
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                SchoolStudentManager manager = new SchoolStudentManager();

                while (true)
                {
                    Console.WriteLine("\n            Student Management System      ");
                    Console.WriteLine("\n1. Add a new Student");
                    Console.WriteLine("2. View all Students");
                    Console.WriteLine("3. Search for a Student by ID");
                    Console.WriteLine("4. Update Student Information");
                    Console.WriteLine("5. Delete a Student");
                    Console.WriteLine("6. Add  a new Course");
                    Console.WriteLine("7. Add a new Instructor");
                    Console.WriteLine("8. Enroll Student in Course");
                    Console.WriteLine("9. View all Courses");
                    Console.WriteLine("10. View all Instructors");
                    Console.WriteLine("11. Exit");
                    
                    Console.Write("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1) //Add a new Student
                    {
                        Console.Write("Student ID: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Age: ");
                        int age = Convert.ToInt32(Console.ReadLine());

                        Student student = new Student { StudentId = id, Name = name, Age = age };
                        manager.AddStudent(student);
                        Console.WriteLine("Student added.");
                    }
                    else if (choice == 2) //View all Students
                    {
                       foreach (var s in manager.Students)
                       {
                        Console.WriteLine(s.PrintDetails());
                       }
                    }
                    else if (choice == 3) //Search for a Student by ID
                    {
                       Console.Write("Enter Student ID to search: ");
                       int searchId = Convert.ToInt32(Console.ReadLine());
                       Student found = manager.FindStudent(searchId);
                       if (found != null)
                       {
                        Console.WriteLine("Student found:");
                        Console.WriteLine(found.PrintDetails());
                       }
                       else
                       {
                        Console.WriteLine("Student not found.");
                       }
                    }
                    else if (choice == 4) //Update Student Information
                    {
                       Console.Write("Enter Student ID to update: ");
                       int updateId = int.Parse(Console.ReadLine());

                       Student student = manager.FindStudent(updateId);
                       if (student != null)
                       {
                          Console.WriteLine("Student found. Leave blank if you don't want to change a field.");
                          Console.Write("New Name (current: " + student.Name + "): ");
                          string newName = Console.ReadLine();
                          if (!string.IsNullOrWhiteSpace(newName))
                          {
                            student.Name = newName;
                          }
                          Console.Write("New Age (current: " + student.Age + "): ");
                          string newAgeStr = Console.ReadLine();
                          if (!string.IsNullOrWhiteSpace(newAgeStr) && int.TryParse(newAgeStr, out int newAge))
                          {
                            student.Age = newAge;
                          }
                          Console.WriteLine("Student information updated.");
                       }
                       else
                       {
                         Console.WriteLine("Student not found.");
                       }
                    }
                    else if (choice == 5) //Delete a Student
                    {
                       Console.Write("Enter Student ID to delete: ");
                       int deleteId = int.Parse(Console.ReadLine());

                       Student student = manager.FindStudent(deleteId);
                       if (student != null)
                       {
                         manager.Students.Remove(student);
                         Console.WriteLine("Student deleted.");
                       }
                       else
                       {
                        Console.WriteLine("Student not found.");
                       }
                    }
                    else if (choice == 6) //Add  a new Course
                    {
                        Console.Write("Course ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Instructor ID: ");
                        int instId = int.Parse(Console.ReadLine());

                        Instructor instructor = manager.FindInstructor(instId);
                        if (instructor != null)
                        {
                            Course course = new Course { CourseId = id, Title = title, Instructor = instructor };
                            manager.AddCourse(course);
                            Console.WriteLine("Course added.");
                        }
                        else
                        {
                            Console.WriteLine("Instructor not found.");
                        }
                    }
                    else if (choice == 7) //Add a new Instructor
                    {
                       Console.Write("Instructor ID: ");
                       int id = int.Parse(Console.ReadLine());
                       Console.Write("Name: ");
                       string name = Console.ReadLine();
                       Console.Write("Specialization: ");
                       string spec = Console.ReadLine();
                       Instructor instructor = new Instructor { InstructorId = id, Name = name, Specialization = spec };
                       manager.AddInstructor(instructor);
                       Console.WriteLine("Instructor added.");
                    }
                    else if (choice == 8) //Enroll a Student in a Course
                    {
                       Console.Write("Student ID: ");
                       int sid = int.Parse(Console.ReadLine());
                       Console.Write("Course ID: ");
                       int cid = int.Parse(Console.ReadLine());
                       bool enrolled = manager.EnrollStudentInCourse(sid, cid);
                       Console.WriteLine(enrolled ? "Student enrolled." : "Enrollment failed.");
                    }
                    else if (choice == 9) //View all Courses
                    {
                        foreach (var c in manager.Courses)
                        {
                            Console.WriteLine(c.PrintDetails());
                        }
                    }
                    else if (choice == 10) //View all Instructors
                    {
                        foreach (var i in manager.Instructors)
                        {
                            Console.WriteLine(i.PrintDetails());
                        }
                    }
                    else if (choice == 11) //exit or error
                    {
                        Console.WriteLine("Goodbye!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Try again.");
                    }
                }
            }
        }
    }

