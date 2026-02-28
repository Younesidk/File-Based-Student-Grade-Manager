using System;
using System.Collections.Generic;
using System.Text;

namespace File_Based_Student_Grade_Manager;

public class StudentModel
{
    public int ID { get { return field; } set; }
    private string FirstName { get { return field; } set; }
    private string LastName { get { return field; } set; }
    private int Age { get { return field; }  set; }
    private double Exam1 { get { return field; }  set; }
    private double Exam2 { get { return field; } set; }
    private double Exam3 { get { return field; } set; }

    public StudentModel(int ID,string FirstName, string LastName, int Age, double Exam1, double Exam2, double Exam3)
    {
        this.ID = ID;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Age = Age;
        this.Exam1 = Exam1;
        this.Exam2 = Exam2;
        this.Exam3 = Exam3;
    }

    public StudentModel(string FirstName, string LastName, int Age, double Exam1, double Exam2, double Exam3)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Age = Age;
        this.Exam1 = Exam1;
        this.Exam2 = Exam2;
        this.Exam3 = Exam3;
    }

    public static StudentModel? FromCsv(string line)
    {
        const int NumberOfParts = 7;
        var parts = line.Split(',');

        if (parts.Length != NumberOfParts)
            return null;            

        return new StudentModel(
            int.Parse(parts[0]),
            parts[1],
            parts[2],
            int.Parse(parts[3]),
            double.Parse(parts[4]),
            double.Parse(parts[5]),
            double.Parse(parts[6])
        );
    }

    public double Average => (Exam1 + Exam2 + Exam3) / 3;


    public override string ToString()
    {
        return $"{ID},{FirstName},{LastName},{Age},{Exam1},{Exam2},{Exam3}";
    }

}
