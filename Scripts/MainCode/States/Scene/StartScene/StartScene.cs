using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using ShuShan;

namespace ShuShan
{

    public class StartScene : SceneBase
    {
        public StartScene()
        {
            this.StateName = g_emScene.开始界面;
            scenename = "Start";
        }
        
        public override void Init()
        {
            //TileMgr.Instance.LoadMapLocation("TianDuan");
            Debug.Log("开始界面");

            // UIMgr.Instance.ShowUI(UI_StartState_StartMenu.CreateInstance());

        }


        public override void Step( )
        {

        }
        public override void Render()
        {

        }
        public override void End()
        {
            //UIMgr.Instance.End();
        }

        public override void Enter()
        {
            
        }
    }
}
