using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task5 : MonoBehaviour, ITask
{
    public bool isResolved { get; set; }
    public GameObject NextButton { get; set; }
    [SerializeField] GameObject nextButton;

    private bool r30Checked;
    private bool r40Checked;
    private bool r50Checked;
    private float secondsDelay = 0;

    void Awake()
    {
        NextButton = nextButton;
    }
    
    public bool TaskCondition(ProgressManager obj) 
    {
        var calkManager = obj.PhysCalcManager.GetComponent<PhysCalculation>();
        var r = calkManager.R2;

        if(r == 30)
        {
            var ig = calkManager.progressedIG;

            if(Mathf.Abs(ig) <= 10)
            {
                r30Checked = true;
            }
        }

        if(r == 40)
        {
            var ig = calkManager.progressedIG;

            if(Mathf.Abs(ig) <= 10)
            {
                r40Checked = true;
            } 
        }

        if(r == 50)
        {
            var ig = 0f;
            secondsDelay += Time.deltaTime;

            if(secondsDelay > 1)
            {
                ig = calkManager.progressedIG;
                if(Mathf.Abs(ig) <= 10)
                {
                    r50Checked = true;
                }
            } 
        }

        return r30Checked && r40Checked && r50Checked;
    }

    public void ResetTask()
    {
        NextButton.GetComponent<Button>().interactable = false;
    }
}