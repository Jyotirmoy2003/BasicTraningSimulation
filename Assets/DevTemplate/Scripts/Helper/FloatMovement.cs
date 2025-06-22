using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatMovement : MonoBehaviour
{
    public float hoverAmplitude = 0.1f; // how much it moves up and down
    public float hoverFrequency = 1f;   // how fast it oscillates
    [SerializeField] Transform floatMesh;
    private Vector3 startPosition;

    void Start()
    {
        if (!floatMesh) floatMesh = transform;
        startPosition = floatMesh.localPosition;
    }
    void Update()
    {
        FloatinAir();
    }
    
    void FloatinAir()
    {
        float newY = Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        floatMesh.localPosition = startPosition + new Vector3(0f, newY, 0f);
    }
}
