using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningEntityFramework.ViewModels
{
    public class MotorcycleViewModel
    {
        public List<Motorcycle> AllMotorcycles { get; set; }
        public Motorcycle NewMotorcycle { get; set; }
        public Motorcycle EditMotorcycle { get; set; }

    }
}