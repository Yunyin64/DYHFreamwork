using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShuShan
{
    public class RenderPool:Pool
    {
        public static RenderPool Instance { get;private set; }
        private void Awake()
        {
            this.CMDIconRoot = new GameObject("_IconRoot");
            this.CMDIconPool = new GameObject("Pool");
            this.CMDIconPool.transform.SetParent(this.CMDIconRoot.transform);
            this.CMDIconPool.SetActive(false);
            base.gameObject.AddComponent<ResourceLoader>();
            this.ViewTex3D = new RenderTexture(512, 512, 24);
            RenderPool.Instance = this;
        }

        public GameObject CMDIconRoot;
        public GameObject CMDIconPool;

        private AssatMgr _assetmgr;

        public AssatMgr AssatMgr
        {
            get
            {
                if (_assetmgr == null && AssatMgr.Instance != null) {
                    _assetmgr = AssatMgr.Instance;
                }
                return _assetmgr;
            }
        }

        private RenderTexture ViewTex3D;
    }
}
