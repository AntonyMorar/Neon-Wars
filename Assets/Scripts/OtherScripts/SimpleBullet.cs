using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public GameObject hitEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter2D");
            //GameObject collitionEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            //Destroy(collitionEffect, 3f);
            Destroy(gameObject);
        }
    }
}
