using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOP_Seminars.Models
{
    public class ForestArea
    {
        private List<(double, double)> coordinates;
        public List<(double, double)> Coordinates
        {
            get
            {
                return coordinates;
            }
            private set
            {
                // TODO: проверка правильного расположения точек
                if (value.Count < 3)
                {
                    throw new Exception("Coordinates count can't be lower than 3");
                }
                else
                {
                    coordinates = value;
                }
            }
        }

        public List<Tree> Trees { get; private set; }

        public ForestArea(List<(double, double)> coordinates)
        {
            Coordinates = coordinates;
            Trees = new List<Tree>();
        }

        // TODO: проверка вхождения дерева в лесной массив
        public void AddTree(Tree tree)
        {
            Trees.Add(tree);
        }

        public void RemoveTree(Tree tree)
        {
            Trees.Remove(tree);
        }
    }
}