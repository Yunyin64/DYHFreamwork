using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuShan
{
    public class StoryMgr : Mgr
    {
        public static StoryMgr Instance { get; private set; }
        public override void Init()
        {
            Instance = this;
        }
        public override void End()
        {

        }


    }
}
