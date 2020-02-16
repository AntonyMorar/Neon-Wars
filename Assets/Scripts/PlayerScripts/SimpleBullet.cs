using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    [Header("Bullet Editors")]
    public GameObject explotion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "Shield")
        {
            if (collision.gameObject.tag == "Wall")
            {
                //Explotion particle system play
                GameObject cloneExplotion = (GameObject)Instantiate(explotion, transform.position, Quaternion.identity);
                //Play Sound BulletHitwall
                SoundManager.instance.PlaySound("BulletHitwall");
                //Destroy all
                Destroy(cloneExplotion, 0.8f);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
}
