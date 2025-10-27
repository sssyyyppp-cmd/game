using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichBounds : MonoBehaviour


{
    // Start is called before the first frame update
    void Start()
    {
        swichConfinerShape();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void swichConfinerShape()
    {
        PolygonCollider2D pc2d = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();

        CinemachineConfiner cinemachine = GetComponent<CinemachineConfiner>();

        cinemachine.m_BoundingShape2D =pc2d;
        //清楚缓存达到刷新碰撞边界的作用
        cinemachine.InvalidatePathCache();
    }
}
