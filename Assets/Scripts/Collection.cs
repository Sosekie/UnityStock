using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public virtual void Death()
    {
        FindObjectOfType<PlayerController>().CherryCount();
        Destroy(gameObject);

    }
}
