using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PF
{
    public class click : MonoBehaviour
    {
        private bool isPick = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isPick)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (!Physics.Raycast(ray, out hit))
                {
                    return;
                }
                transform.position = hit.point + hit.normal;
            }
        }
        private void OnMouseDown()
        {
            if (StateMgr.Instance == null)
            {
                Debug.LogError(1010);
            }
            StateMgr.Instance.player = GameObject.Find("Cube1");
            //isPick = true;
        }

        private void OnMouseUp()
        {

            isPick = false;
        }
    }
}
