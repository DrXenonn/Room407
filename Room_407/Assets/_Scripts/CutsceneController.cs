using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector Director;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject CutsceneCamera;
    [SerializeField] private RoomTransition Transition;
    private bool _isWaitingForInput = false;

    private void Update()
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
    
    public void EnterGameplay()
    {
        CutsceneCamera.SetActive(false);
        Player.SetActive(true);
    }

    public void ExitGameplay()
    {
        CutsceneCamera.SetActive(true);
        Player.SetActive(false);
    }
}