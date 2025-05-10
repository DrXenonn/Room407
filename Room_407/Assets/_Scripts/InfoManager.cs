using System;
using TMPro;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI InfoText;
    
    public void OnInfo(Component sender, object data)
    {
        var doorData = (Tuple<string, float>)data;
        InfoText.text = doorData.Item1;
        CancelInvoke(nameof(ClearInfo));
        Invoke(nameof(ClearInfo), doorData.Item2);
    }

    private void ClearInfo()
    {
        InfoText.text = "";
    }
}