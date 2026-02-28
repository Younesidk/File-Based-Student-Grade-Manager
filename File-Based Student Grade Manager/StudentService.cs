using System;
using System.Collections.Generic;
using System.Text;

namespace File_Based_Student_Grade_Manager;

public class StudentService
{
    private readonly string FilePath;

    public StudentService(string FilePath)
    {
        this.FilePath = FilePath;
    }

    public List<StudentModel> LoadStudents()
    {
        var FileContent = File.ReadAllLines(FilePath);

        List<StudentModel> students = new();

        foreach (var line in FileContent)
        {
            var NewStudent = StudentModel.FromCsv(line);

            if (NewStudent is null)
                continue;

            students.Add(NewStudent);
        }

        return students;
    }

    public void SaveStudents(List<StudentModel> students)
    {
        students = students.OrderBy(s => s.ID).ToList();

        var lines = new List<string>();

        foreach (var s in students)
        {
            lines.Add(s.ToString());
        }

        File.WriteAllLines(FilePath, lines);
    }

    public void AddStudent(StudentModel student)
    {
        if (student is null)
            return;

        var Students = LoadStudents();

        var MaxID = Students.Any() ? Students.Max(s => s.ID) : 0;
        student.ID = MaxID + 1;

        Students.Add(student);
        SaveStudents(Students);
    }

    public StudentModel? GetStudentByID(int ID)
    {
        var students = LoadStudents();

        var TargetStudent = students.Find(s => s.ID == ID);

        return TargetStudent;
    }

    public void EditStudent(StudentModel student,int TargetID)
    {
        student.ID = TargetID;
        var Students = LoadStudents();

        var Position = Students.FindIndex(s => s.ID == TargetID);

        if (Position < 0)
            return;

        Students[Position] = student;


        SaveStudents(Students);
    }

    public void DeleteStudent(StudentModel student)
    {
        var students = LoadStudents();
        students.RemoveAll(s => s.ID == student.ID);
        SaveStudents(students);
    }

    public void DeleteStudent(int ID)
    {
        var Students = LoadStudents();
        var student = Students.Find(s => s.ID == ID);
        if (student is null)
            return;

        Students.RemoveAll(s => s.ID == ID);
        SaveStudents(Students);
    }
}