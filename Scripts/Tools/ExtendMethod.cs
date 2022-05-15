using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuShan
{
    

    public class NumList<T> : System.Collections.Generic.List<T>
    {
        public static NumList<T> operator +(NumList<T> b, NumList<T> c)
        {
            if (b.Count == c.Count)
            {
                NumList<T> output = new NumList<T>();
                for (int i = 0; i < b.Count; i++)
                {
                    double a1 = Double.Parse(b[i].ToString());
                    double a2 = Double.Parse(c[i].ToString());
                    output.Add((T)Convert.ChangeType(a1 + a2, typeof(T)));
                }
                return output;
            }
            else
            {
                return null;
            }
        }
    }
}
