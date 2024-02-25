using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Vector3 camFollow;
    private Transform ball, win;

    private void Awake()
    {
        ball = FindObjectOfType<BallController>().transform;
    }

    private void Update()
    {
        if (win == null)
        {
            win = GameObject.Find("WinPrefab").GetComponent<Transform>();
            Debug.Log("Found");
        }

        if (transform.position.y > ball.transform.position.y && transform.position.y > win.transform.position.y+4f)
        {
            camFollow = new Vector3(transform.position.x,ball.transform.position.y,transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, camFollow.y, -5);
    }
}
