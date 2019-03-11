//////////////////////////////HEADER////////////////////////////////////////
///KELLI HADEN
///TINFO 200C
///StudentDB
///
////////////////////////////Change History//////////////////////////////////
///DATE         DEVELOPER   DESCRIPTION
///2019.3.7     KHADEN2     file creation for grad student/ set up ctor and override
///2019-03-08   KHADEN2     change ReadDataFromInputFile to read in grad and undergrad
///2019-03-09   KHADEN2     edit comments/clean code

using System;

namespace StudentDB
{
    public class GradStudent : Student
    {
        public string FacultyAdvisor { get; set; }

        public decimal TuitionCredit { get; set; }

        //ctor used by the app to read in data from the input file
        // inputs - 
        // postcondition - an GradStudent object is instantiated and initialized correctly
        public GradStudent(int id, string first, string last, string email, DateTime enroll, string advisor, decimal credit)
            : base(id, first, last, email, enroll)
        {
            FacultyAdvisor = advisor;
            TuitionCredit = credit;
        }

        //ctor for interactive use in the data interactively from the user
        //inputs - 
        // postcondition - an GradStudent object is instantiated and initialized correctly
        public GradStudent(string first, string last, string email, string advisor, decimal credit)
            : base(first, last, email)
        {
            FacultyAdvisor = advisor;
            TuitionCredit = credit;
        }

        //expression-bodied method to override tostring
        //this is the labled output for human-readable display
        public override string ToString() => base.ToString() + "\n   Advisor: " + FacultyAdvisor + "\n    Credit: " + TuitionCredit;

        //expression-bodied method to override tostring
        //this is the labled output for human-readable display
        public override string ToStringFileFormat() => base.ToString() + "\n" + FacultyAdvisor + "\n" + TuitionCredit;
    }
}