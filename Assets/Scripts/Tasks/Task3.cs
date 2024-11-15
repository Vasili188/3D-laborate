using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task3 : MonoBehaviour, ITask
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
        var calkManager = obj.PhysCalcManager.GetComponent<PhysCalculation>();
        var ig = calkManager.progressedIG;
        var r = calkManager.R2;

        return r == 10 && Mathf.Abs(ig) <= 10;
    }

    public void ResetTask()
    {
        NextButton.GetComponent<Button>().interactable = false;
    }
}