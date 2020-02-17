using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkParty : MonoBehaviour
{
    public bool isActiveParty;
    public int spawnChance;
    public GameObject[] explotionsPrefab;

    private void Update()
    {
        FireworkExplotions();
        
    }

    void FireworkExplotions()
    {
        int randomExplotion = Random.Range(0, explotionsPrefab.Length);
        int chance = Random.Range(0, spawnChance);
        if (isActiveParty && chance == 0)
        {
            //Explotion particle system play
            GameObject cloneExplotion = Instantiate(explotionsPrefab[randomExplotion], GetSpawnPosition(), Quaternion.identity);
            cloneExplotion.transform.SetParent(transform);
            Destroy(cloneExplotion, 0.8f);
        }
    }

    Vector3 GetSpawnPosition()
    {
        Vector2 cameraLimits = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 randomPos = new Vector2(Random.Range(-cameraLimits.x, cameraLimits.x), Random.Range(-cameraLimits.y, cameraLimits.y));

        return randomPos;
    }
}
