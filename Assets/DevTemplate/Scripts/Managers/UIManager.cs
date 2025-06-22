using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameObject UiContainer;
    [Header("Lap UI")]
    [SerializeField] Slider lapSlider;

    [Header("GunUI")]
    [SerializeField] Image gunIcon;
    [SerializeField] TMP_Text ammoText;


    void Start()
    {

    }

    #region GUN
    public void SetAmmo(int ammo)
    {
        ammoText.text = ammo.ToString();
    }

    public void SetGunReloadin(bool isReloadin)
    {
        if (isReloadin)
        {
            gunIcon.color = Color.red;
        }
        else
        {
            gunIcon.color = Color.white;
        }
    }
    #endregion



    #region LAP

    public void SetUpSlider(int max, int value)
    {
        lapSlider.maxValue = max;
        lapSlider.value = value;
    }

    public void SetLapValue(int value)
    {
        lapSlider.value = value;
    }

    #endregion



    public void BlackScreenFadeIn()
    {

    }

    public void BlackScreenFadeOut()
    {
        
    }

}
