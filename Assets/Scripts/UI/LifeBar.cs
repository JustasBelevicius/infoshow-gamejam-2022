using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField]
    List<Image> bubbles;
    Config config = ConfigLoader.getConfig();

    public void SetLife(float life)
    {
        for (int i = 0; i < config.maxFood; i++)
        {
            if (i < Mathf.FloorToInt(life))
            {
                bubbles[i].fillAmount = 1;
            } else if (i >= Mathf.CeilToInt(life))
            {
                bubbles[i].fillAmount = 0;
            } else
            {
                bubbles[i].fillAmount =  life - Mathf.FloorToInt(life);
            }
        }
    }
}
