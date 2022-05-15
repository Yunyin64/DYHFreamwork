using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuShan
{
    public class LocalizationMgr :Mgr
    {
        public static LocalizationMgr Instance { get; private set; }

        public override void Init()
        {
            LocalizationMgr.Instance = this;

        }
        public override void End()
        {

        }
    }
}
