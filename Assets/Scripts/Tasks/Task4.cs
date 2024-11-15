using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task4 : MonoBehaviour, ITask
{
    public bool isResolved { get; set; }
    public GameObject NextButton { get; set; }
    [SerializeField] GameObject nextButton;
    private float secondsDelay = 0;

    void Awake()
    {
        NextButton = nextButton;
    }
    
    public bool TaskCondition(ProgressManager obj)
    {
        var calkManager = obj.PhysCalcManager.GetComponent<PhysCalculation>();
        var r = calkManager.R2; 

        if(r == 20)
        {
            var ig = 0f;
            secondsDelay += Time.deltaTime;

            if(secondsDelay > 1)
            {
                ig = calkManager.progressedIG;
                secondsDelay = 0;

                return Mathf.Abs(ig) <= 10;
            } 
        }
        
        return false;

        // var ig = calkManager.progressedIG;
        // return r == 20 && Mathf.Abs(ig) <= 10;
    }

    public void ResetTask()
    {
        NextButton.GetComponent<Button>().interactable = false;
    }
}