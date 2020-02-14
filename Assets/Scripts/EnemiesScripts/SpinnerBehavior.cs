using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBehavior : Enemy
{
    //[Header("Spinner Status")]

    [Header("Spinner In Game")]
    public bool isMini;

    [Header("Spinner Editors")]
    public GameObject spinnerPrefab;

    public override void DestroyEnemy()
    {
        if (!isMini)
        {
            Instantiate(spinnerPrefab, new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(spinnerPrefab, new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z), Quaternion.identity);
        }
        base.DestroyEnemy();
    }
}
