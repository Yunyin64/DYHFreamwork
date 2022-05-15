using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuShan
{
    public class NpcSwapMgr : Mgr
    {
        public static NpcSwapMgr Instance { get; private set; }
        public override void Init()
        {
            Instance = this;
        }
        public override void End()
        {

        }
    }
}
