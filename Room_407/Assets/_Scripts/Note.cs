using UnityEngine;
using UnityEngine.Playables;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayableDirector Director;
    [SerializeField] private Transform CutsceneCam;
    [SerializeField] private Transform PlayerCam;
    
    public void OnInteract()
    {
        CutsceneCam.position = PlayerCam.position;
        CutsceneCam.rotation = PlayerCam.rotation;
        Director.Play();
    }

    public string GetHoverText()
    {
        return "[E] Read";
    }
}