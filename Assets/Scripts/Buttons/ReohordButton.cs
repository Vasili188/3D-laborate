using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReohordButton : MonoBehaviour, IRMButton, ILMButton, ILMButtonRush, IRMButtonRush
{
    [SerializeField] GameObject PhysCalculations;
    [SerializeField] GameObject AngleReohord;
    
    float secondsDelay; // таймер    
    public float txt = 135; // значение реохорда

    public void LMBInteraction() // метод вызывыаемый одиночным щелчком левой кнопки мыши
    {
        if(txt < 270)
        {
            transform.Rotate(0f, 0f, 2f);
            txt++;
            AngleReohord.GetComponent<TextMeshPro>().text = txt.ToString();
        }
    }

    public void LMBInteractionRush() // метод вызываемый удержанием левой кнопки мыши
    {
        if(txt < 270)
        {
            transform.Rotate(0f, 0f, 2f);
            secondsDelay += 4f * Time.deltaTime; // отсчёт времени после каждого прибавленного значения, таким образом контролируется скорость вращения реохорда
            if(secondsDelay > 0.1f)
            {
                txt++;
                AngleReohord.GetComponent<TextMeshPro>().text = txt.ToString();
                secondsDelay = 0;
            }
        }
    }

    public void RMBInteraction() // метод вызывыаемый одиночным щелчком правой кнопки мыши
    {
        if(txt > 0)
        {
            transform.Rotate(0f, 0f, -2f);
            txt--;
            AngleReohord.GetComponent<TextMeshPro>().text = txt.ToString();
        }
    }

    public void RMBInteractionRush() // метод вызываемый удержанием правой кнопки мыши
    {
        if(txt > 0)
        {
            transform.Rotate(0f, 0f, -2f);
            secondsDelay += 4f * Time.deltaTime; // отсчёт времени после каждого прибавленного значения, таким образом контролируется скорость вращения реохорда
            if(secondsDelay > 0.1f)
            {
                txt--;
                AngleReohord.GetComponent<TextMeshPro>().text = txt.ToString();
                secondsDelay = 0;
            }
        }
    }
}
