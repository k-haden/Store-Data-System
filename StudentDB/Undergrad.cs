//////////////////////////////HEADER////////////////////////////////////////
///KELLI HADEN
///TINFO 200C
///StudentDB
///
////////////////////////////Change History//////////////////////////////////
///DATE         DEVELOPER   DESCRIPTION
///2019.3.7     KHADEN2     file creation for undergrad/ set up ctor and override
///2019-03-08   KHADEN2     change ReadDataFromInputFile to read in grad and undergrad
///2019-03-09   KHADEN2     edit comments/clean code

using System;

namespace StudentDB
{
    public enum YearRank
    {
        Freshman = 1,
        Sophomore = 2,
        Junior = 3,
        Senior =4,
    }

    public class Undergrad : Student
    {
        public YearRank Rank { get; set; }
        public float GradePointAverage { get; set; }

        //ctor used by the app to read in data from the input file
        // inputs - 
        // postcondition - an undergrad object is instantiated and initialized correctly
        public Undergrad(int id, string first, string last, string email, DateTime enroll, YearRank rank, float gpa)
            : base(id, first, last, email, enroll)
        {
            Rank = rank;
            GradePointAverage = gpa;
        }

        //ctor for interactive use in the database app
        //inputs - 
        // postcondition - an undergrad object is instantiated and initialized correctly
        public Undergrad(string first, string last, string email, YearRank rank, float gpa)
            : base(first, last, email)
        {
            Rank = rank;
            GradePointAverage = gpa;
        }

        //expression-bodied method to override tostring
        //this is the labled output for human-readable display
        public override string ToString() => base.ToString() + "\n      Year: " + Rank + "\n       GPA: " + GradePointAverage;

        //expression-bodied method to override tostring
        //this is the labled output for human-readable display
        public override string ToStringFileFormat() => base.ToString() + "\n" + Rank + "\n" + GradePointAverage;
    }
}