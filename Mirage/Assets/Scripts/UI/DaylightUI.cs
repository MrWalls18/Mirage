using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaylightUI : MonoBehaviour
{

    public Image icon;
    public RectTransform iconPlacement;
    public float fillAmount;
    public Color dayColor;
    public Color nightColor;

    private void Update()
    {
        icon.fillAmount = Sun.Instance.sunPercentage;
        fillAmount = icon.fillAmount;
        iconPlacement.anchorMin = new Vector2(1 - fillAmount, iconPlacement.anchorMin.y);
        iconPlacement.anchorMax = new Vector2(1 - fillAmount, iconPlacement.anchorMax.y);
        iconPlacement.anchoredPosition = Vector2.zero;
        icon.color = Color.Lerp(dayColor, nightColor, fillAmount);
    }

}
