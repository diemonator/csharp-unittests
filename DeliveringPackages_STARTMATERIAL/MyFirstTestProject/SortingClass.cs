using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliverablesApp;

namespace MyFirstTestProject
{
    class SortingClass : IComparer<Deliverable>
    {
        public int Compare(Deliverable x, Deliverable y)
        {
            if (x.Weight > y.Weight)
                return -1;
            else if (x.Weight < y.Weight)
                return 1;
            return 0;
        }
    }
}
