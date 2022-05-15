using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PF
{

public class gv : MonoBehaviour
{
    public  GameObject player;
    CharacterController cc;
    public float surfaceOffset = 1f;
    public Cinemachine.CinemachineVirtualCamera cv;
    Vector3 k;
    // Start is called before the first frame update
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
        k = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(0);
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(1);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            Debug.Log(2);
            return;
            }
             k = hit.point + hit.normal * surfaceOffset ;
            k.y = 6.5f;
            //cc.Move(k);
           // cc.transform.position = hit.point + hit.normal * surfaceOffset ;
        }
        //cc.Move(Vector3.down * 0.098f);
        
            float X = Input.GetAxis("Mouse X") * 2;
            float Y = Input.GetAxis("Mouse Y") * 2 ;
        if (Input.GetMouseButton(2))
        {
            k += Vector3.forward * Y + Vector3.right * X;
            //cc.Move(Vector3.forward *Y + Vector3.right *X  );
        }
        //cc.transform.position += Vector3.down * 0.098f;
        float d = Input.GetAxis("Mouse ScrollWheel");
        cv.m_Lens.FieldOfView += -d*5;



        if (Input.GetMouseButton(1))
        {
            //Vector3 v3 = obj.transform.position;
            //v3.y = transform.position.y;
            transform.Rotate(Vector3.up, X);

        }
        cc.Move((k - cc.transform.position) / 15f);

        if (Input.GetKeyUp(KeyCode.Space))
        {
                if(StateMgr.Instance.player != null)
                {
                    player = StateMgr.Instance.player;
                }
            k = player.transform.position;
                
                
        }
    }
    private void FixedUpdate()
    {
    }
}

}