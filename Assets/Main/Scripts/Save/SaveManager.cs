using UnityEngine;
using System;

public class SaveManager : Singleton<SaveManager>
{
    public enum TypeOfKey { INTEGER,FLOAT,STRING }
    public TypeOfKey enKeyType;

    // On start scene
    public bool isBanSave = false;

    // Names for save
    public string nameProject;

    // Working with keys
    public string key;
    public string value;
    
    private static bool isDeleteObject = false;

	private void Awake ()
    {
        if (!isDeleteObject)
        {
            isDeleteObject = true;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }

    public static string GetKeyProject()
    {
        return Instance.nameProject;
    }


    /// <summary>
    /// Удалить ключ
    /// </summary>
    /// <param name="_key"></param>
    public static void DeleteKey(string _key)
    {
        PlayerPrefs.DeleteKey(GetKeyProject() + _key);
    }

    public void EditorDeleteKey(string _key)
    {
        if (PlayerPrefs.HasKey(nameProject + _key))
        {
            PlayerPrefs.DeleteKey(nameProject + _key);
            print("Ключ удалён");
            return;
        }
        else
        {
            print("Такого ключа не существует");
        }
    }

    public void EditorAddKey<T>(string _key, T _value)
    {
        if (typeof(T) == typeof(int))
        {
            int? tempValue = _value as int?;
            PlayerPrefs.SetInt(nameProject + _key, (int)tempValue);
            print("Ключ создан");
        }

        if (typeof(T) == typeof(float))
        {
            float? tempValue = _value as float?;
            PlayerPrefs.SetFloat(nameProject + _key, (float)tempValue);
            print("Ключ создан");
        }

        if (typeof(T) == typeof(string))
        {
            string tempValue = _value as string;
            PlayerPrefs.SetString(nameProject + _key, tempValue);
            print("Ключ создан");
        }
    }

    public void EditorGetKey<T>(string _key)
    {
        if (!PlayerPrefs.HasKey(nameProject + _key))
        {
            print("Такого ключа не существует");
            return;
        }
        if (typeof(T) == typeof(int))
        {
            print(PlayerPrefs.GetInt(nameProject + _key));
        }
        else if (typeof(T) == typeof(float))
        {
            print(PlayerPrefs.GetFloat(nameProject + _key));
        }
        else if (typeof(T) == typeof(string))
        {
            print(PlayerPrefs.GetString(nameProject + _key));
        }
    }

    /// <summary>
    /// Добавить ключ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_key"></param>
    /// <param name="_value"></param>
    public static void SetKey<T>(string _key,T _value)
    {
        if (Instance.isBanSave)
            return;

        if(typeof(T) == typeof(int))
        {
            int? tempValue = _value as int?;
            PlayerPrefs.SetInt(GetKeyProject() + _key, (int)tempValue);
        }

        if (typeof(T) == typeof(float))
        {
            float? tempValue = _value as float?;
            PlayerPrefs.SetFloat(GetKeyProject() + _key, (float)tempValue);
        }

        if (typeof(T) == typeof(string))
        {
            string tempValue = _value as string;
            PlayerPrefs.SetString(GetKeyProject() + _key, tempValue);
        }
    }

    /// <summary>
    /// Проверка наличия ключа
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public static bool HasKey(string _key)
    {
        return PlayerPrefs.HasKey(GetKeyProject() + _key);
    }

    public static int GetKey(string _key, int _defaultValue)
    {
        return PlayerPrefs.GetInt(GetKeyProject() + _key, _defaultValue);
    }

    public static float GetKey(string _key, float _defaultValue)
    {
        return PlayerPrefs.GetFloat(GetKeyProject() + _key, _defaultValue);
    }

    public static string GetKey(string _key, string _defaultValue)
    {
        return PlayerPrefs.GetString(GetKeyProject() + _key, _defaultValue);
    }
}
