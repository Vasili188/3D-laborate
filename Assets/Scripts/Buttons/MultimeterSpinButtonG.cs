using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// кнопка мультиметра
public class MultimeterSpinButtonG : MonoBehaviour, ILMButton, IRMButton
{
    [SerializeField] GameObject MultimeterG;
    private Multimeter multimeterG;
    public int position = 0; // текущая позиция кнопки
    private int angle = 0; // угол поворота ручки

    void Start()
    {
        multimeterG = MultimeterG.GetComponent<Multimeter>();
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
        if(position == 4)
        {
            multimeterG.ChangeDisplayText("0");
            multimeterG.displayTurnedOn = true;
        }
        else
        {
            multimeterG.ChangeDisplayText("");
            multimeterG.displayTurnedOn = false;
        }
    }

    public void Reset() // сброс мультиметра
    {
        position = 0;
        multimeterG.displayTurnedOn = false;
        gameObject.transform.Rotate(0f, 0f, -angle);
        angle = 0;
    }

    public bool CheckTrue() // метод для проверки состояния мультиметра в других скриптах
    {
        return position == 4 ? true : false;
    }
}

