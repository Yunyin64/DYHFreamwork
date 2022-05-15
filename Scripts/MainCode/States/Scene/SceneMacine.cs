using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.SceneManagement;

namespace ShuShan
{
    public class SceneMacine : StateMacine
    {
        
        public SceneBase CurScene
        {
            get
            {
               return (SceneBase)_CurState;
            }
            set
            {
                _CurState = value;
            }
        }
        public void AddSubScene(SceneBase scene)
        {
            if (!m_States.ContainsKey(scene.scenename)){
                m_States.Add(scene.scenename, scene);
            }
        }

        public IEnumerator EnterScene(SceneBase state)
        {
            if (CurScene != null) CurScene.End();
            CurScene = state;
            yield return SceneManager.LoadSceneAsync(CurScene.scenename);

            if (CurScene != null && !CurScene.isInited)
            {
                CurScene.Init();
                CurScene.isInited = true;
            }

        }

    }
}
