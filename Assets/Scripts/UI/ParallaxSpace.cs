using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSpace : MonoBehaviour
{
    public Transform playerTarjet;
    public Transform cameraTarjet;
    public float offsetScale;

    private bool existPlayer;

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Material material = meshRenderer.material;
        Vector2 offset = material.mainTextureOffset;

        //Revisa si existe el jugador dentro de el juego
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0)
        {
            playerTarjet = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }

        if (playerTarjet != null)
        {
            offset.x = playerTarjet.position.x / offsetScale;
            offset.y = playerTarjet.position.y / offsetScale;
        }
        else
        {
            offset.x = cameraTarjet.position.x / offsetScale;
            offset.y = cameraTarjet.position.y / offsetScale;
        }

        material.mainTextureOffset = offset;
    }
}
