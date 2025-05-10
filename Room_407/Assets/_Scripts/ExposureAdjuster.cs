using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ExposureAdjuster : MonoBehaviour
{
    [SerializeField] private Volume Volume;
    [SerializeField] private float ExposureStep;
    [SerializeField] private float MinExposure;
    [SerializeField] private float MaxExposure;

    private ColorAdjustments _colorAdjustments;

    private void Start()
    {
        Volume.profile.TryGet(out _colorAdjustments);
    }

    private void Update()
    {
        if (Keyboard.current.equalsKey.wasPressedThisFrame)
        {
            _colorAdjustments.postExposure.value = Mathf.Clamp(
                _colorAdjustments.postExposure.value + ExposureStep,
                MinExposure,
                MaxExposure
            );
        }

        if (Keyboard.current.minusKey.wasPressedThisFrame)
        {
            _colorAdjustments.postExposure.value = Mathf.Clamp(
                _colorAdjustments.postExposure.value - ExposureStep,
                MinExposure,
                MaxExposure
            );
        }
    }
}