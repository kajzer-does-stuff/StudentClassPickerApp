using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentClassPicker.Models
{
    public static class SavePath
    {
        public static string filePath = $"{FileSystem.AppDataDirectory}/classSaveData.txt";
    }
}
