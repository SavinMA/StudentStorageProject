using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class FilterGroup
    {
        public string GroupName { get; set; }

        public void Apply(string groupName)
        {
            GroupName = groupName;
        }
    }
}
