using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirelightFlicker : MonoBehaviour
{
    [SerializeField, Min(0)] float maxIntensity = 150f;
    [SerializeField, Min(0)] float minIntensity = 50f;
    [SerializeField, Min(0)] float maxFlickerFrequency = 1f;
    [SerializeField, Min(0)] float minFlickerFrequency = 0.1f;
    [SerializeField, Min(0)] float strength = 5f;

    float baseIntensity;
    float nextIntensity;

    float flickerFrequency;
    float timeOfLastFlicker;

    private Light LightSource => GetComponent<Light>();


    private void OnValidate()
    {
        if (maxIntensity < minIntensity) minIntensity = maxIntensity;
        if (maxFlickerFrequency < minFlickerFrequency) minFlickerFrequency = maxFlickerFrequency;
    }

    private void Awake()
    {
        baseIntensity = LightSource.intensity;
        timeOfLastFlicker = Time.time;
    }

    private void Update()
    {
        if (timeOfLastFlicker + flickerFrequency < Time.time)
        {
            timeOfLastFlicker = Time.time;
            nextIntensity = Random.Range(minIntensity, maxIntensity);
            flickerFrequency = Random.Range(minFlickerFrequency, maxFlickerFrequency);
        }

        Flicker();
    }

    private void Flicker()
    {
        LightSource.intensity = Mathf.Lerp(
            LightSource.intensity,
            nextIntensity,
            strength * Time.deltaTime
            );
    }

    public void Reset()
    {
        LightSource.intensity = baseIntensity;
    }
}