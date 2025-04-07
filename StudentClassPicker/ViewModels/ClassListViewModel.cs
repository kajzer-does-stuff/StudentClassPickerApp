using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public async void LoadClassList()
        {
            ClassList.Clear();
            List<Models.Class> classList = [];
            
            try
            {
                string saveData = File.ReadAllText(Models.SavePath.filePath);
                classList = JsonSerializer.Deserialize<List<Models.Class>>(saveData);                                
            }
            finally
            {
                if(classList != null)
                {
                    foreach (Models.Class _class in classList)
                    {
                        ClassList.Add(_class);
                    }
                }
            }
        }

        public static void SaveClass(Models.Class saveClass)
        {
            if(saveClass != null)
            {
                List<Models.Class> allClassList = [];                

                try
                {
                    string allClassSaveData = File.ReadAllText(Models.SavePath.filePath);
                    allClassList = JsonSerializer.Deserialize<List<Models.Class>>(allClassSaveData);
                }
                finally
                {
                    if(allClassList != null)
                    {
                        if (allClassList.Any(x => x.classID == saveClass.classID))
                        {
                            int index = allClassList.FindIndex(y => y.classID == saveClass.classID);
                            if(index != -1)
                            {
                                allClassList[index] = saveClass;
                            }
                        }
                        else
                        {
                            allClassList.Add(saveClass);
                        }
                    }
                    string saveData = JsonSerializer.Serialize(allClassList);
                    File.WriteAllText(Models.SavePath.filePath, saveData);
                }                
            }
        }
        public static List<Models.Class> GetClassList()
        {
            List<Models.Class> classList = [];

            try
            {
                string saveData = File.ReadAllText(Models.SavePath.filePath);
                classList = JsonSerializer.Deserialize<List<Models.Class>>(saveData);
            }
            catch (JsonException e) { }

            if (classList != null) return classList;
            else return [];
        }
        public static int GetLastClassId()
        {
            List<Models.Class> classList = GetClassList();

            if (classList != null)
            {
                var highestIdClass = classList.MaxBy(x => x.classID);
                if (highestIdClass != null)
                {
                    return highestIdClass.classID;
                }
            }
            return -1;
        }
    }
}
