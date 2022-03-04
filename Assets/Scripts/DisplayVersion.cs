using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayVersion : MonoBehaviour
{
    [SerializeField]
    Text text;

    void Awake()
    {
        if (text == null)
        {
            Debug.LogError("`text` parameter is not assigned");
            return;
        }
        text.text = ConfigLoader.getConfig().version;
    }
}
