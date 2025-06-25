using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Init();
    public void OnPointerEnter();
    public void OnPointerExit();
    public void OnInteract();    
    
}
