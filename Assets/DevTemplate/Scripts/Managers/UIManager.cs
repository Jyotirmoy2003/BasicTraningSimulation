using UnityEngine;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameObject UiContainer;
    [SerializeField] CanvasGroup blackScreenGroup;
    [SerializeField] float fadeTime = 0.3f;



    void Start()
    {
        EventManager.Init += BlackScreenFadeOut;
    }

    public void BlackScreenFadeIn()
    {
        blackScreenGroup.DOFade(1, fadeTime);
    }

    public void BlackScreenFadeOut()
    {
        blackScreenGroup.DOFade(0, fadeTime);
    }


    
}
