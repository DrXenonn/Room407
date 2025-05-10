using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private PlayableDirector Cutscene2;
    [SerializeField] private Volume Volume;
    [SerializeField] private VolumeProfile HorrorVolume;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Transform DefaultPlayerTransform;
    [SerializeField] private Door Room407Door;
    [SerializeField] private LightFlicker LightFlicker;
    [SerializeField] private AudioSource Radio;
    [SerializeField] private AudioClip EvacClip;
    [SerializeField] private Animator ClockAnimation;
    [SerializeField] private GameObject[] OldItems;
    [SerializeField] private GameObject[] NewItems;

    private bool _hasChanged = false;

    public void OnDoorFirstTry(Component sender, object data)
    {
        if (_hasChanged) return;
        StartCoroutine(BlackoutDelayed());
        _hasChanged = true;
    }

    private IEnumerator BlackoutDelayed()
    {
        yield return new WaitForSeconds(10f);

        Cutscene2.Play();
    }

    public void ChangeRoom()
    {
        Volume.profile = HorrorVolume;
        Room407Door.IsLocked = false;
        PlayerTransform.position = DefaultPlayerTransform.position;
        PlayerTransform.rotation = DefaultPlayerTransform.rotation;
        Radio.clip = EvacClip;
        ClockAnimation.StopPlayback();
        
        foreach (var oldItem in OldItems)
        {
            oldItem.SetActive(false);
        }

        foreach (var newItem in NewItems)
        {
            newItem.SetActive(true);
        }
        
        StartCoroutine(PlayAudioDelayed());
        StartCoroutine(StopLoop());
        LightFlicker.ChangeFlickerSettings(0.35f, 0.5f, 0.08f);
    }

    private IEnumerator PlayAudioDelayed()
    {
        yield return new WaitForSeconds(4.5f);
        Radio.Play();
    }

    private IEnumerator StopLoop()
    {
        yield return new WaitForSeconds(35);
        Radio.loop = false;
    }
}