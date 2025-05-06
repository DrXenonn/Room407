using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public struct Footstep
{
    public string Tag;
    public AudioClip[] Clips;
    public float MinPitch, MaxPitch;
    public float Volume;
}

public class Footsteps : MonoBehaviour
{
    [SerializeField] private List<Footstep> FootstepsList = new();
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform RayOrigin;
    [SerializeField] private Movement Movement;

    private Dictionary<string, Footstep> _footstepDictionary;
    private Footstep _currentFootstep;

    private void Start()
    {
        _footstepDictionary = new Dictionary<string, Footstep>();

        foreach (var footstep in FootstepsList)
        {
            _footstepDictionary[footstep.Tag] = footstep;
        }

        InvokeRepeating(nameof(SlowUpdate), 0, 0.1f);
    }

    public void OnStep(Component sender, object data)
    {
        if (_currentFootstep.Clips == null || _currentFootstep.Clips.Length == 0) return;
        
        var randomIndex = Random.Range(0, _currentFootstep.Clips.Length);
        var randomClip = _currentFootstep.Clips[randomIndex];

        AudioSource.pitch = Random.Range(_currentFootstep.MinPitch, _currentFootstep.MaxPitch);
        AudioSource.volume = _currentFootstep.Volume;
        AudioSource.PlayOneShot(randomClip);
    }

    private void SlowUpdate()
    {
        if (!Movement.IsMoving) return;
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        if (!Physics.Raycast(RayOrigin.position, Vector3.down, out var hit, 2, GroundLayer)) return;

        var objectTag = hit.collider.tag;
        _currentFootstep = _footstepDictionary.GetValueOrDefault(objectTag, _currentFootstep);
    }
}