namespace StudentClassPicker.Views;
using StudentClassPicker.ViewModels;

public partial class AddStudentPage : ContentPage
{
    private readonly Models.Class selectedClass;
	public AddStudentPage()
	{
		InitializeComponent();
	}
    public AddStudentPage(Models.Class selectedClass)
    {
        this.selectedClass = selectedClass;
        InitializeComponent();
    }

	private async void AddStudentButton_Clicked(object sender, EventArgs e)
	{
        string newStudentName = NewStudentNameEditor.Text;
        string newStudentSurname = NewStudentSurnameEditor.Text;

        if (!string.IsNullOrEmpty(newStudentName) && !string.IsNullOrEmpty(newStudentSurname)){

            if(selectedClass != null)
            {
                int newStudentId;

                if(selectedClass.classStudentList.Count < 1)
                {
                    newStudentId = 1;
                }
                else
                {
                    newStudentId = selectedClass.classStudentList.MaxBy(x => x.studentID).studentID + 1;
                }

                Models.Student newStudent = new(newStudentId, newStudentName, newStudentSurname);
                selectedClass.classStudentList.Add(newStudent);

                ClassListViewModel.SaveClass(selectedClass);

                await Shell.Current.GoToAsync("..");
            }
        }
        else
        {
            await DisplayAlert("B³¹d", "Podaj imiê i nazwisko ucznia", "OK");
        }

        /*string newClassName = NewClassNameEditor.Text;

        if (!string.IsNullOrEmpty(newClassName))
        {
            Models.Class newClass = new()
            {
                ClassName = newClassName,
                classID = ClassListViewModel.GetLastClassId() + 1,
                classStudentList = []
            };

            ClassListViewModel.SaveClass(newClass);

            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await DisplayAlert("B³¹d", "Podaj nazwê klasy", "OK");
        }*/
    }
    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}