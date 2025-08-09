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
    [SerializeField] Image coreImage;
    private float filledAmount = 0;
    private bool b_isInFocus = false;
    private bool b_keepFilling = false;
    [SerializeField] bool isInteractable = true;
    [SerializeField] Color cachedColorCoreImage;

    public UnityEvent buttonPressed;


    void Start()
    {
        cachedColorCoreImage = coreImage.color;
        SetInteractable(isInteractable);
    }

    [NaughtyAttributes.Button]
    public void OnPointerEnter()
    {
        if (!isInteractable) return;
        b_isInFocus = true;
        b_keepFilling = true;
        StartCoroutine(FillButton());
    }

    [NaughtyAttributes.Button]
    public void OnPointerExit()
    {
        if (!isInteractable) return;
        b_isInFocus = false;
        StopCoroutine(FillButton());
        sliderImage.fillAmount = 0;
    }

    public void OnPoiterPressed()
    {
        if (!isInteractable) return;
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

    public void SetInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
        coreImage.color = (!isInteractable) ? new Color(cachedColorCoreImage.r, cachedColorCoreImage.g, cachedColorCoreImage.b, cachedColorCoreImage.a - 30) :
            cachedColorCoreImage;
    }
}
