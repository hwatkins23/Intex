using System;
using System.ComponentModel.DataAnnotations;

namespace Intex.Models
{
    public class crash
    {
        [Key]
        [Required]
        public int CRASH_ID { get; set; }
        [Required(ErrorMessage = "Please fill in the Accident Date")]
        public string CRASH_DATE { get; set; }
        [Required(ErrorMessage = "Please fill in the Time")]
        public string TIME {get;set;}
        [Required(ErrorMessage = "Please select AM or PM")]
        public string TIME_OF_DAY { get; set; }
        [Required(ErrorMessage = "Please fill in the City")]
        public string CITY { get; set; }
        [Required(ErrorMessage = "Please fill in the County")]
        public string COUNTY_NAME { get; set; }
        [Required]
        public int CRASH_SEVERITY_ID { get; set; }

        public string ROUTE { get; set; }
        public string MILEPOINT { get; set; }
        public string LAT_UTM_Y { get; set; }
        public string LONG_UTM_X { get; set; }
        public string MAIN_ROAD_NAME { get; set; }
        public string WORK_ZONE_RELATED { get; set; }
        public string PEDESTRIAN_INVOLVED { get; set; }
        public string BICYCLIST_INVOLVED { get; set; }
        public string MOTORCYCLE_INVOLVED { get; set; }
        public string IMPROPER_RESTRAINT { get; set; }
        public string UNRESTRAINED { get; set; }
        public string DUI { get; set; }
        public string INTERSECTION_RELATED { get; set; }
        public string WILD_ANIMAL_RELATED { get; set; }
        public string DOMESTIC_ANIMAL_RELATED { get; set; }
        public string OVERTURN_ROLLOVER { get; set; }
        public string COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }
        public string TEENAGE_DRIVER_INVOLVED { get; set; }
        public string OLDER_DRIVER_INVOLVED { get; set; }
        public string NIGHT_DARK_CONDITION { get; set; }
        public string SINGLE_VEHICLE { get; set; }
        public string DISTRACTED_DRIVING { get; set; }
        public string DROWSY_DRIVING { get; set; }
        public string ROADWAY_DEPARTURE { get; set; }
    }

}
