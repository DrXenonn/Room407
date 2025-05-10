using System.Collections;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private AudioSource TriggerSound;
    [SerializeField] private GameObject TriggerObject;
    [SerializeField] private Transform CamTransform;
    [SerializeField] private float SoundDelay;
    [SerializeField] private float MinCamRotation, MaxCamRotation;
    private bool _triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        if(!other.CompareTag("Player")) return;
        if (GetYRotation() > MinCamRotation &&
            GetYRotation() < MaxCamRotation)
        {
            TriggerObject.SetActive(true);
            StartCoroutine(PlaySoundDelayed());
            _triggered = true;
        }
    }

    private IEnumerator PlaySoundDelayed()
    {
        yield return new WaitForSeconds(SoundDelay);
        TriggerSound.Play();
    }
    
    private float GetYRotation()
    {
        var y = CamTransform.localRotation.eulerAngles.y;
        if (y > 180f)
            y -= 360f;
        return y;
    }

}