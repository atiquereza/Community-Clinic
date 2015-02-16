using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class TreatmentMedicine
    {
        public int ID { set; get; }
        public int TreatmentID { set; get; }
        public int MedicineId { set; get; }
        public string Medicine { set; get; }
        public string Dose { set; get; }
        public int QtyGiven { set; get; }
        public string Note { set; get; }
        public string B4AfterMeal { set; get; }
        public virtual Treatment Treatment { set; get; }

    }
}