using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PickupBase : InteractBase
{
    public int itemId = -1;
    public Rigidbody rb;
    public Image sliderImageForGrab;
    public bool canPick = true;
    public bool canPickablebyLeft = true;
    public bool canPickablebyRight = true;
    public bool reset = true;
    [HideInInspector] public bool picked = false;



    public E_hand objectHoldingHand;
    private Vector3 resetLocation;
    private Vector3 resetRotation;

    public Action AC_OnObjectGrabbed;
    public Action AC_OnObjectDropped;
    public Action AC_OnObjectReset;

    private Transform myTranform;
    private bool showHighlight = false;
    public GameObject highlightArrow;


    void Start()
    {
        myTranform = transform;
        resetLocation = myTranform.position;
        resetRotation = myTranform.eulerAngles;

        uiParent.SetActive(false);
        highlightArrow.SetActive(false);
    }


    public override void OnInteract()
    {
        picked = PickupManager.Instance.PickObject(this);
    }

    public override void OnPointerEnter()
    {
        if (canPick && !picked)
        {
            uiParent.SetActive(true);
            b_keepFilling = true;
            StartCoroutine(FillButton());
        }
    }

    public override void OnPointerExit()
    {
        StopCoroutine(FillButton());
        b_keepFilling = false;
        sliderImageForGrab.fillAmount = 0f;
        uiParent.SetActive(false);
    }

    public virtual void HighlightPickup(bool show)
    {
        showHighlight = show;
        highlightArrow.SetActive(show);

    }


    #region PICKUP

    public void Picked(Transform parentHandmyTranform)
    {
        if (rb)
        {
            rb.isKinematic = true;
        }
        myTranform.position = parentHandmyTranform.position;
        myTranform.SetParent(parentHandmyTranform);
        uiParent.SetActive(false);
        highlightArrow.SetActive(false);
        AC_OnObjectGrabbed?.Invoke();
        //parentmyTranform = parentHandmyTranform;
    }

    public void Drop()
    {
        myTranform.parent = null;
        rb.isKinematic = false;
        //Events
        AC_OnObjectDropped?.Invoke();
        EventManager.AC_ObjectDropped?.Invoke(this);

        if (reset)
        {
            Invoke(nameof(Reset), 2f);
        }
        else
        {
            picked = false;
        }
    }


    public void Reset()
    {
        //Tranform
        myTranform.parent = null;
        myTranform.position = resetLocation;
        myTranform.eulerAngles = resetRotation;
        if (showHighlight) highlightArrow.SetActive(true); //highlight

        rb.isKinematic = false; //Rigidbody
        AC_OnObjectReset?.Invoke();

        picked = false;
    }

    public void RemoveFromPlayArea()
    {
        myTranform.parent = null;
        rb.isKinematic = true;

        highlightArrow.SetActive(false);
        EventManager.AC_ObjectDropped?.Invoke(this);

        myTranform.position = new Vector3(0, 10000, 0);

    }
    #endregion

}
