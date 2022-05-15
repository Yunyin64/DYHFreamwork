using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuShan
{
    /// <summary>
    /// log控制器，用于官方调试
    /// </summary>
    public  class  LogMgr:Mgr
    {
        public override void Init()
        {
            
        }
        public override void End()
        {

        }

        public static void Err(string ex,params object[] objs)
        {

        }

        public static void Dbg(string ex, params object[] objs)
        {

        }

        public static void Warn(string ex, params object[] objs)
        {

        }

    }
}
