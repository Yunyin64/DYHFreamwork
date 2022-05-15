using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class line : MonoBehaviour
{

    public List<GameObject> characters;
    public Color rectColor = Color.green;
    public Cinemachine.CinemachineVirtualCamera cvc;

    private Vector3 start = Vector3.zero;//������갴��λ��

    // private Material rectMat = null;//���ߵĲ��� ���趨ϵͳ���õ�ǰ���ʻ��� ������ɿ�
    public Material rectMat = null;//����ʹ��Sprite�µ�defaultshader�Ĳ��ʼ���

    private bool drawRectangle = false;//�Ƿ�ʼ���߱�־

    // Use this for initialization

    void Start()
    {

        //        rectMat = new Material("Shader \"Lines/Colored Blended\" {" +
        //
        //        "SubShader { Pass { " +
        //
        //        "    Blend SrcAlpha OneMinusSrcAlpha " +
        //
        //        "    ZWrite Off Cull Off Fog { Mode Off } " +
        //
        //        "    BindChannels {" +
        //
        //        "      Bind \"vertex\", vertex Bind \"color\", color }" +
        //
        //        "} } }");//���ɻ��ߵĲ���

        rectMat.hideFlags = HideFlags.HideAndDontSave;

        rectMat.shader.hideFlags = HideFlags.HideAndDontSave;//����ʾ��hierarchy����е���ϣ������浽��������ж��Resources.UnloadUnusedAssets��ж�صĶ���

    }
    IEnumerator changecamera()
    {
        for (int i = 0; i < 100; i++)
        {
           cvc.GetCinemachineComponent<Cinemachine.CinemachineTrackedDolly>().m_PathPosition += 0.02f;
            cvc.transform.Rotate(Vector3.up, -0.9f,Space.World);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(changecamera());
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Time.timeScale = 0.6f;
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            Time.timeScale = 1f;
        }
        if (Input.GetMouseButtonDown(0))
        {

            drawRectangle = true;//������������� ���ÿ�ʼ���߱�־

            start = Input.mousePosition;//��¼����λ��

        }
        else if (Input.GetMouseButtonUp(0))
        {

            drawRectangle = false;//����������ſ� ��������
            checkSelection(start, Input.mousePosition);
        }
    }



    void OnPostRender()
    {//�������ֲ����Ƽ���OnPostRender��������� ������ֱ�ӷ���Update��������Ҫ��־������

        if (drawRectangle)
        {

            Vector3 end = Input.mousePosition;//��굱ǰλ��

            GL.PushMatrix();//����������任����,��ͶӰ��ͼ�����ģ����ͼ����ѹ���ջ����

            if (!rectMat)

                return;

            rectMat.SetPass(0);//Ϊ��Ⱦ���������pass��

            GL.LoadPixelMatrix();//��������Ļ�����ͼ

            GL.Begin(GL.QUADS);//��ʼ���ƾ���

            GL.Color(new Color(rectColor.r, rectColor.g, rectColor.b, 0.1f));//������ɫ��͸���ȣ������ڲ�͸��

            //���ƶ���
            GL.Vertex3(start.x, start.y, 0);

            GL.Vertex3(end.x, start.y, 0);

            GL.Vertex3(end.x, end.y, 0);

            GL.Vertex3(start.x, end.y, 0);

            GL.End();


            GL.Begin(GL.LINES);//��ʼ������

            GL.Color(rectColor);//���÷���ı߿���ɫ �߿�͸��

            GL.Vertex3(start.x, start.y, 0);

            GL.Vertex3(end.x, start.y, 0);

            GL.Vertex3(end.x, start.y, 0);

            GL.Vertex3(end.x, end.y, 0);

            GL.Vertex3(end.x, end.y, 0);

            GL.Vertex3(start.x, end.y, 0);

            GL.Vertex3(start.x, end.y, 0);

            GL.Vertex3(start.x, start.y, 0);

            GL.End();

            GL.PopMatrix();//�ָ������ͶӰ����

        }

    }

    //��ⱻѡ�������
    void checkSelection(Vector3 start, Vector3 end)
    {

        Vector3 p1 = Vector3.zero;

        Vector3 p2 = Vector3.zero;

        if (start.x > end.x)
        {//��Щ�ж�������ȷ��p1��xy����С��p2��xy���꣬��Ϊ���Ŀ򲻼��þ������µ�������������

            p1.x = end.x;

            p2.x = start.x;

        }

        else
        {

            p1.x = start.x;

            p2.x = end.x;

        }

        if (start.y > end.y)
        {

            p1.y = end.y;

            p2.y = start.y;

        }

        else
        {

            p1.y = start.y;

            p2.y = end.y;

        }

        foreach (GameObject obj in characters)
        {//�ѿ�ѡ��Ķ��󱣴���characters������

            Vector3 location = Camera.main.WorldToScreenPoint(obj.transform.position);//�Ѷ����positionת������Ļ����

            if (location.x < p1.x || location.x > p2.x || location.y < p1.y || location.y > p2.y

            || location.z < Camera.main.nearClipPlane || location.z > Camera.main.farClipPlane)//z���������������趨ֵ����������Ҳ����Ҫѡ����

            {
                //disselecting(obj);//�����������ɸѡ ����ѡ��Χ�ڵĶ���Ȼ�����ȡ��ѡ����������������ŵ�default�㣬�Ͳ���ʾ��������
                print("---" + obj.name);
            }

            else

            {
                //selecting(obj);//����ͽ���ѡ�в��������������ŵ��������ߵĲ�ȥ
                print("+++" + obj.name);

            }

        }

    }
}