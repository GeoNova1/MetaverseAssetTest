using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DockUI : MonoBehaviour
{
    [SerializeField] Image radialImage;
    [SerializeField] TMP_Text percentageText;
    [SerializeField] string percentageTextPreface;
    

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <param name="percent"> The normalised docking progress in the range 0-1 </param>
    public void SetDockingProgress(float percent)
    {
        radialImage.fillAmount = percent;
        percentageText.text = percentageTextPreface + (percent * 100f).ToString("0") + "%";
    }
}
