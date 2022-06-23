using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    LineRenderer pointLineRenderer;
    LineRenderer midLineRenderer;
    LineRenderer subLineRenderer;
    GameObject[] points;
    GameObject[] midPoints;
    GameObject[] subPoints;


    public Color startColor;
    public Color endColor;
    public float lineWidth;
    public float alpha;
    public float t = 0.5f;

    int i;
    float t0;
    float t1;
    float t2;

    GameObject p0;
    GameObject p1;
    GameObject p2;
    GameObject p3;

    GameObject midA;
    GameObject midB;
    GameObject midC;

    GameObject sub0;
    GameObject sub1;
    GameObject f0;

    void Start()
    {
        Mathf.Clamp01(t);
        // POINTS

        p0 = GameObject.Find("P_0");
        p1 = GameObject.Find("P_1");
        p2 = GameObject.Find("P_2");
        p3 = GameObject.Find("P_3");


        // MID_POINTS

        midA = GameObject.Find("mid_A");
        midB = GameObject.Find("mid_B");
        midC = GameObject.Find("mid_C");

        //SUB_POINTS

        sub0 = GameObject.Find("sub_0");
        sub1 = GameObject.Find("sub_1");

        //POINT

        f0 = GameObject.Find("f_0");





        pointLineRenderer = gameObject.AddComponent<LineRenderer>();
        midLineRenderer = midA.AddComponent<LineRenderer>();
        subLineRenderer = sub1.AddComponent<LineRenderer>();

        BezierDraw();
    }

    void BezierDraw()
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(startColor, 0.0f), new GradientColorKey(endColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        pointLineRenderer.colorGradient = gradient;


        points = new GameObject[] { p0, p1, p2, p3 };

        midPoints = new GameObject[] { midA, midB, midC };

        subPoints = new GameObject[] { sub0, sub1 };



        for (int i = 0; i < points.Length; i++)
        {
            pointLineRenderer.positionCount = points.Length;

            // Debug.Log(points[i].transform.position);

            pointLineRenderer.SetPosition(i, points[i].transform.position);
            pointLineRenderer.widthMultiplier = 0.03f;
            pointLineRenderer.startColor = Color.cyan;




        }

        for (int j = 0; j < midPoints.Length; j++)
        {

            /* 
            
            MID-POINT FORMULAE x(t)=(1−t)p1+tp2

            */

            float p0_p1 = (p0.transform.position).magnitude - (p1.transform.position).magnitude;
            float p1_p2 = (p1.transform.position).magnitude - (p2.transform.position).magnitude;
            float p2_p3 = (p2.transform.position).magnitude - (p3.transform.position).magnitude;

            t = Mathf.Clamp01(((p0_p1 * p1_p2 * p2_p3 ) * -1) + 0.5f);









            midPoints[0].transform.position = new Vector2((1 - t) * (p0.transform.position.x) + (t * p1.transform.position.x), (1 - t) * (p0.transform.position.y) + (t * p1.transform.position.y));
            midPoints[1].transform.position = new Vector2((1 - t) * (p1.transform.position.x) + (t * p2.transform.position.x), (1 - t) * (p1.transform.position.y) + (t * p2.transform.position.y));
            midPoints[2].transform.position = new Vector2((1 - t) * (p2.transform.position.x) + (t * p3.transform.position.x), (1 - t) * (p2.transform.position.y) + (t * p3.transform.position.y));

            midLineRenderer.positionCount = midPoints.Length;
            midLineRenderer.widthMultiplier = 0.02f;
            midLineRenderer.SetPosition(j, midPoints[j].transform.position);

            Debug.Log(t);
        }

        for (int k = 0; k < subPoints.Length; k++)
        {
            subLineRenderer.positionCount = subPoints.Length;

            subPoints[0].transform.position = new Vector2((1 - t) * (midA.transform.position.x) + (t * midB.transform.position.x), (1 - t) * (midA.transform.position.y) + (t * midB.transform.position.y));
            subPoints[1].transform.position = new Vector2((1 - t) * (midB.transform.position.x) + (t * midC.transform.position.x), (1 - t) * (midB.transform.position.y) + (t * midC.transform.position.y));

            subLineRenderer.widthMultiplier = 0.01f;
            subLineRenderer.SetPosition(k, subPoints[k].transform.position);
        }

        f0.transform.position = new Vector2((1 - t) * (sub0.transform.position.x) + (t * sub1.transform.position.x), (1 - t) * (sub0.transform.position.y) + (t * sub1.transform.position.y));




    }
    void Update()
    {
        Application.targetFrameRate = 30;
        BezierDraw();
    }
}
