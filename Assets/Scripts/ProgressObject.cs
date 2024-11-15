using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//контроль над всеми тасками
public class ProgressObject : MonoBehaviour
{
    public List<GameObject> tasks;

    private int currentTask;

    [SerializeField] GameObject showTaskButton;

    void Update()
    {
        if(tasks[currentTask].TryGetComponent(out ITask task) && task.isResolved)
        {
            task.NextButton.GetComponent<Button>().interactable = true;
        }
    }

    public void NextPage()
    {
        tasks[currentTask].SetActive(false);
        tasks[currentTask + 1].SetActive(true);
        currentTask++;
    }

    public void PreviusPage()
    {
        tasks[currentTask].SetActive(false);
        tasks[currentTask - 1].SetActive(true);
        currentTask--;
    }

    public void StartPage()
    {
        tasks[currentTask].SetActive(false);
        tasks[0].SetActive(true);
        currentTask = 0;
    }

    public void HideTask()
    {
        gameObject.SetActive(false);
        showTaskButton.SetActive(true);
    }

    public void ShowTask()
    {
        showTaskButton.SetActive(false);
        gameObject.SetActive(true);
    }
}
