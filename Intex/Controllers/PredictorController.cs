using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex.Models;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.AspNetCore.Routing;


//--------------------------------------------PREDICTOR-------------------------------------------------------------

namespace Intex.Controllers
{
    public class PredictorController : Controller
    {
        private InferenceSession _session;
        private IcrashRepository repo;
        public PredictorController(InferenceSession session, IcrashRepository temp)
        {
            _session = session;
            repo = temp;
        }

        [HttpGet]
        public IActionResult CrashPredictor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrashPredictor(CrashVariables crashVariables)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", crashVariables.AsTensor())
            });

            Tensor<float> score = result.First().AsTensor<float>();

            var prediction = new Prediction { PredictedValue = score.First() };
            result.Dispose();

            ViewBag.Prediction = prediction.PredictedValue;

            if (ViewBag.Prediction > 5)
            {
                ViewBag.Prediction = 5;
            }
            else if (ViewBag.Prediction < 1)
            {
                ViewBag.Prediction = 1;
            }
            else
            {
                ViewBag.Prediction = Math.Round(prediction.PredictedValue);
            }

            return View();
        }

        [HttpGet]
        public IActionResult DetailPredictor(int crashID)
        {
            var crash = repo.crashes.Single(x => x.CRASH_ID == crashID);
            var crashVariables = new CrashVariables();
            var inputs = new DetailInputs
            {
            PEDESTRIAN_INVOLVED = crash.PEDESTRIAN_INVOLVED,
            BICYCLIST_INVOLVED = crash.BICYCLIST_INVOLVED,
            MOTORCYCLE_INVOLVED = crash.MOTORCYCLE_INVOLVED,
            IMPROPER_RESTRAINT = crash.IMPROPER_RESTRAINT,
            UNRESTRAINED = crash.UNRESTRAINED,
            DUI = crash.DUI,
            INTERSECTION_RELATED = crash.INTERSECTION_RELATED,
            OVERTURN_ROLLOVER = crash.OVERTURN_ROLLOVER,
            DISTRACTED_DRIVING = crash.DISTRACTED_DRIVING,
            DROWSY_DRIVING = crash.DROWSY_DRIVING,
            MILEPOINT = crash.MILEPOINT,
            CITY = crash.CITY
            };

            

            float pedestrianFloat = 0.0F;
            float bikeFloat = 0.0F;
            float motorcycleFloat = 0.0F;
            float improperrestrainFloat = 0.0F;
            float unrestrainedFloat = 0.0F;
            float duiFloat = 0.0F;
            float intersectionFloat = 0.0F;
            float overturnFloat = 0.0F;
            float distractedFloat = 0.0F;
            float drowsyFloat = 0.0F;
            float mileFloat = 0.0F;
            float citySLCFloat = 0.0F;
            float cityWJFloat = 0.0F;

            if(inputs.PEDESTRIAN_INVOLVED == "TRUE")
            {
                pedestrianFloat = 1.0F;
            }
            if(inputs.BICYCLIST_INVOLVED == "TRUE")
            {
                bikeFloat = 1.0F;
            }
            if(inputs.MOTORCYCLE_INVOLVED == "TRUE")
            {
                motorcycleFloat = 1.0F;
            }
            if(inputs.IMPROPER_RESTRAINT == "TRUE")
            {
                improperrestrainFloat = 1.0F;
            }
            if(inputs.UNRESTRAINED == "TRUE")
            {
                unrestrainedFloat = 1.0F;
            }
            if(inputs.DUI == "TRUE")
            {
                duiFloat = 1.0F;
            }
            if(inputs.INTERSECTION_RELATED == "TRUE")
            {
                intersectionFloat = 1.0F;
            }
            if (inputs.OVERTURN_ROLLOVER == "TRUE")
            {
                overturnFloat = 1.0F;
            }
            if (inputs.DISTRACTED_DRIVING == "TRUE")
            {
                distractedFloat = 1.0F;
            }
            if (inputs.DROWSY_DRIVING == "TRUE")
            {
                drowsyFloat = 1.0F;
            }
            if (inputs.CITY == "SALT LAKE CITY")
            {
                citySLCFloat = 1.0F;
            }
            if (inputs.CITY == "WEST JORDAN")
            {
                cityWJFloat = 1.0F;
            }

            crashVariables.PedestiranInvolved = pedestrianFloat;
            crashVariables.BicyclistInvolved = bikeFloat;
            crashVariables.MotorcycleInvolved = motorcycleFloat;
            crashVariables.ImproperRestraint = improperrestrainFloat;
            crashVariables.Unrestrained = unrestrainedFloat;
            crashVariables.DUI = duiFloat;
            crashVariables.IntersectionRelated = intersectionFloat;
            crashVariables.OverturnRollover = overturnFloat;
            crashVariables.DistractedDriving = distractedFloat;
            crashVariables.DrowsyDriving = drowsyFloat;
            crashVariables.MilepointOther = mileFloat;
            crashVariables.CitySaltLakeCity = citySLCFloat;
            crashVariables.CityWestJordan = cityWJFloat;

            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", crashVariables.AsTensor())
            });

            Tensor<float> score = result.First().AsTensor<float>();

            var prediction = new Prediction { PredictedValue = score.First() };
            result.Dispose();

            ViewBag.Prediction = prediction.PredictedValue;
            
            if (ViewBag.Prediction > 5)
            {
                ViewBag.Prediction = 5;
            }
            else if (ViewBag.Prediction < 1)
            {
                ViewBag.Prediction = 1;
            }
            else
            {
                ViewBag.Prediction = Math.Round(prediction.PredictedValue);
            }

            return RedirectToAction("Details","Home", new { crashID, predictedval = ViewBag.Prediction });
        }



    }
}
