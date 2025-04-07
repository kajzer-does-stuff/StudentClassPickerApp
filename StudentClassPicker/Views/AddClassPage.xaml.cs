using System.Diagnostics;
using System.Text.Json;
using StudentClassPicker.ViewModels;

namespace StudentClassPicker.Views;

public partial class AddClassPage : ContentPage
{
	public AddClassPage()
	{
		InitializeComponent();
	}
	
	private async void AddClassButton_Clicked(object sender, EventArgs e)
	{
		string newClassName = NewClassNameEditor.Text;

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
		}
	}
    private async void LoadClassButton_Clicked(object sender, EventArgs e)
    {
		var textFilePickerType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<String>>
		{
			{DevicePlatform.WinUI, new[] {".txt"} }
		});

		var selectedFile = await FilePicker.Default.PickAsync(new PickOptions() { PickerTitle = "Wybierz plik .txt", FileTypes = textFilePickerType});

		if (selectedFile != null && selectedFile.FileName.EndsWith("txt"))
		{
			using StreamReader reader = new(selectedFile.OpenReadAsync().Result);
			string fileData = await reader.ReadToEndAsync();

			List<Models.Class> readClassList = JsonSerializer.Deserialize<List<Models.Class>>(fileData);

			//Debug.WriteLine("\n" + fileData);
			//Debug.WriteLine("\n" + readClassList[0].ClassName);

			if(readClassList != null)
			{
                foreach (Models.Class readClass in readClassList)
                {
                    if (readClass != null)
					{
						ClassListViewModel.SaveClass(readClass);
                        await Shell.Current.GoToAsync("..");
                    }
				}
            }
        }
    }
    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}