using UnityEngine;

public class T_SimpleDragAndDrop : TaskBase
{
    public PickupBase GOref;
    [SerializeField] PlaceTrigger placeTrigger;
    [SerializeField] GameObject dummyObject;
    [SerializeField] GameObject realObject;
    private bool audioFinshed = false;

    void Start()
    {
        realObject.SetActive(false);
        dummyObject.SetActive(false);
    }
    public override void OnTaskActivate()
    {
        base.OnTaskActivate();
        if (!GOref.picked) taskManager.SetTaskUidatabyIndex(TaskNo);
        else ObjectPicked();

        GOref.HighlightPickup(true);
        GOref.canPick = true;
        GOref.AC_OnObjectGrabbed += ObjectPicked;
        GOref.AC_OnObjectDropped += ObjectDropped;

    }

    public override void OnTaskDeactivate()
    {
        base.OnTaskDeactivate();
        GOref.HighlightPickup(false);
        TaskComplete();
    }


    void ObjectPicked()
    {
        

        if (audioFinshed)
        {
            dummyObject.SetActive(true);
            placeTrigger.AC_TriggerInteracted += ObjectPlacedOnTrigger;
            placeTrigger.EnableTrigger(true);
        }
        else
        {
            taskManager.SetTaskUidatabyIndex(TaskNo + 1);
            taskManager.guide.AC_OnAudioFinished += AudioFinshed;
        }
        

    }

    void ObjectDropped()
    {
        dummyObject.SetActive(false);
        placeTrigger.AC_TriggerInteracted -= ObjectPlacedOnTrigger;
        placeTrigger.EnableTrigger(false);
    }

    void AudioFinshed()
    {
        taskManager.guide.AC_OnAudioFinished -= AudioFinshed;
        taskManager.SetTaskUidatabyIndex(TaskNo + 2);
        audioFinshed = true;
        if (GOref.picked)
        {
            dummyObject.SetActive(true);
            placeTrigger.AC_TriggerInteracted += ObjectPlacedOnTrigger;
            placeTrigger.EnableTrigger(true);
        }
    }



    void ObjectPlacedOnTrigger(PickupBase placedObject)
    {
        if (placedObject == GOref)
        {
            GOref.AC_OnObjectDropped -= ObjectDropped;
            GOref.AC_OnObjectGrabbed -= ObjectPicked;
            GOref.RemoveFromPlayArea();

            dummyObject.SetActive(false);
            placeTrigger.AC_TriggerInteracted -= ObjectPlacedOnTrigger;
            placeTrigger.EnableTrigger(false);

            realObject.SetActive(true);

            TaskDeactive();
        }
    }

}
