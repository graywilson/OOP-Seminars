using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOP_Seminars.Models
{
    public abstract class DecisionAlgorithm
    {
        public abstract bool ShouldCutDown(Tree tree);
        public abstract bool IsMainSpeciesTree(Tree tree);
        public abstract double GetTreesDistance(Tree t1, Tree t2);
        public abstract Tree GetThinnesTree(ForestArea area);
        public abstract Tree GetFattestTree(ForestArea area);

        public IEnumerable<Tree> DisplayResults(List<Tree> trees)
        {
            foreach (var tree in trees)
            {
                if (ShouldCutDown(tree))
                {
                    yield return tree;
                }
            }
        }
    }
}