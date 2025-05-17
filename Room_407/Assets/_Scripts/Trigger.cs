using UnityEngine;
using UnityEngine.Playables;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Transform CamTransform;
    [SerializeField] private PlayableDirector Director;
    [SerializeField] private GameObject ActivatedObject;
    [SerializeField] private GameObject DeactivatedObject;
    [SerializeField] private float MinCamRotation, MaxCamRotation;
    private bool _triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        if(!other.CompareTag("Player")) return;
        
        if (GetYRotation() > MinCamRotation &&
            GetYRotation() < MaxCamRotation)
        {
            if(ActivatedObject) ActivatedObject.SetActive(true);
            if(DeactivatedObject) DeactivatedObject.SetActive(false);
        }
        
        Director?.Play();
        _triggered = true;
    }
    private float GetYRotation()
    {
        var y = CamTransform.localRotation.eulerAngles.y;
        if (y > 180f)
            y -= 360f;
        return y;
    }

}