using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffsetController : MonoBehaviour
{
    [SerializeField] private Material roadMat;
    [SerializeField] private Material backgroundMat;

    public float speed = 1;

    private void Update()
    {
        float offset = Time.time * speed;
        roadMat.mainTextureOffset = new Vector2(0, offset);
        backgroundMat.mainTextureOffset = new Vector2(0, offset);
    }
}
