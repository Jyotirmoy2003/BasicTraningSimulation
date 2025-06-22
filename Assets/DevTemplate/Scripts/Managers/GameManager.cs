using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<InteractBase> interactBases = new List<InteractBase>();
    [SerializeField] float initTime = 5f;
    void Start()
    {
        // Find all InteractableBase in the scene
        interactBases.AddRange(FindObjectsOfType<InteractBase>());
        Invoke(nameof(Init), initTime);
    }

    void Init()
    {
        foreach (InteractBase item in interactBases) item.Init();

        Invoke(nameof(StartSimulation), 2f);
    }

    void StartSimulation()
    {
        FindObjectOfType<TaskManagerBase>().TaskManagerInit();
    }

    
}
