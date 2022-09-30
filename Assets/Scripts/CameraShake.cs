using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Desired duration of the shake effect
    private float shakeDuration = 0.3f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.2f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 0.4f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void OnEnable()
    {
        initialPosition = transform.localPosition;
        shakeDuration = 0.3f;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
            this.enabled = false;
        }
    }
}