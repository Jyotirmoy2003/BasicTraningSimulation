using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Needed for LINQ

public class GameManager : MonoBehaviour
{
    [SerializeField] List<IInteractable> interactBases = new List<IInteractable>();
    [SerializeField] float initTime = 5f;
    void Start()
    {
        // Find all MonoBehaviours and filter for IInteractable
        var interactables = FindObjectsOfType<MonoBehaviour>(true)  // true = include inactive
                            .OfType<IInteractable>();

        interactBases.AddRange(interactables);
        Invoke(nameof(Init), initTime);
    }

    void Init()
    {
        foreach (IInteractable item in interactBases) item.Init();

        Invoke(nameof(StartSimulation), 2f);
    }

    void StartSimulation()
    {
        FindObjectOfType<TaskManagerBase>().TaskManagerInit();
    }

    
}
