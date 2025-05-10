using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool IsLocked;
    [SerializeField] private GameEvent Info;
    [SerializeField] private GameEvent OnFirstTry;
    [SerializeField] private bool IsClosed = true;
    [SerializeField] private float RotationDuration;
    [SerializeField] private float OpenAngle;
    [SerializeField] private string InteractionText;

    private bool _isMoving;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    private bool _firstTry = true;

    private void Start()
    {
        _firstTry = true;
        _closedRotation = transform.localRotation;
        _openRotation = _closedRotation * Quaternion.Euler(0, 0, OpenAngle);
    }

    public void OnInteract()
    {
        if (_isMoving) return;
        
        if (IsLocked)
        {
            var doorData = new Tuple<string, float>("It's locked...", 2);
            Info.Raise(doorData);
            
            if (_firstTry)
            {
                OnFirstTry.Raise();
                _firstTry = false;
            }
            
            return;
        }

        StartCoroutine(OpenCloseAnimation());
    }

    public string GetHoverText()
    {
        return InteractionText;
    }

    private IEnumerator OpenCloseAnimation()
    {
        _isMoving = true;

        var startRot = transform.localRotation;
        var endRot = IsClosed ? _openRotation : _closedRotation;
        var elapsed = 0f;

        while (elapsed < RotationDuration)
        {
            transform.localRotation = Quaternion.Slerp(startRot, endRot, elapsed / RotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = endRot;
        IsClosed = !IsClosed;
        _isMoving = false;
    }
}