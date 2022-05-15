using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShuShan
{
    
    public class UIMgr : Mgr
    {
        public static UIMgr Instance { get; private set; }
        public override void Init()
        {
            Instance = this;

        }
        public override void End()
        {
            
        }

        public void Bind(UIRoot obj)
        {
            UIRoot = obj;
        }

        public void Show(string name,Transform pos = null)
        {
            UIRoot.ShowWindow(name);
        }

        public UIRoot UIRoot;
    }
}
