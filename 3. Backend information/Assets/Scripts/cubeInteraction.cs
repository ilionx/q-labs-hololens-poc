using HoloToolkit.Unity.InputModule;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class cubeInteraction : MonoBehaviour, IFocusable, IInputClickHandler
{
    public TextMesh feedback;
    
    // Use this for initialization
    void Start()
    {
        feedback.text = "Started";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnFocusEnter()
    {
    }

    public void OnFocusExit()
    {
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        feedback.text = "Tapped on object.";

        string url = @"https://pokeapi.co/api/v2/pokemon/6/";

        StartCoroutine(GetCall(url));

    }
    IEnumerator GetCall(string url)
    {
        feedback.text = "Getting from API";
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        
        var parsedObject = JObject.Parse(uwr.downloadHandler.text);
        
        feedback.text = parsedObject["name"].ToString();
    }
}