using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeInteraction : MonoBehaviour, IFocusable, IInputClickHandler
{
    public float Speed;
    public Vector3 ScaleChange;

    public TextMesh feedback;

    private bool Rotating;

    // Use this for initialization
    void Start()
    {
        feedback.text = "Started";
    }

    // Update is called once per frame
    void Update()
    {
        if (Rotating)
            transform.Rotate(Vector3.up * Time.deltaTime * Speed);
    }

    public void OnFocusEnter()
    {
        feedback.text = "Focus on object.";
        Rotating = true;
    }

    public void OnFocusExit()
    {
        feedback.text = "Focus off object.";
        Rotating = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        feedback.text = "Tapped on object.";
        transform.localScale += ScaleChange;
    }
}