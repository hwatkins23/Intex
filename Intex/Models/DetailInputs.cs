using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models
{
    //----------------------------------------------Detail Inputs (inputs for predictor from detail pages)-----------------------------------
    public class DetailInputs
    {

        public string PEDESTRIAN_INVOLVED { get; set; }
        public string BICYCLIST_INVOLVED { get; set; }
        public string MOTORCYCLE_INVOLVED { get; set; }
        public string IMPROPER_RESTRAINT { get; set; }
        public string UNRESTRAINED { get; set; }
        public string DUI { get; set; }
        public string INTERSECTION_RELATED { get; set; }
        public string OVERTURN_ROLLOVER { get; set; }
        public string DISTRACTED_DRIVING { get; set; }
        public string DROWSY_DRIVING { get; set; }
        public string MILEPOINT { get; set; }
        public string CITY { get; set; }
    }
}
