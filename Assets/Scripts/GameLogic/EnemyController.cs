using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AreaObjectController
{
    [SerializeField] MeshRenderer meshRenderer;

    private Material carMaterial;

    private void Awake()
    { 
        carMaterial = new Material(meshRenderer.materials[2]);
        meshRenderer.materials[2] = carMaterial;
    }
    public void SetColor(Color color)
    {
        meshRenderer.materials[2].color = color;
    }


}
