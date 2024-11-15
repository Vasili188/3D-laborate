using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//информация о пользователе
public static class UserInfo
{
    private static bool isLogged;
    public static bool IsLogged
    {
        get
        {
            return isLogged;
        }
    }

    private static string login;
    public static string Login
    {
        get
        {
            return login;
        }
        set
        {
            login = value;
            isLogged = true;
        }

    }

    //номер ЛР
    private static string lab = "lab12";
    public static string Lab
    {
        get
        {
            return lab;
        }
    }
}
