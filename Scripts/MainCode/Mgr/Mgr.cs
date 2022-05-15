using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using ShuShan;
using Newtonsoft.Json;

namespace ShuShan
{

    public abstract class Mgr
    {

        /// <summary>
        /// 无耦合初始化
        /// </summary>
        public abstract void Init();
        public abstract void End();

        [JsonIgnore]
        public bool has_submgr = false;

        /// <summary>
        /// 有耦合初始化
        /// </summary>
       public virtual void Begin()
        {

        }

        // Update is called once per frame
        public virtual void Step(float dt)
        {

        }
        public virtual void Update()
        {

        }
        public virtual void Render(float dt)
        {

        }
        public virtual IEnumerator  Save()
        {
            yield break;
        }
        public virtual IEnumerator Load()
        {
            yield break;
        }


        [JsonIgnore]
        public string Name = "UnknowMgr";

        [JsonIgnore]
        public virtual string Desc
        {
            get
            {
                return "天道雏形".Translate();
            }
        }
    }
}
