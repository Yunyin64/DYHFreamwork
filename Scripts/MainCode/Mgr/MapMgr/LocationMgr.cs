using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShuShan
{
    public class LocationMgr : Mgr,IMgrUseXml<MapLocationDef>
    {
        public static LocationMgr Instance { get; private set; }
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
            ModsMgr.Instance.LoadXmlDefs<MapLocationDefs>("Settings/Location", delegate (MapLocationDefs defs, string flag)
            {
                foreach (MapLocationDef tangMasteryDef in defs.Defs)
                {
                    tangMasteryDef.FromMod = flag;
                    if (m_locations.ContainsKey(tangMasteryDef.Name))
                    {
                        m_locations[tangMasteryDef.Name] = tangMasteryDef;
                    }
                    else
                    {
                        m_locations.Add(tangMasteryDef.Name, tangMasteryDef);
                    }
                    //Debug.Log(tangMasteryDef.Desc);
                }
            }, "*.xml", null);
            Debug.Log("已初始化location：" + m_locations.Count);
        }

        public MapLocationDef FindDef(string name)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, MapLocationDef> m_locations = new();
    }
}
