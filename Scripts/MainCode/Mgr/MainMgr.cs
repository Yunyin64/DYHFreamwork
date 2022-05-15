using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using ShuShan;

namespace ShuShan
{
    public class MainMgr : Mgr
    {

        public static MainMgr Instance { get; private set; }
        //public List<Mgr> m_listmgr = new List<Mgr>();

        
        public MainMgr()
        {
            Name = "Main";
            if (MainMgr.Instance != null)
            {
                MainMgr.Instance = null;
            }
            MainMgr.Instance = this;
            this.Init();

        }
        public override void Init()
        {
            this.sub_mgrs.Clear();

            ///系统mgr
            AddMgr(new InputMgr());
            AddMgr(new ModsMgr());
            AddMgr(new SceneMgr());

            ///世界mgr
            AddMgr(new TimeMgr());


            ///实体mgr
            AddMgr(new ItemMgr());
            AddMgr(new ExpditonMgr());
            AddMgr(new RoomMgr());
            AddMgr(new BagMgr());
            AddMgr(new LocationMgr());
            AddMgr(new WorldMgr());
            AddMgr(new EquiptMgr());
            AddMgr(new FeatureMgr());
            AddMgr(new NpcMgr());
            AddMgr(new PropertyMgr());
            AddMgr(new StoryMgr());
            AddMgr(new NpcSwapMgr());


            ///高位mgr
            AddMgr(new UIMgr());
            AddMgr(new EventMgr());
            //AddMgr(new LuaMgr());
        }

        public IEnumerator Start()
        {
            foreach (Mgr mgr in sub_mgrs.Values)
            {
                mgr.Init();
            }
            yield break;
        }

        

        public T GetManager<T>()
        {
            Mgr managerModule;
            if (this.sub_mgrs.TryGetValue(typeof(T).Name, out managerModule))
            {
                return (T)((object)managerModule);
            }
            return default(T);
        }
        public override void Step(float dt)
        {   

            foreach(Mgr mgr in sub_mgrs.Values)
            {
                mgr.Step(dt);
            }
        }
        public override void Update()
        {
            foreach (Mgr mgr in sub_mgrs.Values)
            {
                mgr.Update();
            }
        }
        public override void Render(float dt)
        {
            foreach (Mgr mgr in sub_mgrs.Values)
            {
                mgr.Render(dt);
            }
        }
        public override void End()
        {
            foreach (Mgr mgr in sub_mgrs.Values)
            {
                mgr.End();
            }
        }

        public void AddMgr(Mgr mgr)
        {
            if (mgr.Name == "UnknowMgr")
            {
                Debug.LogWarning("Find a Mgr without Name:" + mgr.GetType().ToString());
                mgr.Name = mgr.GetType().Name;
            }
            sub_mgrs.Add(mgr.Name, mgr);
        }

        public Dictionary<string, Mgr> sub_mgrs = new Dictionary<string, Mgr>();
    }
}
