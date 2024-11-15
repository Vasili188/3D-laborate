using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task2 : MonoBehaviour, ITask
{
    public bool isResolved { get; set; }
    public GameObject NextButton { get; set; }
    [SerializeField] GameObject nextButton;

    void Awake()
    {
        NextButton = nextButton;
    }
    
    public bool TaskCondition(ProgressManager obj)
    {
        var multimV = obj.MultimeterV.GetComponent<Multimeter>().displayTurnedOn;
        var multimMa = obj.MultimeterMa.GetComponent<Multimeter>().displayTurnedOn;
        var multimG = obj.MultimeterG.GetComponent<Multimeter>().displayTurnedOn;

        return multimV && multimMa && multimG;
    }

    public void ResetTask()
    {
        NextButton.GetComponent<Button>().interactable = false;
    }
}