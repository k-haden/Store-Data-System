//////////////////////////////////Header///////////////////////////////////
///Kelli Haden
///TINFO 200c
///STUDENTDB
///
//////////////////////////////Change History/////////////////////////////
///DATE             DEVELOPER       DESCRIPTION
///2019-02-19       KHADEN2         Beginning of studentdb, creation of variables
///2019-02-21       KHADEN2         creation of tostring, tostringfile, and databaseapp
///2019-02-26       KHADEN2         user interface, quit, save, and change to Test
///2019-02-28       KHADEN2         read in capability
///2019-03-07       KHADEN2         create grad and under grad and inherit from student
///2019-03-08       KHADEN2         change ReadDataFromInputFile to read in grad and undergrad
///2019-03-09       KHADEN2         edit comments/clean code

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    class DatabaseApp
    {
        public const bool DebugTestMode = false;
        public const int NotFound = -1;

        // actual storage for the students
        private List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            DatabaseApp dbApp = new DatabaseApp();
        }

        public DatabaseApp()
        {
            GoDatabase(); //start database
        }

        private void GoDatabase()
        {
            // just until we can get the input file working
            //if (DebugTestMode) TestMain();

            //Read in data from file
            ReadDataFromInputFile();

            Console.WriteLine(
@"
------------------------------------------------------------------------------------
 Welcome to the Student Database Program. In this program you will be given a list 
 of options to choose from below. Please make a selection for the letters listed 
 between [] brackets or when prompted enter another format. It is IMPORTANT that 
 you save after each change is made to the database before exiting. 

                    Please make your selection below.
------------------------------------------------------------------------------------");

            while (true)
            {
                // display the main menu to the user
                DisplayMainMenu();

                // get a selection from the user
                ConsoleKeyInfo selection = GetSelectionFromUser();

                // use that selection in a switch statement to execute a CRUD operation
                switch (selection.KeyChar)
                {
                    case 'P':
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'C':
                    case 'c':
                        CreateNewStudentRecord();
                        break;
                    case 'F':
                    case 'f':
                        FindStudentRecord();
                        break;
                    case 'U':
                    case 'u':
                        UpdateStudentRecord();
                        break;
                    case 'D':
                    case 'd':
                        DeleteStudentRecord();
                        break;
                    case 'S':
                    case 's':
                        // this is to save without exiting the app
                        WriteDataToOutputFile();
                        Console.WriteLine("  ...File Saved.");
                        break;
                    case 'Q':
                    case 'q':
                        WriteDataToOutputFile();
                        QuitDatabaseApp();
                        break;
                    default:
                        break;
                }
            }
        }
        // allows user to find student records in index or tells user record is not in system
        // post conditions: prints out student record that was requested by user
        private void FindStudentRecord()
        {
            int location = GetIndexFromList();
            if (location == NotFound)
            {
                Console.WriteLine("Record Not Found");
            }
            else
            {
                //print to console
                Console.WriteLine($"{students[location]}");
            }
        }
        // allows user to create a new student record
        // post conditions: new student record is added to index
        private void CreateNewStudentRecord()
        {
            Console.WriteLine("\nCreating a new Student Record.");
            string email = string.Empty;
            bool emailNotAvailable = false;

            do
            {
                Console.Write("Choose an email address (must be system unique): ");
                email = Console.ReadLine();
                emailNotAvailable = false;

                for (int i = 0; i < students.Count; i++)
                {
                    if(students[i].EmailAddress == email)
                    {
                        emailNotAvailable = true;
                        Console.WriteLine($"Sorry, {email} is in use  - it is not available.");
                        break;
                    }
                }
            } while (emailNotAvailable);

            // rest of the student create
            Console.Write("Is the student an Undergrad or Grad Student?\nEnter 'U' for Undergrad or 'G' for Grad: ");
            char underOrGrad = char.Parse(Console.ReadLine());
            if (underOrGrad == 'U' || underOrGrad == 'u')
            {
                Console.WriteLine("Enter a First Name: ");
                string first = Console.ReadLine();
                Console.WriteLine("Enter a Last Name: ");
                string last = Console.ReadLine();
                Console.WriteLine("Enter a Year Rank(1, 2, 3, or 4): ");
                int yearRank = int.Parse(Console.ReadLine());
                YearRank rank = YearRank.Freshman;
                while (yearRank < 1 || yearRank > 4)
                {
                    Console.WriteLine("Please Enter 1, 2, 3, or 4: ");
                    yearRank = int.Parse(Console.ReadLine());
                }
                if (yearRank == 1)
                {
                    rank = YearRank.Freshman;
                }
                if (yearRank == 2)
                {
                    rank = YearRank.Sophomore;
                }
                if (yearRank == 3)
                {
                    rank = YearRank.Junior;
                }
                if (yearRank == 4)
                {
                    rank = YearRank.Senior;
                }
                float gpa = 0.0f;
                students.Add(new Undergrad(students.Count, first, last, email, DateTime.Now, rank, gpa));
            }
            if (underOrGrad == 'G' || underOrGrad == 'g')
            {
                Console.WriteLine("Enter a First Name: ");
                string first = Console.ReadLine();
                Console.WriteLine("Enter a Last Name: ");
                string last = Console.ReadLine();
                Console.WriteLine("Enter a Faculty Advisor: ");
                string advisor = Console.ReadLine();
                Console.WriteLine("Enter the amount of tuition credit: ");
                decimal credit = decimal.Parse(Console.ReadLine());
                students.Add(new GradStudent(students.Count, first, last, email, DateTime.Now, advisor, credit));
            }
        }
        // allows each record to be updated via proper menu options if grad or undergrad student. 
        // post conditions: Student records are updated with changes made
        private void UpdateStudentRecord()
        {
            Console.WriteLine("\nAttempting to update student record.\n");

            // assume that the return val in location is absolutely within range
            int location = GetIndexFromList();

            //use RTTI to determine what kind of student
            if (students[location] is Undergrad)
            {
                EditUndergradRecord(location);
            }
            else
            {
                EditGradStudentRecord(location);
            }
        }
        // make menu selection to edit undergrad records
        // post conditions: after running updates are added to index
        private void EditUndergradRecord(int location)
        {
            Undergrad under = (Undergrad)students[location];

            Console.WriteLine($"     Student ID:    {under.StudentID} (**readonly field)");
            Console.WriteLine($"   [F]irst name:    {under.FirstName}");
            Console.WriteLine($"   [L]ast  name:    {under.LastName}");
            Console.WriteLine($"  Email address:    {under.EmailAddress} (**readonly filed)");
            Console.WriteLine($"[D]ate enrolled:    {under.EnrollmentDate}");
            Console.WriteLine($"    [Y]ear Rank:    {under.Rank}");
            Console.WriteLine($"[G]rade Pt. Avg:    {under.GradePointAverage}");
            Console.Write("Enter selection:    ");

            char selection = char.Parse(Console.ReadLine());

            switch (selection)
            {
                case 'F':
                case 'f':
                    Console.Write("Enter the new first name: ");
                    students[location].FirstName = Console.ReadLine();
                    break;
                case 'L':
                case 'l':
                    Console.Write("Enter a new Last Name: ");
                    students[location].LastName = Console.ReadLine();
                    break;
                case 'D':
                case 'd':
                    Console.Write("Enter a new Enrollment Date: ");
                    students[location].EnrollmentDate = DateTime.Parse(Console.ReadLine());
                    break;
                case 'Y':
                case 'y':
                    Console.Write("Enter the new year rank in school [1, 2, 3, 4] only: ");
                    int yearRank = int.Parse(Console.ReadLine());
                    if(YearRank.Freshman.Equals(yearRank)) under.Rank = YearRank.Freshman;
                    else if (YearRank.Sophomore.Equals(yearRank)) under.Rank = YearRank.Sophomore;
                    else if (YearRank.Junior.Equals(yearRank)) under.Rank = YearRank.Junior;
                    else if (YearRank.Senior.Equals(yearRank)) under.Rank = YearRank.Senior;
                    break;
                case 'G':
                case 'g':
                    Console.Write("Enter the new GPA: ");
                    under.GradePointAverage = float.Parse(Console.ReadLine());
                    break;
                default:
                    break;
            }
        }
        // make menu selection for grad student record
        // post conditions: after running updates are added to index
        private void EditGradStudentRecord(int location)
        {
            GradStudent grad = (GradStudent)students[location];
            Console.WriteLine($"       Student ID: {grad.StudentID} (**readonly field)");
            Console.WriteLine($"     [F]irst Name: {grad.FirstName}");
            Console.WriteLine($"      [L]ast Name: {grad.LastName}");
            Console.WriteLine($"    Email Address: {grad.EmailAddress} (**readonly field)");
            Console.WriteLine($"  [D]ate Enrolled: {grad.EnrollmentDate}");
            Console.WriteLine($"        [A]dvisor: {grad.FacultyAdvisor}");
            Console.WriteLine($" [T]uition Credit: {grad.TuitionCredit}");
            Console.WriteLine("   Enter Selection: ");

            char selection = char.Parse(Console.ReadLine());
            switch (selection)
            {
                case 'F':
                case 'f':
                    Console.Write("Enter a new First Name: ");
                    students[location].FirstName = Console.ReadLine();
                    break;
                case 'L':
                case 'l':
                    Console.Write("Enter a new Last Name: ");
                    students[location].LastName = Console.ReadLine();
                    break;
                case 'D':
                case 'd':
                    Console.Write("Enter a new Enrollment Date: ");
                    students[location].EnrollmentDate = DateTime.Parse(Console.ReadLine());
                    break;
                case 'A':
                case 'a':
                    Console.Write("Enter a new Faculty Advisor: ");
                    grad.FacultyAdvisor = Console.ReadLine();
                    break;
                case 'T':
                case 't':
                    Console.Write("Enter a new Tuition Credit ");
                    grad.TuitionCredit = decimal.Parse(Console.ReadLine());
                    break;
                default:
                    break;
            }
        }

        // deletes a record in the List<> if it exists 
        // Postconditions - after returning, the record is not in the list<>
        private void DeleteStudentRecord()
        {
            // prompt the user for the record to be deleted
            // check the list<> to see if its in there
            int location = GetIndexFromList();

            if(location == NotFound)
            {
                Console.WriteLine("Record not found. Can't delete.");
            }
            else
            {
                // if it is in the list<> - remove the record from the list
                Console.WriteLine($"Removing: {students[location].FirstName} {students[location].LastName}");
                students.RemoveAt(location);
                Console.WriteLine(" ... Record Deleted.");
            }
        }

        // display a compact view of the students to the user so 
        // that a student can be selected
        private int GetIndexFromList()
        {
            Console.WriteLine("\n********************************");
            Console.WriteLine("** Student email address list **");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i} :: {students[i].EmailAddress}");
            }
            Console.Write("\nSELECT email: ");
            int location = int.Parse(Console.ReadLine());

            return location;
        }
        // reads data from input file
        // post conditions: reads input file to display grad and undergrad students correct menu formatting
        private void ReadDataFromInputFile()
        {
            // construct an object connected to the input file
            StreamReader infile = new StreamReader("STUDENT_DATABASE_INPUT_FILE.txt");

            string str = string.Empty;
            // read the data in from the file - and store it in the list<>
            while ((str = infile.ReadLine()) != null)
            {
                int id = int.Parse(str);
                string first = infile.ReadLine();
                string last = infile.ReadLine();
                string email = infile.ReadLine();
                DateTime enrolled = DateTime.Parse(infile.ReadLine());
                string advisorOrRank = infile.ReadLine();
                if (advisorOrRank == "1" || advisorOrRank == "2" || advisorOrRank == "3" || advisorOrRank == "4")
                {
                    YearRank rank = (YearRank)Enum.Parse(typeof(YearRank), advisorOrRank);
                    float gpa = float.Parse(infile.ReadLine());
                    Student stu = new Undergrad(id, first, last, email, enrolled, rank, gpa);
                    students.Add(stu);
                }
                else
                {
                    decimal credit = decimal.Parse(infile.ReadLine());
                    Student stu = new GradStudent(id, first, last, email, enrolled, advisorOrRank, credit);
                    students.Add(stu);
                }
            }
            // close the file
            infile.Close();
        }
        // prints all student records 
        private void PrintAllRecords()
        {
            for (int i = 0; i < students.Count; ++i)
            {
                Console.WriteLine(students[i]);
            }
        }

        private void QuitDatabaseApp()
        {
            // normal exit with no errors = 0
            Environment.Exit(0);
        }

        private ConsoleKeyInfo GetSelectionFromUser()
        {
            return Console.ReadKey();
        }
        // Main menu options
        private void DisplayMainMenu()
        {
            Console.Write(@"
********************************
** Student database main menu **
** [P]rint all records
** [C]reate a record
** [F]ind a record
** [U]pdate a record
** [D]elete a record
** [S]ave database w/o exit
** [Q]uit and Save the database
**
** ENTER SELECTION: ");
        }
        // writes saved data to outputfile
        public void WriteDataToOutputFile()
        {
            // create the otuput file

            StreamWriter outfile = new StreamWriter("STUDENT_DATABASE_OUTPUT_FILE.txt");

            for (int i = 0; i < students.Count; ++i)
            {
                // output each student in the array list
                outfile.WriteLine(students[i].ToStringFileFormat());
            }

            // close the output file
            outfile.Close();
        }

        public void TestMain()
        {
            // creating student objects for testing the POCO class
            Student stu01 = new Undergrad("Alice", "Anderson", "aanderson@uw.edu", YearRank.Freshman, 3.1f);
            Student stu02 = new Undergrad("Bob", "Bradshaw", "bbradshaw@uw.edu", YearRank.Sophomore, 3.2f);
            Student stu03 = new Undergrad("Chuck", "Costarella", "ccostarella@uw.edu", YearRank.Junior, 3.3f);

            Student stu04 = new GradStudent("David", "Davis", "ddavis@uw.edu", "Dr. Donald Chinn", 11111.99m);

            // but now with the new dynamic arraylist, here is the "add" operation
            students.Add(stu01);
            students.Add(stu02);
            students.Add(stu03);
            students.Add(stu04);
        }
    }
}
