using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// экран мультиметра
public class Multimeter : MonoBehaviour
{
    [SerializeField] GameObject Display;
    public bool displayTurnedOn;

    public void ChangeDisplayText(string text)
    {
        Display.GetComponent<TextMeshPro>().text = text;
    }
}
