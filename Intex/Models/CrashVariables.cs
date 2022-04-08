using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Models
{
    //--------------------------------------------------------Crash Variables for Predictor--------------------------------------------------------
    public class CrashVariables
    {
        public float PedestiranInvolved { get; set; }
        public float BicyclistInvolved { get; set; }
        public float MotorcycleInvolved { get; set; }
        public float ImproperRestraint { get; set; }
        public float Unrestrained { get; set; }
        public float DUI { get; set; }
        public float IntersectionRelated { get; set; }
        public float OverturnRollover { get; set; }
        public float DistractedDriving { get; set; }
        public float DrowsyDriving { get; set; }
        public float MilepointOther { get; set; }
        public float CitySaltLakeCity { get; set; }
        public float CityWestJordan { get; set; }

        //Runs the variables from the controller for the prediction
        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                PedestiranInvolved, BicyclistInvolved, MotorcycleInvolved,
                ImproperRestraint, Unrestrained, DUI, IntersectionRelated,
                OverturnRollover, DistractedDriving, DrowsyDriving,
                MilepointOther, CitySaltLakeCity, CityWestJordan
            };
            int[] dimensions = new int[] { 1, 13 };

            return new DenseTensor<float>(data, dimensions);
        }
    }
}
