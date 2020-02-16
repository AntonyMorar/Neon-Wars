using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    CircleCollider2D shieldCollider;

    void Start()
    {
        shieldCollider = GetComponent<CircleCollider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
