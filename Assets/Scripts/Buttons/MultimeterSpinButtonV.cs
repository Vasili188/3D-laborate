using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultimeterSpinButtonV : MonoBehaviour, ILMButton, IRMButton
{
    [SerializeField] GameObject MultimeterV;
    private Multimeter multimeterV;
    public int position = 0; // текущая позиция кнопки
    private int angle = 0; // угол поворота ручки

    void Start()
    {
        multimeterV = MultimeterV.GetComponent<Multimeter>();
    }

    public void LMBInteraction()
    {
        gameObject.transform.Rotate(0f, 0f, 18f);

        angle += 18;
        
        if(position == 19)
        {
            position = 0;
        }
        else
        {
            position++;
        }

        CheckAngle();
    }

    public void RMBInteraction()
    {
        gameObject.transform.Rotate(0f, 0f, -18f);

        angle -= 18;

        if(position == 0)
        {
            position = 19;
        }
        else
        {
            position--;
        }

        CheckAngle();
    }

    void CheckAngle()
    {
        if(position == 17)
        {
            multimeterV.ChangeDisplayText("0");
            multimeterV.displayTurnedOn = true;
        }
        else
        {
            multimeterV.ChangeDisplayText("");
            multimeterV.displayTurnedOn = false;
        }
    }

    public void Reset() // сброс мультиметра
    {
        position = 0;
        multimeterV.displayTurnedOn = false;
        gameObject.transform.Rotate(0f, 0f, -angle);
        angle = 0;
    }

    public bool CheckTrue() // метод для проверки состояния мультиметра в других скриптах
    {
        return position == 17 ? true : false;
    }
}