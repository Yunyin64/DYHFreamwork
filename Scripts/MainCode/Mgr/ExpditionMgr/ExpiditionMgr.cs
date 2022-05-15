using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuShan
{
    public class ExpditonMgr : Mgr
    {
        public static ExpditonMgr Instance { get; private set; }
        public override void Init()
        {
            Instance = this;
        }
        public override void End()
        {

        }


    }
}
