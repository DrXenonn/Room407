using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light[] LightSources;
    [SerializeField] private float MinIntensity;
    [SerializeField] private float MaxIntensity;
    [SerializeField] private float FlickerSpeed;

    private void Start()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            var randomIntensity = Random.Range(MinIntensity, MaxIntensity);
            
            foreach (var lightSource in LightSources)
            {
                lightSource.intensity = randomIntensity;
            }
            
            yield return new WaitForSeconds(Random.Range(FlickerSpeed / 2f, FlickerSpeed * 2f));
        }
    }

    public void ChangeFlickerSettings(float minIntensity, float maxIntensity, float flickerSpeed)
    {
        MinIntensity = minIntensity;
        MaxIntensity = maxIntensity;
        FlickerSpeed = flickerSpeed;
    }
}