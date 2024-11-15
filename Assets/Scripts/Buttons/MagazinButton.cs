using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// кнопка магазина сопротивлений
public class MagazinButton : MonoBehaviour, ILMButton, IRMButton
{
    [SerializeField] GameObject PhysCalculations;
    [SerializeField] GameObject MagazinAngle;
    private PhysCalculation manager;
    private float angle = 0; // угол наклона ручки
    private float r = 10; // значение потенциометра


    void Start()
    {
        manager = PhysCalculations.GetComponent<PhysCalculation>();
        manager.R2 = r;
    }

    public void LMBInteraction()
    {
        if(angle < 200)
        {
            transform.Rotate(0f, 50f, 0f);
            angle += 50;
            r += 10;
            manager.R2 = r;
        }
    }

    public void RMBInteraction()
    {
        if(angle > 0)
        {
            transform.Rotate(0f, -50f, 0f);
            angle -= 50;
            r -= 10;
            manager.R2 = r;
        }
    }

    public void Reset() // Сброс потенциометра
    {
        angle = 0 - angle;
        transform.Rotate(0f, angle, 0f);
        angle = 0;
        r = 10;
        manager.R2 = r;
    }
}
