using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// подсветка слотов
public class Outline : MonoBehaviour
{
    float a = 0;
    bool flag = true;
    [Range(10, 40)]
    [SerializeField] int flickerSpeed = 10;

    bool startOutline = false;
    
    void Update()
    {
        if(startOutline)
        {
            if(a <= 0)
                flag = false;
            if(a >= 1)
                flag = true;
            if(flag)
                a -= 0.1f * Time.deltaTime * flickerSpeed;
            else
            a += 0.1f * Time.deltaTime * flickerSpeed; 
            
            SetBrightness(a);
        }
    }

    private void SetBrightness(float a)
    {
        gameObject.GetComponent<MeshRenderer>().material.color =
            new Color(Color.green.r, Color.green.g, Color.green.b, a);
    }

    public void SetRedColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = 
            new Color(Color.red.r, Color.red.g, Color.red.b);
        Invoke("StopOutline", 1.2f);
    }

    public void StartOutline()
    {
        startOutline = true;
    }

    public void StopOutline()
    {
        startOutline = false;
        SetBrightness(0f);
    }
}
