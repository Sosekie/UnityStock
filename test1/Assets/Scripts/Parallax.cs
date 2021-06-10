using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform Cam;
    public float moveRate;
    private float startPointX;
    private float startPointY;
    public bool lockY;
    // Start is called before the first frame update
    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPointX + Cam.position.x * moveRate, startPointY);
        }
        else
        {
            transform.position = new Vector2(startPointY + Cam.position.x * moveRate, startPointY + Cam.position.x * moveRate);
        }
    }
}
