using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace ShuShan
{
    public class TimeMgr : Mgr
    {
        public static TimeMgr Instance { get; private set; }

        public int Day
        {
            get
            {
                return Mathf.Clamp(_day, 1, 999);
            }
            set
            {
               _day = Mathf.Clamp(value, 1, 999);
            }
        }

        public override void Init()
        {
            TimeMgr.Instance = this;

        }
        public override void End()
        {

        }

        public void NextDay()
        {
           
        }

        private int _day;
    }
}
