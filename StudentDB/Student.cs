//////////////////////////////HEADER////////////////////////////////////////
///KELLI HADEN
///TINFO 200C
///StudentDB
///
////////////////////////////Change History//////////////////////////////////
///DATE          DEVELOPER   DESCRIPTION
///2019-02-19    KHADEN2    Beginning of studentdb, creation of variables
///2019-02-21    KHADEN2    creation of tostring, tostringfile, and databaseapp
///2019-02-26    KHADEN2    user interface, quit, save, and change to Test
///2019-02-28    KHADEN2    read in capability
///2019-03-07    KHADEN2    create grad and under grad and inherit from student
///2019-03-08    KHADEN2    change ReadDataFromInputFile to read in grad and undergrad
///2019-03-09    KHADEN2    edit comments/clean code


using System;

namespace StudentDB
{
    public class Student
    {
        // storage for data in the object
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string EmailAddress { get; set; }

        // ctor for making student objects from data that is prompted 
        // interactively by the db operator
        public Student(string first, string last, string email)
        {
            FirstName = first;
            LastName = last;
            EmailAddress = email;
            EnrollmentDate = DateTime.Now;
        }
        public Student(int id, string first, string last, string email)
        {
            StudentID = id;
            FirstName = first;
            LastName = last;
            EmailAddress = email;
            EnrollmentDate = DateTime.Now;
        }

        // ctor for making student objects from data read in from the input file
        public Student(int id, string first, string last, string email, DateTime time)
        {
            StudentID = id;
            FirstName = first;
            LastName = last;
            EmailAddress = email;
            EnrollmentDate = time;
        }

        // convenient ways to get the data out of the objects
        public override string ToString()
        {
            string str = string.Empty;

            str += "\n***** Student data *****";
            str += "\nFirst name: " + FirstName;
            str += "\n Last name: " + LastName;
            str += "\nEmail addr: " + EmailAddress;
            str += "\n  Enrolled: " + EnrollmentDate;

            return str;
        }
        public virtual string ToStringFileFormat()
        {
            string str = "" + StudentID;
            str += "\n" + FirstName;
            str += "\n" + LastName;
            str += "\n" + EmailAddress;
            str += "\n" + EnrollmentDate;

            return str;
        }
    }
}







