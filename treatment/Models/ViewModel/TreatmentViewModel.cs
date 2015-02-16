using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models.ViewModel
{
    public class TreatmentViewModel
    {
        public Treatment ATreatment { set; get; }
        public List<TreatmentMedicine> ATreatmentMedicinesList { set; get; }
    }
}