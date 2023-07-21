using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] MeshRenderer meshRenderer;

    private Material carMaterial;
    public static Action<EnemyController> OnOutOfBounds;

    private void Awake()
    {
        
        carMaterial = new Material(meshRenderer.materials[2]);
        meshRenderer.materials[2] = carMaterial;
    }
    void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.back;   
        if(transform.position.z < -15)
        {
            OnOutOfBounds?.Invoke(this);
            
        }
    }
    public void SetColor(Color color)
    {
        meshRenderer.materials[2].color = color;
    }

    public void AccelarateCarSpeed(float acclerateEnemies)
    {
        float newSpeed = speed + acclerateEnemies;
        if (newSpeed < maxSpeed)
            speed = newSpeed;
    }
}
