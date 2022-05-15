using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShuShan
{
    public class ItemMgr : Mgr, IMgrUseXml<ItemDef>
    {
        public static ItemMgr Instance { get; private set; }
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
            ModsMgr.Instance.LoadXmlDefs<ItemDefs>("Settings/Item", delegate (ItemDefs defs,string flag)
             {
                 foreach (var def in defs.Defs)
                 {
                     def.FromMod = flag;
                     if (m_itemdef.ContainsKey(def.Name))
                     {
                         m_itemdef[def.Name] = def;
                     }
                     else
                     {
                         m_itemdef.Add(def.Name, def);
                     }
                 }

             });
            Debug.Log("已初始化item：" + m_itemdef.Count);

        }

        public ItemDef FindDef(string name)
        {
            ItemDef def = new ItemDef();
            if (!m_itemdef.TryGetValue(name, out def))
            {
                LogMgr.Dbg("Not Find ItemDef :" + name);
            }

            return def;
        }

        public Dictionary<string, ItemDef> m_itemdef = new Dictionary<string, ItemDef>();
    }
}
