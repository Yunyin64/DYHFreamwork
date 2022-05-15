using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShuShan
{
    public class UIRoot : MonoBehaviour
    {
        private void Start()
        {
            Mgr = UIMgr.Instance;
            Mgr.Bind(this);


            for (int i = 0; i < 10; i++)
            {
                GameObject obj = new GameObject();
                obj.name = "UI_" + i;
                obj.transform.parent = transform;
                //Instantiate(obj, this.transform);
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform t = transform.GetChild(i);
                m_Windows.Add(t.name, t);
            }
        }
        public void Init()
        {

        }

        public void ShowWindow(string name)
        {
            Transform t;
            if (!m_Windows.TryGetValue(name,out t))
            {
                Debug.Log("Not Find UI:"+name);
            }
            else
            {
                t.gameObject.SetActive(!t.gameObject.activeSelf);
            }
        }

        public UIMgr Mgr;

        
        public Dictionary<string, Transform> m_Windows = new Dictionary<string, Transform>();
    }
}
