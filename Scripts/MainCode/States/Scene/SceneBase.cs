using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShuShan
{
    public abstract class SceneBase : State
    {
        public Scene _scene;

        public string scenename;

        public bool isInited = false;

        protected SceneMgr stateMgr = null;

        public  g_emScene StateName;


        public SceneBase()
        {
            stateMgr = SceneMgr.Instance;
        }

        
    }
}
