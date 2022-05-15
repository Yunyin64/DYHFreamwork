using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject o1;
    public float k ;
    public float o1d;
    public GameObject o2;
    public float o2d;
    public bool isMove = false;

    public Vector3 d1d;
    public Vector3 d2d;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        o1d = Vector3.Distance(transform.position, o1.transform.position);
        o2d = Vector3.Distance(transform.position, o2.transform.position);

        rb = GetComponent<Rigidbody>();
        k = 100f;

    }

    // Update is called once per frame
    void Update()
    {
        float dis = (o1d - Vector3.Distance(transform.position, o1.transform.position));
        Vector3 no = ((transform.position - o1.transform.position).normalized);
         d1d = dis*k*no;
        d2d = (o2d - Vector3.Distance(transform.position, o2.transform.position) )* k * ((transform.position - o2.transform.position).normalized);

        //rb.AddForce(d1d);
       if(!isMove) rb.AddForce(d1d + d2d);

    }
    public void click()
    {

    }
}
