using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentClassPicker.ViewModels
{
    public class StudentListViewModel
    {
        public ObservableCollection<Models.Student> StudentList { get; set; } = new ObservableCollection<Models.Student>();

        public StudentListViewModel(Models.Class selectedClass)
        {
            StudentList.Clear();
            StudentList = selectedClass.classStudentList;
        }
    }
}
