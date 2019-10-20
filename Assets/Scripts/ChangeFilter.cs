using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFilter : MonoBehaviour
{
    public TwitterScript ts;

    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitName);
        input.onEndEdit = se;
    }

    public void SubmitName(string arg0)
    {
        ts = GameObject.Find("Environment").GetComponent<TwitterScript>();
        Debug.Log(arg0);
        Debug.Log("Suubmit");
        ts.FilterBtn(arg0);
    }
}

