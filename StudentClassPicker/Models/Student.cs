using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace StudentClassPicker.Models
{
    public partial class Student : ObservableObject
    {       
        public int studentID { get; set; }

        [ObservableProperty]
        public string studentName;

        [ObservableProperty]
        public string studentSurname;

        public Student(int studentID, string studentName, string studentSurname)
        {
            this.studentID = studentID;
            this.studentName = studentName;
            this.studentSurname = studentSurname;
        }
    }
}
