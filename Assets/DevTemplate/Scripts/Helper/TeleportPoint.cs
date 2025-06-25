using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    [Header("Teleport Visuals")]
    public Transform targetMesh;              // Reference to the mesh transform
    public Vector3 meshInitialSize = Vector3.one * 0.5f;
    public Vector3 meshMaxSize = Vector3.one * 1.2f;


    [SerializeField] bool canInteract = true;
    [SerializeField] Transform playerPoint;

    private bool b_keepFilling = false;
    private float filledAmount = 0;
    private bool caninteractStatus = false;







    public void OnInteract()
    {
        canInteract = false;
        EventManager.Instance.AC_OnTeleportInitate?.Invoke();
        UIManager.Instance.BlackScreenFadeIn();

        Invoke(nameof(Teleport), 1f);
    }

    void Teleport()
    {
        _GameAssets.Instance.playerTranform.position = playerPoint.position;
        UIManager.Instance.BlackScreenFadeOut();
        Invoke(nameof(TeleportDone), 2f);
    }

    void TeleportDone()
    {
        EventManager.Instance.AC_OnTeleportDone?.Invoke(this);
    }

    void ListenToTeleportDoneEvent(TeleportPoint teleportPoint)
    {
        if (teleportPoint == this)
        {
            canInteract = false;
        }
        else
        {
            canInteract = true;
        }
    }

    #region Interface

    public void Init()
    {
        EventManager.Instance.AC_OnTeleportDone += ListenToTeleportDoneEvent;
        EventManager.OnChapterEndEvent += ListenToChapterEnd;
        EventManager.OnChapterStartEvent += ListenToChapterStart;
    }

    public void ListenToChapterStart()
    {
        caninteractStatus = canInteract;
    }

    public void ListenToChapterEnd()
    {
        canInteract = false;
    }

    public void OnPointerEnter()
    {
        if (caninteractStatus)
        {
            b_keepFilling = true;
            StartCoroutine(FillButton());
        }
    }

    public void OnPointerExit()
    {
        StopCoroutine(FillButton());
        b_keepFilling = false;
        targetMesh.localScale = meshInitialSize;
    }
    
    protected virtual IEnumerator FillButton()
    {
        float elapsedTime = 0f;

        // Make sure the mesh starts at its initial size
        if (targetMesh != null)
            targetMesh.localScale = meshInitialSize;

        while (b_keepFilling && elapsedTime < _GameAssets.Instance.fillDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _GameAssets.Instance.fillDuration);

            // Lerp the mesh scale from initial to max
            if (targetMesh != null)
                targetMesh.localScale = Vector3.Lerp(meshInitialSize, meshMaxSize, t);

            yield return null;
        }

        if (elapsedTime >= _GameAssets.Instance.fillDuration)
        {
            if (targetMesh != null)
                targetMesh.localScale = meshMaxSize;

            OnInteract();
        }
    }

    #endregion
}
