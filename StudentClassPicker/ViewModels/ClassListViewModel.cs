using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentClassPicker.ViewModels
{
    public class ClassListViewModel
    {
        public ObservableCollection<Models.Class> ClassList { get; set; } = new ObservableCollection<Models.Class>();

        public ClassListViewModel() => LoadClassList();

        public void LoadClassList()
        {
            ClassList.Clear();
            string filePath = FileSystem.AppDataDirectory;

            IEnumerable<Models.Class> classList = Directory
                .EnumerateFiles(filePath, "*.class.json")
                .Select(file =>
                {
                    try
                    {
                        Models.Class tempClass = JsonSerializer.Deserialize<Models.Class>(File.ReadAllText(file)!);
                        return tempClass ?? new Models.Class();
                    }
                    catch (JsonException e)
                    {
                        return new Models.Class();
                    }

                })
                .OrderBy(_class => _class.ClassName);

            foreach (Models.Class _class in classList)
            {
                ClassList.Add(_class);
            }
        }

        public static void SaveClass(Models.Class saveClass, string filePath)
        {
            if(saveClass != null)
            {
                //dla globalnego zapisu:
                //zczytaj wszystkie klasy z pliku do jednej kolekcji
                //znajdź daną klasę i ją podmień
                //zapisz kolekcję do jednego pliku

                string saveData = JsonSerializer.Serialize(saveClass);
                File.WriteAllText(filePath, saveData);
            }
        }
    }
}
