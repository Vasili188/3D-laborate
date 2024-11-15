using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// генерация случайных значений пользователя
public class PlayerValues : MonoBehaviour
{
    [SerializeField] Text Values; 

    void Start()
    {
        UserInfo.Login = "admin";
        // если нужна генерация значений при каждом перезапуске, нужно удалить if(), оставив саму генерацию
        //if(PlayerPrefs.GetFloat("e") == 0)
        //{
            PlayerPrefs.SetFloat("e", (float)System.Math.Round(Random.Range(4.75f, 5.25f), 3)); 
            PlayerPrefs.SetFloat("Rx", (float)System.Math.Round(Random.Range(22.5f, 27.5f), 3));
            PlayerPrefs.SetFloat("R0", (float)System.Math.Round(Random.Range(0.1f, 0.3f), 3));
        //}

        var e  = PlayerPrefs.GetFloat("e");
        var Rx  = PlayerPrefs.GetFloat("Rx");
        var R0  = PlayerPrefs.GetFloat("R0");

        Values.text = $"e = {e} \nRx ={Rx} \nR0 = {R0}";

        Debug.Log("Rx  :  " + PlayerPrefs.GetFloat("Rx"));
        Debug.Log("R0  :  " + PlayerPrefs.GetFloat("R0"));
        Debug.Log("e  :  " + PlayerPrefs.GetFloat("e"));
    }
}
