using File_Based_Student_Grade_Manager;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Transactions;
using System.Xml.Schema;
using System.Windows.Forms;

class Program
{
    private const string FilePath = "students.txt";
    private static StudentService service = new(FilePath);
    private static List<StudentModel> students = service.LoadStudents();

    private const int Max = 6;
    private const int Min = 0;

    private static Dictionary<int, Action> Actions = new()
    {
        {1, DisplayStudents },
        {2, AddStudent },
        {3, DisplayStudentsAverage },
        {4, EditStudent },
        {5, DeleteStudent },
        {6, ExportToCsvFile }
    };

    [STAThread]
    public static void Main()
    {

        while (true)
        {
            Menu();
            Console.WriteLine("Enter your Choice");
            int Choice = ValidateInput<int>(c => c is < Min or > Max);

            Console.Clear();

            if (Choice == 0)
            {
                Console.WriteLine("See you Next Time !!");
                return;
            }

            if (Actions.TryGetValue(Choice, out var action))
                action();
            else
                Console.WriteLine("Invalid Input");

            Console.WriteLine("\nEnter Key to Proceed...");
            Console.ReadKey();
            Console.Clear();
        }
    }


    public static void DisplayStudents() => 
        students?.ForEach(Console.WriteLine);

    public static void AddStudent()
    {
        var student = CreateStudent();

        service.AddStudent(student);
        students = service.LoadStudents();
    }

    public static void DisplayStudentsAverage()
    {
        var student = GetStudentID("To Get his Average");

        Console.Clear();

        Console.WriteLine($"The Student : {student} \nAverage is : {student.Average}");
    }

    public static void EditStudent()
    {
        var student = GetStudentID("You with to Edit");

        Console.WriteLine(student);

        Console.WriteLine("Now Enter the Editted One");

        var EditStudent = CreateStudent();

        service.EditStudent(EditStudent,student.ID);

        students = service.LoadStudents();
    }

    public static void DeleteStudent()
    {
        var student = GetStudentID("You Wish to Delete");

        Console.WriteLine("Are you Sure you wanna Delete it ?(y/n)");

        char IsSure = ValidateInput<char>(c => c is not ('y' or 'Y' or 'n' or 'N'));

        if (!(IsSure == 'y' || IsSure == 'Y'))
            return;

        service.DeleteStudent(student);

        students = service.LoadStudents();
    }

    public static void Menu()
    {
        Console.WriteLine("=====Student Grade Manager=====");
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Display Students");
        Console.WriteLine("2. Add Student");
        Console.WriteLine("3. See a Student's Grade");
        Console.WriteLine("4. Edit a Student");
        Console.WriteLine("5. Delete a Student");
        Console.WriteLine("6. Export All Students as a CSV File");
    }

    public static StudentModel CreateStudent()
    {
        Console.WriteLine("Enter Details in this Order : First Name - Last Name - Age - First Exam - Second Exam - Third Exam");

        Func<string, bool> NamesValidation = name => name is null;

        var FName = ValidateInput(NamesValidation);
        var LName = ValidateInput(NamesValidation);

        int Age = ValidateInput<int>(Age => Age is < 18 or > 30);

        Func<double, bool> GradeValidation = grade => grade is < 0 or > 20;

        double Exam1 = ValidateInput(GradeValidation);
        double Exam2 = ValidateInput(GradeValidation);
        double Exam3 = ValidateInput(GradeValidation);

        return new StudentModel(FName, LName, Age, Exam1, Exam2, Exam3);
    }

    public static StudentModel GetStudentID(string? Prompt = null)
    {
        while (true)
        {
            DisplayStudents();
            Console.WriteLine($"Enter the Student's ID {Prompt}");

            int ID = ValidateInput<int>();

            var student = service.GetStudentByID(ID);

            if (student is null)
            {
                Console.WriteLine("No Students with this ID were Found");
                Console.WriteLine("Try again");
                Console.ReadKey();
                Console.Clear();
            }
            else
                return student;
        }
    }

    public static T ValidateInput<T>(Func<T, bool>? additionalCondition = null)
        where T : IParsable<T>
    {
        T result;
        while (true)
        {
            var input = Console.ReadLine();

            if (T.TryParse(input, null, out result))
            {
                if (additionalCondition == null || !additionalCondition(result))
                    break;
            }

            Console.WriteLine("Invalid input. Try again.");
        }

        return result;
    }

    public static void ExportToCsvFile()
    {
        using (var sfd = new SaveFileDialog())
        {

            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = "students.csv";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            service.ExportToCsv(students, sfd.FileName);
        }
    }

}