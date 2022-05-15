using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ShuShan
{
    public class NpcCreator:BaseMono
    {
        public Npc NpcData;

        public GameObject _Portrait;

        public GameObject _Name;

        public Sprite Portrait;

        public TextMeshPro Name;

        private void Awake()
        {
            if (_Portrait == null) {
                _Portrait = transform.Find("Portrait").gameObject;
            }
            Portrait = _Portrait.GetComponent<SpriteRenderer>().sprite;
            if (_Name == null) _Name = transform.Find("Name").gameObject;
            Name = _Name.GetComponent<TextMeshPro>();
        }

        public void Random()
        {
            NpcData = NpcMgr.Instance.CreatRandomNpc();
            Init(NpcData);
        }

        public void Init(Npc data)
        {
            Name.text = data.Name;
        }

    }
}
