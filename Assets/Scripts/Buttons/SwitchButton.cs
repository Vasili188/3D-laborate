using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ручка ключа
public class SwitchButton : MonoBehaviour, ILMButton
{
    [SerializeField] GameObject CircuitManager;
    [SerializeField] GameObject CalcManager;
    [SerializeField] GameObject Message;
    [SerializeField] GameObject MultimeterG;
    [SerializeField] GameObject MultimeterV;
    [SerializeField] GameObject MultimeterMa;
    [SerializeField] GameObject MultimetrG;
    [SerializeField] GameObject MultimetrV;
    [SerializeField] GameObject MultimetrMa;

    private Multimeter multimeterG;
    private Multimeter multimeterV;
    private Multimeter multimeterMa;
    
    bool turned = false; // false - ручка поднята, true - опущена

    void Start()
    {
        multimeterG = MultimeterG.GetComponent<Multimeter>();
        multimeterV = MultimeterV.GetComponent<Multimeter>();
        multimeterMa = MultimeterMa.GetComponent<Multimeter>();
        transform.Rotate(50f, 0f, 0f);
    }

    public void LMBInteraction()
    {
        if(turned)
        {
            // если ручка опущена
            if (CalcManager.GetComponent<PhysCalculation>().CircuetIsCorrect == true)  
                CalcManager.GetComponent<PhysCalculation>().CircuetIsCorrect = false; 
            if(MultimetrG.GetComponent<MultimeterSpinButtonG>().position == 4) // если мультметр включен парвильно загорается 0
                multimeterG.ChangeDisplayText("0");
            if(MultimetrV.GetComponent<MultimeterSpinButtonV>().position == 17) 
                multimeterV.ChangeDisplayText("0");
            if(MultimetrMa.GetComponent<MultimeterSpinButtonMa>().position == 7) 
                multimeterMa.ChangeDisplayText("0");
            transform.Rotate(50f, 0f, 0f);
            turned = false;

        } 
        else
        {
            // если ручка поднята
            if (CalcManager.GetComponent<PhysCalculation>().CircuetIsCorrect == true) 
            {         
                // если цепь собрана правильно    
                multimeterG.displayTurnedOn = true;
                multimeterV.displayTurnedOn = true;
                multimeterMa.displayTurnedOn = true;
                turned = true;
                transform.Rotate(-50f, 0f, 0f);
                return;
            }
            // если цепь собрана неправильно 
            if(MultimetrG.GetComponent<MultimeterSpinButtonG>().position == 4) 
                multimeterG.ChangeDisplayText("0");
            if(MultimetrV.GetComponent<MultimeterSpinButtonV>().position == 17) 
                multimeterV.ChangeDisplayText("0");
            if(MultimetrMa.GetComponent<MultimeterSpinButtonMa>().position == 7) 
                multimeterMa.ChangeDisplayText("0");
            transform.Rotate(-50f, 0f, 0f);
            turned = true;
            CheckCircuitCorrectness();
        }
    }

    void CheckCircuitCorrectness() //проверка правильности сборки цепи
    {
        var manager = CircuitManager.GetComponent<CircuitManager>();
        
        foreach (var e in manager.relationsDictionary)
        {
            if(!manager.LogicFlag)
                if(!e.Value)
                {
                    StartCoroutine(ShowMessage());
                    return;
                }
        }

        CalcManager.GetComponent<PhysCalculation>().CircuetIsCorrect = true;
    }

    public void Reset() // сброс ключа
    {
        if(turned)
        {
            transform.Rotate(50f, 0f, 0f);
            turned = false;
        }
        
    }

    IEnumerator ShowMessage() // сообщение о неправильной сборке, при замыкании ключа
    {
        Message.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        Message.SetActive(false);
    }
}
