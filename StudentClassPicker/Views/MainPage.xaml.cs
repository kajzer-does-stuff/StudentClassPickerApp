using StudentClassPicker.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;

namespace StudentClassPicker
{
    public partial class MainPage : ContentPage
    {
        private Models.Class currentSelectedClass;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.ClassListViewModel();
        }

        protected override void OnAppearing()
        {
            ((ViewModels.ClassListViewModel)BindingContext).LoadClassList();
        }

        private void MainClassList_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (args.CurrentSelection.Count != 0)
            {
                currentSelectedClass = (Models.Class)args.CurrentSelection[0];

                SelectedClassNameLabel.Text = currentSelectedClass.ClassName;
                CurrentStudentList.BindingContext = new ViewModels.StudentListViewModel(currentSelectedClass);
            }
        }
        private void ClassNameEditor_TextChanged(object sender, EventArgs e)
        {
            Class senderClass = (Class)(sender as Editor).BindingContext;

            string editedName = (sender as Editor).Text;

            if(!string.IsNullOrEmpty(editedName))
            {
                senderClass.ClassName = editedName;
                ViewModels.ClassListViewModel.SaveClass(senderClass, senderClass.classFileName);
            }

            if(currentSelectedClass == senderClass)
            {
                SelectedClassNameLabel.Text = editedName;
            }

        }

        private void StudentNameEditor_TextChanged(object sender, EventArgs e)
        {
            Student senderStudent = (Student)(sender as Editor).BindingContext;

            string editedName = (sender as Editor).Text;

            if (!string.IsNullOrEmpty(editedName))
            {
                senderStudent.StudentName = editedName;
                ViewModels.ClassListViewModel.SaveClass(currentSelectedClass, currentSelectedClass.classFileName);
            }
        }
        private void StudentSurnameEditor_TextChanged(object sender, EventArgs e)
        {
            Student senderStudent = (Student)(sender as Editor).BindingContext;

            string editedSurname = (sender as Editor).Text;

            if (!string.IsNullOrEmpty(editedSurname))
            {
                senderStudent.StudentSurname = editedSurname;
                ViewModels.ClassListViewModel.SaveClass(currentSelectedClass, currentSelectedClass.classFileName);
            }
        }
        private void StudentDeleteButton_Clicked(object sender, EventArgs e)
        {
            Student senderStudent = (Student)(sender as Button).BindingContext;

            currentSelectedClass.classStudentList.Remove(senderStudent);
            ViewModels.ClassListViewModel.SaveClass(currentSelectedClass, currentSelectedClass.classFileName);
        }
    }
 }
