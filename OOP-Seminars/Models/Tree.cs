using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOP_Seminars.Models
{
    public class Tree
    {
        private double diameter;
        private string species;

        public double Diameter
        {
            get
            {
                return diameter;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new Exception("Diameter can't be <= 0");
                }
                else
                {
                    diameter = value;
                }
            }
        }

        public string Species
        {
            get
            {
                return species;
            }
            private set
            {
                // TODO: создать массив (enum) видов и проверять на вхождение в него
                if (value.Length < 1)
                {
                    throw new Exception("Species len cant't be lower than 1");
                }
                else
                {
                    species = value;
                }
            }
        }

        public (double, double) Coordinates { get; private set; }

        public Tree((double, double) coordinates, double diameter, string species)
        {
            Coordinates = coordinates;
            Diameter = diameter;
            Species = species;
        }
    }
}