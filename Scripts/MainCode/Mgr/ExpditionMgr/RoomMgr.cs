using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShuShan
{
    public class RoomMgr : Mgr,IMgrUseXml<RoomDef>
    {
        public static RoomMgr Instance { get; private set; }
        public override void Init()
        {
            Instance = this;
            InitXML();
        }
        public override void End()
        {

        }

        public void InitXML()
        {
            ModsMgr.Instance.LoadXmlDefs<RoomDefs>("Settings/Room", delegate (RoomDefs defs, string flag)
            {
                foreach (RoomDef tangMasteryDef in defs.Defs)
                {
                    tangMasteryDef.FromMod = flag;
                    if (m_Roomdefs.ContainsKey(tangMasteryDef.Name))
                    {
                        m_Roomdefs[tangMasteryDef.Name] = tangMasteryDef;
                    }
                    else
                    {
                        m_Roomdefs.Add(tangMasteryDef.Name, tangMasteryDef);
                    }
                    //Debug.Log(tangMasteryDef.Desc);
                }
            }, "*.xml", null);
            Debug.Log("已初始化Room：" + m_Roomdefs.Count);
        }

        public RoomDef FindDef(string name)
        {
            RoomDef def = new RoomDef();
            if (!m_Roomdefs.TryGetValue(name, out def))
            {
                LogMgr.Dbg("Not Find FeatureDef :" + name);
            }

            return def;
        }

        public Dictionary<string, RoomDef> m_Roomdefs = new Dictionary<string, RoomDef>();
    }
}
