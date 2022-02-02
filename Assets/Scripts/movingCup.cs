using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingCup : MonoBehaviour
{
    public float horizontalSpeed = 0.01f;
    public float verticalSpeed = 1.0f;
    public float amplitude = 1.0f;

    public Vector3 tempPosition;

    // Start is called before the first frame update
    void Start()
    {
        tempPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempPosition.x += horizontalSpeed;
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition;
    }
}

