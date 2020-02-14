using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSpace : MonoBehaviour
{
    public Transform tarjet;
    public float offsetScale;

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Material material = meshRenderer.material;
        Vector2 offset = material.mainTextureOffset;

        offset.x = tarjet.position.x / offsetScale;
        offset.y = tarjet.position.y / offsetScale;

        material.mainTextureOffset = offset;
    }
}
