using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using ShuShan;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using System.Diagnostics;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;

public class 大运河的测试 : MonoBehaviour
{
    public List<SB> sbs = new List<SB>();


    public int a1 = 0;
    public int a2= 0;
    public int a3 = 0;
    public int a4 = 0;
    public int a5 = 0;

    public SB s = new();
    public class SB
    {

        public float v1;
        public float v2;
        public float v3;
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //save测试1
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            List<Action> a = new();
           
            

            for(int i = 0; i < 5; i++)
            {
                Loom.RunAsync(delegate {
                    System.Random r = new();
                    s.v1 = r.Next(0, i*100);
                    Debug.Log(i+":"+s.v1);
                });
            }


            Debug.Log("F:"+s.v1);

        }

        if (InputMgr.Instance.GetKey(KeyCode.K))
        {
            UIMgr.Instance.Show("TestUI");
        }
        if (InputMgr.Instance.GetKey(KeyCode.E))
        {
            FeatureDef def = FeatureMgr.Instance.GetRandomFeatureDef(Em_FeatureType.天赋);
            Feature f = new Feature(def);
            Debug.Log("def1:"+ def.Name);
            Debug.Log("f1:" + f.Name);
            f.Name = "asfasfasf";
            Debug.Log("f2:" + f.Name);
            Debug.Log("def2:" + def.Name);
        }
    }

    public void OnFightBodyBeHit(object t, object[] objs)
    {
        Debug.Log("OnFightBodyBeHit");
    }
}
