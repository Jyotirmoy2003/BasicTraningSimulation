using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GVR_Button : MonoBehaviour, IUI
{
    [Header("Button")]
    [SerializeField] Image normalImg;
    [SerializeField] Image hoverImg;
    [SerializeField] Image pressedImg;

    [Header("settings")]
    [SerializeField] Image sliderImage;
    private float filledAmount = 0;
    private bool b_isInFocus = false;
    private bool b_keepFilling = false;


    public UnityEvent buttonPressed;


    [NaughtyAttributes.Button]
    public void OnPointerEnter()
    {
        b_isInFocus = true;
        b_keepFilling = true;
        StartCoroutine(FillButton());
    }

    [NaughtyAttributes.Button]
    public void OnPointerExit()
    {
        b_isInFocus = false;
        StopCoroutine(FillButton());
        sliderImage.fillAmount = 0;
    }

    public void OnPoiterPressed()
    {
        b_keepFilling = false;
        buttonPressed?.Invoke();
    }

    IEnumerator FillButton()
    {
        float elapsedTime = 0f;

        while (b_isInFocus && b_keepFilling && elapsedTime < _GameAssets.Instance.fillDuration)
        {
            elapsedTime += Time.deltaTime;
            filledAmount = Mathf.Clamp01(elapsedTime / _GameAssets.Instance.fillDuration);
            sliderImage.fillAmount = filledAmount;

            yield return null;
        }

        if (filledAmount >= 1f)
        {
            sliderImage.fillAmount = 1f;
            OnPoiterPressed();
        }
    }
}
