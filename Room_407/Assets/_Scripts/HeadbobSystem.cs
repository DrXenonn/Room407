using UnityEngine;

public class HeadbobSystem : MonoBehaviour
{
    [SerializeField] private Movement Movement;

    [Range(0.001f, 0.01f)] public float Amount = 0.002f;
    [Range(1f, 30f)] public float Frequency = 10.0f;
    [Range(10f, 100f)] public float Smooth = 10.0f;

    private Vector3 _startPos;

    private float _previousSinValue;
    private bool _stepTriggered;

    private void Start()
    {
        _startPos = transform.localPosition;
        _previousSinValue = Mathf.Sin(Time.time * Frequency);
        _stepTriggered = false;
    }

    private void Update()
    {
        if (Movement.IsMoving)
        {
            StartHeadBob();
        }
        else
        {
            ResetHeadbob();
            StopHeadbob();
        }
    }

    private void StartHeadBob()
    {
        var pos = Vector3.zero;
        var sinValue = Mathf.Sin(Time.time * Frequency);

        pos.y += Mathf.Lerp(pos.y, sinValue * Amount * 1.4f, Smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
        transform.localPosition += pos;
        
        if (_previousSinValue > 0 && sinValue < 0 && !_stepTriggered)
        {
            _stepTriggered = true;
        }
        else if (sinValue > 0)
        {
            _stepTriggered = false;
        }

        _previousSinValue = sinValue;
    }

    private void StopHeadbob()
    {
        if (transform.localPosition != _startPos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, Smooth * Time.deltaTime);
        }
    }

    private void ResetHeadbob()
    {
        _previousSinValue = Mathf.Sin(Time.time * Frequency);
        _stepTriggered = false;
    }
}
