using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intex.Models;
using Microsoft.ML.OnnxRuntime.Tensors;

//--------------------------------------------PREDICTOR-------------------------------------------------------------
namespace Intex.Controllers
{
    public class PredictorController : Controller
    {
        private InferenceSession _session;

        public PredictorController(InferenceSession session)
        {
            _session = session;
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

            var prediction = new Prediction { PredictedValue = score.First()};
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

        
    }
}
