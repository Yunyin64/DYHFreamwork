using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ShuShan;

namespace ShuShan
{
    public class SceneMgr : Mgr
    {
        
        public static SceneMgr Instance { get; private set; }

        public override void Init()
        {
            SceneMgr.Instance = this;
            _SceneMacine.AddSubScene(new StartScene());
        }
        public override void Step(float dt)
        {
            _SceneMacine.Step();
        }

        public override void Render(float dt)
        {

            _SceneMacine.Render();
        }
        public override void End()
        {

        }
        public IEnumerator SetScene(SceneBase scene)
        {
            yield return _SceneMacine.EnterScene(scene);

        }

        private SceneMacine _SceneMacine = new SceneMacine();
        //private Scene _state;
    }
}
