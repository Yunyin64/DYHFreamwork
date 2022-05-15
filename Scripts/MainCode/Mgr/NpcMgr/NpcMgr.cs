using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace ShuShan
{
    public class NpcMgr : Mgr
    {
        public static NpcMgr Instance { get; private set; }
        public override void Init()
        {
            Instance = this;
        }
        public override void End()
        {

        }
        public Npc CreatRandomNpc()
        {
            Npc npc = new Npc();

            
            Feature f1 =FeatureMgr.Instance.GetRandomFeature(Em_FeatureType.性格);
            Feature f2 = FeatureMgr.Instance.GetRandomFeature(Em_FeatureType.天赋);
            Feature f3 = FeatureMgr.Instance.GetRandomFeature(Em_FeatureType.经历);

            npc.InitFeature(f1,f2,f3);

            npc.Name = "TestNpc" + Random.Range(1, 100);

            return npc;
        }

        public Npc[] Player = new Npc[5];


    }
}
