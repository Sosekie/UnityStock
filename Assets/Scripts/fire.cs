using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{

    public float speed;
    public float distance;
    public Transform LeftFire;
    public Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        {
            float distancePlayer = (transform.position - playerTransform.position).sqrMagnitude;
            double disa = System.Math.Sqrt(distancePlayer - distance);
            float dis1 = (float)disa;

            float distancePlayerLeft = (transform.position - LeftFire.position).sqrMagnitude;
            double disLeft1 = System.Math.Sqrt(distancePlayerLeft);
            float disLeft = (float)disLeft1;

            if (distancePlayer >= distance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.fixedDeltaTime* (dis1+ disLeft) /10);
            }
            else if(distancePlayerLeft >= distance/5)
            {
                transform.position = Vector2.MoveTowards(transform.position, LeftFire.position, speed * Time.fixedDeltaTime * disLeft / 10);
            }
        }
    }
}
