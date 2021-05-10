using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityUI : SingletonPattern<SanityUI>
{
    [SerializeField] private Image sanitySprite;
    [SerializeField] private PlayerStats myStats;

    public Color32[] colors = new Color32[4];

    private void Update()
    {
        CheckSanity();
    }

    public void CheckSanity()
    {
        float sanityPercent = (myStats.sanity / myStats.maxSanity) * 100f;

        if (sanityPercent < 25f)
        {
            sanitySprite.color = colors[3];
        }
        else if (sanityPercent < 50f)
        {
            sanitySprite.color = colors[2];
        }
        else if (sanityPercent < 75f)
        {
            sanitySprite.color = colors[1];
        }
        else
        {
            sanitySprite.color = colors[0];
        }
    }
}
