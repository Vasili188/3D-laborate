using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// активация кнопок, поддержка работы кнопок, логика
public class ButtonsRayCast : MonoBehaviour
{
    public Camera cameraTR;
    private RaycastHit[] hits;
    float secondsDelay = 0; // таймер
    bool shortClick = true; // переменная чтобы различать короткие клики и удержание кнопки

    public void Update()
    {
        if(UserInfo.IsLogged)
        {
            if(Input.GetMouseButton(0)) // удержание ЛКМ
            {
                if(shortClick) InteractButton(0);
                secondsDelay += 2f * Time.deltaTime; // отсчёт врмемени удержания ЛКМ
                if(secondsDelay > 2) // при прошествии около 2с кнопка становится активной
                    InteractButton(2); 
                shortClick = false;
            }

            if(Input.GetMouseButtonUp(0)) // одиночный клик ЛКМ
            {
                shortClick = true;
                secondsDelay = 0;
            }
            
            if(Input.GetMouseButton(1)) // удержание ПКМ
            {
                if(shortClick) InteractButton(1);
                secondsDelay += 2f * Time.deltaTime; // отсчёт врмемени удержания ЛКМ
                if(secondsDelay > 2) // при прошествии около 2с кнопка становится активной
                    InteractButton(3);
                shortClick = false;
            }

            if(Input.GetMouseButtonUp(1)) // одиночный клик ПКМ
            {
                shortClick = true;
                secondsDelay = 0;
            }
        }
    }

    void InteractButton(int click)
    {
        hits = Physics.RaycastAll(cameraTR.ScreenPointToRay(Input.mousePosition), 15);
        foreach(var hit in hits)
        {
            var collider = hit.collider;
            
            if(click == 0)
                if(collider.TryGetComponent(out ILMButton leftButton))
                {
                    leftButton.LMBInteraction();
                    break;
                }

            if(click == 1)
                if(collider.TryGetComponent(out IRMButton rightButton))
                {
                    rightButton.RMBInteraction();
                    break;
                } 

            if(click == 2)
                if(collider.TryGetComponent(out ILMButtonRush leftButtonRush))
                {
                    leftButtonRush.LMBInteractionRush();
                    break;
                }

            if(click == 3)
                if(collider.TryGetComponent(out IRMButtonRush rightButtonRush))
                {
                    rightButtonRush.RMBInteractionRush();
                    break;
                }
        }
    }
}