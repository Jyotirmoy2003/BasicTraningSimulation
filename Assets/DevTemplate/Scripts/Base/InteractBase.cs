using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractBase : MonoBehaviour, IInteractable
{
    public bool canInteract = true;
    [HideInInspector] public bool b_keepFilling = false;
    public Image sliderImage;
    public GameObject uiParent;
    [HideInInspector] public float filledAmount = 0;

    void Start()
    {
        if(uiParent)uiParent.SetActive(false);
    }

    public virtual void OnInteract()
    {

    }

    public virtual void OnPointerEnter()
    {
        if (canInteract)
        {
            b_keepFilling = true;
            StartCoroutine(FillButton());
            if (uiParent) uiParent.SetActive(true);
        }
    }

    public virtual void OnPointerExit()
    {
        StopCoroutine(FillButton());
        b_keepFilling = false;
        sliderImage.fillAmount = 0f;
        if (uiParent) uiParent.SetActive(false);

    }


    protected virtual IEnumerator FillButton()
    {
        float elapsedTime = 0f;

        while (b_keepFilling && elapsedTime < _GameAssets.Instance.fillDuration)
        {
            elapsedTime += Time.deltaTime;
            filledAmount = Mathf.Clamp01(elapsedTime / _GameAssets.Instance.fillDuration);
            sliderImage.fillAmount = filledAmount;

            yield return null;
        }

        if (filledAmount >= 1f)
        {
            sliderImage.fillAmount = 1f;
            OnInteract();
        }
    }

    public void ListenToChapterStart()
    {
        canInteract = true;
    }

    public void ListenToChapterEnd()
    {
        canInteract = false;
    }

    public virtual void Init()
    {
        EventManager.OnChapterEndEvent += ListenToChapterEnd;
        EventManager.OnChapterStartEvent += ListenToChapterStart;

        canInteract = true;
    }
}
