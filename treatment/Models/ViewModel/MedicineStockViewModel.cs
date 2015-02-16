using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models.ViewModel
{
    public class MedicineStockViewModel
    {
        public int ID { set; get; }
        public int MedicineId { set; get; }
        public string MedicineName { set; get; }
        public string WeightContains { set; get; }
        public string MG_ML { set; get; }
        public int Quantity { set; get; }
    }
}