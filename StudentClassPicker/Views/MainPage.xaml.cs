using StudentClassPicker.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;

namespace StudentClassPicker.Views
{
    public partial class MainPage : ContentPage
    {
        private Class currentSelectedClass;
        private Student pickedStudent;

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
                ViewModels.ClassListViewModel.SaveClass(senderClass);
            }

            if(currentSelectedClass == senderClass)
            {
                SelectedClassNameLabel.Text = editedName;
            }
        }
        private void ClassDeleteButton_Clicked(object sender, EventArgs e)
        {
            Class senderClass = (Class)(sender as Button).BindingContext;
 
            ViewModels.ClassListViewModel.DeleteClass(senderClass);
            currentSelectedClass = null;

            BindingContext = new ViewModels.ClassListViewModel();
            SelectedClassNameLabel.Text = "Wybierz klasę";
            CurrentStudentList.BindingContext = null;           
        }

        private async void AddClassButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddClassPage());
        }

        private async void AddStudentButton_Clicked(object sender, EventArgs e)
        {
            if(currentSelectedClass != null)
            {
                await Navigation.PushAsync(new AddStudentPage(currentSelectedClass));
            }
            else
            {
                await DisplayAlert("Błąd", "Wybierz klasę, do której chcesz dodać ucznia", "OK");
            }            
        }

        private void StudentNameEditor_TextChanged(object sender, EventArgs e)
        {
            Student senderStudent = (Student)(sender as Editor).BindingContext;

            string editedName = (sender as Editor).Text;

            if (!string.IsNullOrEmpty(editedName))
            {
                senderStudent.StudentName = editedName;
                ViewModels.ClassListViewModel.SaveClass(currentSelectedClass);
            }
        }

        private void StudentSurnameEditor_TextChanged(object sender, EventArgs e)
        {
            Student senderStudent = (Student)(sender as Editor).BindingContext;

            string editedSurname = (sender as Editor).Text;

            if (!string.IsNullOrEmpty(editedSurname))
            {
                senderStudent.StudentSurname = editedSurname;
                ViewModels.ClassListViewModel.SaveClass(currentSelectedClass);
            }
        }

        private void StudentDeleteButton_Clicked(object sender, EventArgs e)
        {
            Student senderStudent = (Student)(sender as Button).BindingContext;

            currentSelectedClass.classStudentList.Remove(senderStudent);
            ViewModels.ClassListViewModel.SaveClass(currentSelectedClass);
        }

        private async void RandomPickButton_Clicked(object sender, EventArgs e)
        {
            if(currentSelectedClass != null)
            {
                var randGenerator = new Random();
                int randBound = currentSelectedClass.classStudentList.Count;

                if(randBound <= 0)
                {
                    await DisplayAlert("Błąd", "Zbyt mała liczba uczniów, nie można losować", "OK");
                }
                else
                {
                    int randNumber = randGenerator.Next(0, randBound);
                    try
                    {
                        pickedStudent = currentSelectedClass.classStudentList[randNumber];
                    }
                    catch(IndexOutOfRangeException ex)
                    {
                        await DisplayAlert("Błąd", "Wystąpił błąd podczas losowania", "OK");
                    }
                    finally
                    {
                        if(pickedStudent != null)
                        {
                            RandomPickNumberLabel.Text = pickedStudent.studentID.ToString();
                            RandomPickNumberLabel.BackgroundColor = Colors.DarkMagenta;
                            RandomPickNameLabel.Text = $"{pickedStudent.StudentName} {pickedStudent.StudentSurname}";                            
                            RandomPickTitleLabel.Text = "Wylosowano";
                        }
                    }
                }                
            }
            else
            {
                await DisplayAlert("Błąd", "Wybierz klasę, z której ucznia chcesz losować", "OK");
            }
        }
    }
 }
