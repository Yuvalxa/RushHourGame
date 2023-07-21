using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.Sounds;
using UnityEditor.Rendering;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float boost;
    [SerializeField] LayerMask layers;
    [SerializeField] int playerhealth = 3;
    [SerializeField] GameObject rightBorder;
    [SerializeField] GameObject leftBorder;
    [SerializeField] ParticleSystem particle;
    [SerializeField] AudioClip crushMusic;
    public static Action<int> OnCarCrush;
    private void Start()
    {
        InvokePlayerHealth();
    }
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float carTotalSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) // bost shift key
        {
            carTotalSpeed += boost;
            particle.startColor = Color.red;
        }
        else
            particle.startColor = Color.yellow;

        float input = Input.GetAxis("Horizontal");
        Vector3 movementVector = input * carTotalSpeed * Time.deltaTime * Vector3.right;
        Vector3 movementTarget = transform.position + movementVector;
        
        if (movementTarget.x < rightBorder.transform.position.x && movementTarget.x> leftBorder.transform.position.x)
            transform.position += movementVector;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            SoundManager.Instance.PlayEffect(crushMusic);
            playerhealth--;
            InvokePlayerHealth();
            EnemyController enemy = other.GetComponentInParent<EnemyController>();
            enemy.gameObject.SetActive(false);
        }
    }
    private void InvokePlayerHealth()
    {
        OnCarCrush?.Invoke(playerhealth);
    }
}
