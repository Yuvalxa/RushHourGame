using UnityEngine;
using System.Collections;
using System;

public class AreaObjectController : MonoBehaviour,IMovement
{
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;

    // Use this for initialization
    public static Action<AreaObjectController> OnOutOfBounds;

    // Update is called once per frame
    void Update()
	{
        if (transform.position.z < -15)
        {
            OnOutOfBounds?.Invoke(this);
        }
        Movement();

    }
    public void Movement()
    {
        transform.position += speed * Time.deltaTime * Vector3.back;
    }
    public void AccelarateCarSpeed(float acclerateEnemies)
    {
        float newSpeed = speed + acclerateEnemies;
        if (newSpeed < maxSpeed)
            speed = newSpeed;
    }
}

