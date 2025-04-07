using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentClassPicker.Models
{
    public partial class Class : ObservableObject
    {
        public int classID { get; set; }

        [ObservableProperty]
        public string className;
        //public string classFileName { get; set; }
        public ObservableCollection<Student> classStudentList { get; set; }
        public Class() 
        {
            classID = 1;
            className = "Klasa";
            //classFileName = String.Empty;
            classStudentList = new ObservableCollection<Student>();
        }

        public Class(int classID, string className, string classFileName, ObservableCollection<Student> classStudentList)
        {
            this.classID = classID;
            this.className = className;
            //this.classFileName = classFileName;
            this.classStudentList = classStudentList;
        }
    }
}
