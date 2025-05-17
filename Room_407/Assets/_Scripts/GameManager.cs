using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int TargetFrameRate;
    [SerializeField] private Movement Movement;
    [SerializeField] private CinemachineInputAxisController CamMovement;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TakeControl(bool state)
    {
        CamMovement.enabled = state;
        Movement.enabled = state;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Application.targetFrameRate = TargetFrameRate;
        QualitySettings.vSyncCount = 0;
    }
}