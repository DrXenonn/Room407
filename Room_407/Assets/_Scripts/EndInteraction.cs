using UnityEngine;

public class EndInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject EndObject;
    
    
    public void OnInteract()
    {
        EndObject.SetActive(true);
        Time.timeScale = 0;
    }

    public string GetHoverText()
    {
        return "[E] Exit";
    }
}