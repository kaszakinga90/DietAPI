using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Functionality
{
    public class PatientCardSurvey
    {
        public int PatientCardId { get; set; }
        public PatientCard PatientCard { get; set; }

        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
