using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector Director;
    private bool _isWaitingForInput = false;

    void Update()
    {
        if (!_isWaitingForInput || !Keyboard.current.eKey.wasPressedThisFrame) return;
        
        _isWaitingForInput = false;
        ResumeTimeline();
    }

    public void PauseForInput()
    {
        Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        _isWaitingForInput = true;
    }

    private void ResumeTimeline()
    {
        Director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}