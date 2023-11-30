using Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FiltersExtensions.PatientMessages
{
    public class PatientMessagesParams:PagingParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string DieticianNames { get; set; }
    }
}
