using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1831050241
{
    public class Classify : IComparable<Classify>
    {
        public string classNum;
        public string className;
        public Classify() { }
        public Classify(string num, string name)
        {
            classNum = num;
            className = name;
        }
        public override string ToString()
        {
            return this.className.ToString();
        }
        public int CompareTo(Classify obj_)
        {
            return this.classNum.CompareTo(obj_.classNum);
        }
    }
}
