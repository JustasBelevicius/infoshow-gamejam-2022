using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayConfig : MonoBehaviour
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
        Config config = ConfigLoader.getConfig();
        text.text = $"version: {config.version}\nstarting food: {config.startingFood}\naction cost: {config.actionCost}\nmax food: {config.maxFood}";
    }

}
