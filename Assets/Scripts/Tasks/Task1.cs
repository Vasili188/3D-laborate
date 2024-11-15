using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task1 : MonoBehaviour, ITask
{
    public bool isResolved { get; set; }
    public GameObject NextButton { get; set; }
    [SerializeField] GameObject nextButton;

    void Awake()
    {
        NextButton = nextButton;
    }

    public bool TaskCondition(ProgressManager obj) => 
        obj.PhysCalcManager.GetComponent<PhysCalculation>().CircuetIsCorrect;

    public void ResetTask()
    {
        NextButton.GetComponent<Button>().interactable = false;
    }
}