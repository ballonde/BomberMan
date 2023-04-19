using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http.Headers;
using UnityEngine;

public class SavingPatern<T>
{
    public T element;
    public string IDName;
}


public class PatternList<T> : List<SavingPatern<T>>
{


    public SavingPatern<T> GetPaternByID(string id)
    {
        return this.Find((t) => t.IDName == id);
    }

    public T GetElementByID(string id)
    {
        return GetPaternByID(id).element;
    }

    public void CreateElement(T newElement, string newID)
    {
        var pattern = new SavingPatern<T>();
        pattern.IDName = newID;
        pattern.element = newElement;
        Add(pattern);
    }

    public void SuppElementByID(string id){
        var pattern = GetPaternByID(id);
        Remove(pattern);
    }
}



public class SaveBetweenscene : MonoBehaviour
{
    public PatternList<int> globalInt;
    public PatternList<string> globalString;
    public PatternList<float> globalFloat;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        globalInt = new PatternList<int>();
        globalFloat = new PatternList<float>();
        globalString = new PatternList<string>();
    }

    public static SaveBetweenscene GetGlobalThis()
    {
        return FindObjectOfType<SaveBetweenscene>();
    }
}