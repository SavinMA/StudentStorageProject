using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class FilterStudentViewModel
    {
        public string Gender { get; set; }
        public string FIO { get; set; }
        public string UID { get; set; }
        public string GroupName { get; set; }

        public void Apply(string gender, string fio, string uid, string groupName)
        {
            Gender = gender;
            FIO = fio;
            UID = uid;
            GroupName = groupName;
        }
    }
}
