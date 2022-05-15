using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShuShan
{
    public class FeatureMgr : Mgr,IMgrUseXml<FeatureDef>
    {
        public static FeatureMgr Instance { get; private set; }
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
            ModsMgr.Instance.LoadXmlDefs<FeatureDefs>("Settings/Feature", delegate (FeatureDefs defs, string flag)
            {
                m_t_features.Add(Em_FeatureType.天赋, new Dictionary<string, FeatureDef>());
                m_t_features.Add(Em_FeatureType.性格, new Dictionary<string, FeatureDef>());
                m_t_features.Add(Em_FeatureType.经历, new Dictionary<string, FeatureDef>());

                foreach (FeatureDef tangMasteryDef in defs.Defs)
                {
                    tangMasteryDef.FromMod = flag;
                    if (m_features.ContainsKey(tangMasteryDef.Name))
                    {
                        m_features[tangMasteryDef.Name] = tangMasteryDef;
                        m_t_features[tangMasteryDef.Type][tangMasteryDef.Name] = tangMasteryDef;
                    }
                    else
                    {
                        m_features.Add(tangMasteryDef.Name, tangMasteryDef);
                        m_t_features[tangMasteryDef.Type].Add(tangMasteryDef.Name, tangMasteryDef);
                    }
                    //Debug.Log(tangMasteryDef.Desc);
                }
            }, "*.xml", null);
            Debug.Log("已初始化feature：" + m_features.Count);
        }
        public FeatureDef FindDef(string name)
        {
            FeatureDef def = new FeatureDef();
            if(!m_features.TryGetValue(name,out def))
            {
                LogMgr.Dbg("Not Find FeatureDef :"+ name);
            }

            return def;
        }
        public Feature GetRandomFeature(Em_FeatureType type= Em_FeatureType.None)
        {
            FeatureDef def = null;
            def = GetRandomFeatureDef(type);
            return new Feature(def);
        }
        public FeatureDef GetRandomFeatureDef(Em_FeatureType type = Em_FeatureType.None)
        {
            FeatureDef def = null;
            switch (type)
            {
                case Em_FeatureType.None:
                    def = m_features.GetRandomMapItem<string, FeatureDef>();
                    break;
                case Em_FeatureType.性格:
                    def = m_t_features[type].GetRandomMapItem<string, FeatureDef>();
                    break;
                case Em_FeatureType.天赋:
                    def = m_t_features[type].GetRandomMapItem<string, FeatureDef>();
                    break;
                case Em_FeatureType.经历:
                    def = m_t_features[type].GetRandomMapItem<string, FeatureDef>();
                    break;
                default:
                    break;
            }
            return def;
        }

        public Dictionary<Em_FeatureType, Dictionary<string, FeatureDef>> m_t_features = new Dictionary<Em_FeatureType, Dictionary<string, FeatureDef>>();

        public Dictionary<string, FeatureDef> m_features = new Dictionary<string, FeatureDef>();
    }
}
