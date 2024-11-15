using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    [SerializeField] GameObject Message;

    public void ShowMessage()
    { 
        if(Message.activeSelf)
        {
            CloseMessage();
        }
        else
        {
            Message.SetActive(true);
        }
    }

    public void CloseMessage()
    {
        Message.SetActive(false);
    }
}
