using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quality : MonoBehaviour
{
    public string quality;

    public void SetQuality(string newQuality)
    {
        quality = newQuality;
        Debug.Log($"Food quality: {newQuality}");
    }
}
