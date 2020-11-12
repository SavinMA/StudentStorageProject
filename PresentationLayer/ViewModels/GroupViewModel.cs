using PresentationLayer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class GroupViewModel
    {
        public IEnumerable<GroupModel> Groups { get; set; }
        public FilterGroup Filter { get; set; }
    }
}
