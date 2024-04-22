using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOP_Seminars.Models
{
    public class TreeData
    {
        public string PointNumber { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string TreeType { get; set; }
        public string Diameter { get; set; }
    }
}