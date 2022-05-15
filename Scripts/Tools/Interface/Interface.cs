using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuShan
{
    public interface IMgrUseXml<T> where T: DefBase
    {
        public  void InitXML();

        public T FindDef(string name);


        //public abstract T Find<T>(string name);
    }

    public interface IMgrCheckYear
    {
        public void PassYear();

    }

    public interface IMgrCheckTurn
    {
        public void PassTurn();

    }

    public interface IMgrCheckOrder
    {
        public void PassOrder();

    }
}
