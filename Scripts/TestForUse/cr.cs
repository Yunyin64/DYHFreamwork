using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PF
{
    public class cr : MonoBehaviour
    {
        public GameObject obj;
        StateMgr sm = new StateMgr();
        // Start is called before the first frame update
        void Start()
        {
             sm.Init();

            if (StateMgr.Instance == null)
            {
                Debug.LogError(1010);
            }
        }

        // Update is called once per frame
        void Update()
        {
           
        }
    }

    public abstract class Mgr
    {
        // Start is called before the first frame update
        public abstract void Init();
    }
    public class StateMgr : Mgr
    {

        public static StateMgr Instance { get; private set; }

        public  GameObject player = null;
        public override void Init()
        {
            StateMgr.Instance = this;
        }
    }
 }