using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HoverManager : MonoBehaviour
{

    [Range(-1f, 1f)]
    public float horizontalSpeed;
    [Range(0, 5)]
    public float verticalSpeed;
    [Range(0, 2)]
    public float amplitude;

    private Vector3 originalPos;
    private Vector3 tempPosition;

    void Start()
    {
        tempPosition = originalPos = transform.position;
    }

    void FixedUpdate()
    {
        tempPosition = originalPos;
        tempPosition.x += horizontalSpeed * Time.fixedDeltaTime;
        tempPosition.y += Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition;

        if(Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * 100);
        }
    }
}