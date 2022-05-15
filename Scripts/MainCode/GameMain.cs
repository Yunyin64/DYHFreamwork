using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ShuShan;
using Newtonsoft.Json;

namespace ShuShan
{
    public class GameMain : MonoBehaviour
    {
        public static GameMain Instance = null;
        // Start is called before the first frame update
        private void Awake()
        {
            DontDestroyOnLoad(this);

            if (GameMain.Instance != null)
            {
                GameMain.Instance = null;
            }
            GameMain.Instance = this;

            
        }

        private MainMgr _mainmgr;
        void Start()
        {
            //ViewBag.ClassifyJsonStr = JsonConvert.SerializeObject(classifylist, settings);
            _mainmgr = new MainMgr();
            StartCoroutine(Begin());
        }

        private void FixedUpdate()
        {
            MainMgr.Instance.Step(0.02f);
        }

        // Update is called once per frame
        void Update()
        {
            MainMgr.Instance.Update();
        }
        

        private IEnumerator Begin()
        {
            //World.CreateWorld();
            //World.LoadWorld();
            //StateMgr.Instance.SetState(new StartState(StateMgr.Instance));
            yield return MainMgr.Instance.Start();
            //yield return SceneMgr.Instance.SetScene(new StartScene());
            yield break;
        }
        public bool StartState(SceneBase state)
        {
            StartCoroutine(SetState(state));
            return true;
        }
        private IEnumerator SetState(SceneBase state)
        {
            yield return SceneMgr.Instance.SetScene(state);
        }

        public void AddSubMono(GameObject obj,string name)
        {
            obj.name = name;
            Instantiate(obj, this.transform);
        }

    }

}