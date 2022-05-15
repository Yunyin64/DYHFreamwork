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
    public class InputMgr:Mgr
    {
        public InputMgr()
        {
            Name = "InputMgr";
        }
        public static InputMgr Instance { get; private set; }

        public override void Init()
        {
            InputMgr.Instance = this;
        }
        public override void Step(float dt)
        {
        }
        public override void Update()
        {
            _ColDown += Time.deltaTime;
            if(_ColDown > TimeColDown)
            {
                _ColDown -= TimeColDown;
                iscoldown = true;
            }
        }
        public override void End()
        {

        }

        public bool GetKey(KeyCode keyCode)
        {
            if(iscoldown && Input.GetKey(keyCode))
            {
                iscoldown = false;
                _ColDown = 0;
                return true;
            }
            return false;
        }
        private bool iscoldown = false;

        private float _ColDown = 0f;

        public float TimeColDown = 0.033f;

    }
}
