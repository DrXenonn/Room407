using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private LayerMask InteractionLayer;
    [SerializeField] private Transform CamTransform;
    [SerializeField] private TextMeshProUGUI InteractionTxt;
    [SerializeField] private float InteractionRange = 3f;

    private IInteractable _lastInteractable;

    private void Update()
    {
        if (Physics.Raycast(CamTransform.position, CamTransform.forward, out var hit, InteractionRange, InteractionLayer))
        {
            var interactable = hit.transform.GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                if (_lastInteractable != interactable)
                {
                    InteractionTxt.text = interactable.GetHoverText();
                    _lastInteractable = interactable;
                }

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    interactable.OnInteract();
                }
            }
        }
        else
        {
            InteractionTxt.text = "";
            _lastInteractable = null;
        }
    }
}