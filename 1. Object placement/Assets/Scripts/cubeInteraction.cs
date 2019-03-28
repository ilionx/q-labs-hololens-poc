using HoloToolkit.Unity.InputModule;
using Microsoft.Azure.SpatialAnchors;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.WSA;

public class cubeInteraction : MonoBehaviour, IFocusable, IInputClickHandler
{
    public TextMesh feedback;
    public List<GameObject> prefab;


    private AnchorLocateCriteria anchorLocateCriteria = null;

    private bool nextAnchorFound = false;
    private bool userInitiatesNextStep = false;

    private CloudSpatialAnchorSession cloudSpatialAnchorSession = null;
    private List<string> AnchorIdsToLocate = new List<string>();

    private List<GameObject> gameObjects = new List<GameObject>();

    private List<string> chapterAnchors = new List<string>();
    private int step = 0;
    

    private JArray returnJson;


    /// <summary>
    /// These events are not wired to the actual CloudSpatialAnchorSession, but will
    /// act as a proxy to forward events from the CloudSpatialAnchorSession to the
    /// subscriber.
    /// </summary>
    public event AnchorLocatedDelegate OnAnchorLocated;
    public event LocateAnchorsCompletedDelegate OnLocateAnchorsCompleted;
    public event SessionErrorDelegate OnSessionError;
    public event SessionUpdatedDelegate OnSessionUpdated;
    public event OnLogDebugDelegate OnLogDebug;

    // Use this for initialization
    void Start()
    {
        feedback.text = "Tap this cube to start Onboarding";
        SetChapterAnchor();
        CreateNewCloudSession();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextAnchorFound && userInitiatesNextStep)
        {
            //this.gameObject.SetActive(false);
        }
    }

    public void OnFocusEnter()
    {
        // Nothing yet
    }

    public void OnFocusExit()
    {
        // Feedback.text = "Focus again after clicking.";
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        foreach (GameObject x in gameObjects)
        {
            x.SetActive(false);
        }
        GoToNextLocation();
    }

    private void SetChapterAnchor()
    {
        List<string> chapterAnchorsFromBackend = new List<string> { "Set spatial anchor id" };
        foreach (string anchorId in chapterAnchorsFromBackend)
        {
            chapterAnchors.Add(anchorId);
        }
    }

    private void GoToNextLocation()
    {
        userInitiatesNextStep = true;

        AnchorIdsToLocate = new List<string>
        {
            chapterAnchors[step]
        };
        step += 1;

        anchorLocateCriteria = new AnchorLocateCriteria
        {
            BypassCache = true,
            Identifiers = AnchorIdsToLocate.ToArray(),
            Strategy = LocateStrategy.AnyStrategy
        };
        cloudSpatialAnchorSession.CreateWatcher(anchorLocateCriteria);
        feedback.text = "Watching";
    }

    private void CreateNewCloudSession()
    {
        cloudSpatialAnchorSession = new CloudSpatialAnchorSession();
        string accountId = "set this";
        string accountKey = "set this";
        cloudSpatialAnchorSession.Configuration.AccountId = accountId.Trim();
        cloudSpatialAnchorSession.Configuration.AccountKey = accountKey.Trim();

        cloudSpatialAnchorSession.LogLevel = SessionLogLevel.Information;

        cloudSpatialAnchorSession.OnLogDebug += CloudSpatialAnchorSession_OnLogDebug;
        cloudSpatialAnchorSession.SessionUpdated += CloudSpatialAnchorSession_SessionUpdated;
        cloudSpatialAnchorSession.AnchorLocated += CloudSpatialAnchorSession_AnchorLocated;
        cloudSpatialAnchorSession.LocateAnchorsCompleted += CloudSpatialAnchorSession_LocateAnchorsCompleted;
        cloudSpatialAnchorSession.Error += CloudSpatialAnchorSession_Error;
        feedback.text = "Starting the session!";

        cloudSpatialAnchorSession.Start();
        feedback.text = "Look for next";
    }

    private void CloudSpatialAnchorSession_LocateAnchorsCompleted(object sender, LocateAnchorsCompletedEventArgs args)
    {
        OnLocateAnchorsCompleted?.Invoke(sender, args);
    }

    private void CloudSpatialAnchorSession_AnchorLocated(object sender, AnchorLocatedEventArgs args)
    {
        OnAnchorLocated?.Invoke(sender, args);
        UnityEngine.WSA.Application.InvokeOnAppThread(
            () =>
            {
                GameObject cube = this.gameObject;

                var oldWorldAnchor = cube.GetComponent<WorldAnchor>();
                // remove any world anchor component from the game object so that it can be moved
                if (oldWorldAnchor)
                {
                    DestroyImmediate(oldWorldAnchor);
                }

                cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                
                var worldAnchor = cube.AddComponent<WorldAnchor>();

                
                worldAnchor.SetNativeSpatialAnchorPtr(args.Anchor.LocalAnchor);

                feedback.transform.position = cube.transform.position;

                cube.name = args.Identifier;

            },
            false
        );
    }

    private void CloudSpatialAnchorSession_Error(object sender, SessionErrorEventArgs args)
    {
        OnSessionError?.Invoke(sender, args);
    }

    private void CloudSpatialAnchorSession_SessionUpdated(object sender, SessionUpdatedEventArgs args)
    {
        OnSessionUpdated?.Invoke(sender, args);
    }

    private void CloudSpatialAnchorSession_OnLogDebug(object sender, OnLogDebugEventArgs args)
    {
        OnLogDebug?.Invoke(sender, args);
    }
}
