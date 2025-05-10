using UnityEngine;

public class Billboarding : MonoBehaviour
{
    [SerializeField] private Transform CamTransform;
    
    private void Update()
    {
        transform.forward = CamTransform.forward;
    }
}