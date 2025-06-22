using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManagerBase : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] List<TaskBase> TaskList = new List<TaskBase>();
    public int TaskStartIndex = 0;
    public int ModuleEndIndex = -1;
    [Space]
    [Header("Settings")]
    public bool isSequencialFlow = true;
    public bool isChapterInProgress = false;
    public bool b_starWithWelcomeTask = true;

    [Header("ref")]
    public TaskBase welcomeTask;
    public Guide guide;
    public TaskDatatble taskDatatble;
    [SerializeField] GameObject taskUi;
    [SerializeField] TMP_Text instructionText;


    private int curentTaskIndex = 0;




    void Start()
    {
        taskUi.SetActive(false);
    }

    [NaughtyAttributes.Button]
    public void TaskManagerInit()
    {
        if (b_starWithWelcomeTask && welcomeTask)
        {
            welcomeTask.AC_OnTaskFinished += WelcomeTaskDone;
            welcomeTask.ActivateTask(this);
        }
        else
        {
            ChapterStart();
        }
    }



    void WelcomeTaskDone()
    {
        welcomeTask.AC_OnTaskFinished -= WelcomeTaskDone;
        ChapterStart();
    }







    public void ChapterStart()
    {
        curentTaskIndex = TaskStartIndex;

        if (!isChapterInProgress) EventManager.OnChapterStartEvent?.Invoke();
        isChapterInProgress = true;

        if (isSequencialFlow)
        {
            InitateTaskForSequencital();
        }
    }

    public void ChapterEnd()
    {
        if (!isChapterInProgress) EventManager.OnChapterEndEvent?.Invoke();
        isChapterInProgress = false;

        //Play end audio
        if(taskDatatble.Table.Count>ModuleEndIndex)SetTaskUidatabyIndex(ModuleEndIndex);
    }

    void InitateTaskForSequencital()
    {
        if (TaskList.Count > curentTaskIndex)
        {
            TaskList[curentTaskIndex].AC_OnTaskFinished += TaskFinished;
            TaskList[curentTaskIndex].ActivateTask(this);
        }
        else
        {
            //all task are complete now finish the module 
            ChapterEnd();

        }
    }

    void TaskFinished()
    {
        TaskList[curentTaskIndex].AC_OnTaskFinished -= TaskFinished;
        //for some reason if task is not done force deactivate it
        if (!TaskList[curentTaskIndex].b_TaskDone)
        {
            TaskList[curentTaskIndex].OnTaskDeactivate();
        }

        curentTaskIndex++;
        if (isSequencialFlow) InitateTaskForSequencital();
    }



    public void SetTaskUidatabyIndex(int index)
    {
        if (taskDatatble.Table.Count > index)
        {
            taskUi.SetActive(true);
            guide.PlayDialouge(taskDatatble.Table[index].audioClip);
            instructionText.text = taskDatatble.Table[index].text;
        }
    }
}
