using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// физические вычисления
public class PhysCalculation : MonoBehaviour
{
    [SerializeField] GameObject MultimeterG;
    [SerializeField] GameObject MultimeterMa;
    [SerializeField] GameObject MultimeterV;
    [SerializeField] GameObject AngleReohord;
    [SerializeField] GameObject AmpermeterG;
    [SerializeField] GameObject AmpermeterV;
    [SerializeField] GameObject AmpermeterMa;

    private Multimeter multimeterG;
    private Multimeter multimeterMa;
    private Multimeter multimeterV;

    public float R2;
    public float R3;
    private float R4;
    private float Rx;
    private float E;
    private float Rg;
    private float Ig;
    private float Ix;
    private float Ux;
    private float R0;
    public bool CircuetIsCorrect = false;
    public float progressedIG;

    void Start()
    {
        multimeterG = MultimeterG.GetComponent<Multimeter>();
        multimeterMa = MultimeterMa.GetComponent<Multimeter>();
        multimeterV = MultimeterV.GetComponent<Multimeter>();

        Rx = PlayerPrefs.GetFloat("Rx");
        E = PlayerPrefs.GetFloat("e");
        R0 = PlayerPrefs.GetFloat("R0");
        Rg = 1000;
    }
    
    void FixedUpdate()
    {
        var angle = int.Parse(AngleReohord.GetComponent<TextMeshPro>().text);
        R3 = 0.37037f * angle;
        var r4 = 100 - R3;
        R4 = (float)System.Math.Round(r4, 5);

        if(CircuetIsCorrect)
        {
            Ig = (((R2 * R3 - Rx * R4) * E) / (R2 * (R3 + R4))) / (Mathf.Abs(Rg * (1f + (Rx / R2)) + Rx + ((R3 * R4 * (Rx + R2)) / (R2 * (R3 + R4)))));
            progressedIG = (float)System.Math.Round((Ig * 1000)* 1000);
            Ix = (Ig * (Rg + R2) / R2) + ((R3 / R2) * ((E + Ig * R4) / (R3 + R4)));
            Ix = (float)System.Math.Round(Ix, 2);
            Ux = Ix * (Rx + R0);
            Ux = (float)System.Math.Round(Ux , 2);

            if(multimeterG.displayTurnedOn)
            {
                multimeterG.ChangeDisplayText(((float)System.Math.Round((Ig * 1000)* 1000)).ToString());
            }

            if(multimeterMa.displayTurnedOn)
            {
                multimeterMa.ChangeDisplayText(Ix.ToString());
            }

            if(multimeterV.displayTurnedOn)
            {
                multimeterV.ChangeDisplayText(Ux.ToString());
            }


        }
    }

    public void ResetCalculations() // cброс показаний мультиметра
    {
        CircuetIsCorrect = false;
        multimeterG.ChangeDisplayText("");
        AmpermeterG.GetComponent<MultimeterSpinButtonG>().Reset();
        multimeterV.ChangeDisplayText("");
        AmpermeterV.GetComponent<MultimeterSpinButtonV>().Reset();
        multimeterMa.ChangeDisplayText("");
        AmpermeterMa.GetComponent<MultimeterSpinButtonMa>().Reset();
    }

    public void SwitchResetCalculations()
    {
        multimeterG.ChangeDisplayText("0");
        multimeterV.ChangeDisplayText("0");
        multimeterMa.ChangeDisplayText("0");
    }

    public void SwitchResetCalculations1()
    {
        multimeterG.ChangeDisplayText("");
        multimeterV.ChangeDisplayText("");
        multimeterMa.ChangeDisplayText("");
    }
}