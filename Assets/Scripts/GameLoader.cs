﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : AsyncLoader
{
    public int sceneIndexToLoad = 1;
    public GameObject UIManagerPrefab = null;
    public LoadingScreen loadingScreen = null;
    private static int _sceneIndex = 1;
    private static GameLoader _instance;

    protected override void Awake()
    {
        Debug.Log("GameLoader Starting");

        // Safety Check
        if (_instance != null && _instance != this)
        {
            Debug.Log("A Duplicated instance of the GameLoader was found, and will be ignored. Only one instance is permitted.");
            Destroy(gameObject);
            return;
        }

        // Set reference to this instance
        _instance = this;

        // Make this object persistent
        DontDestroyOnLoad(gameObject);

        // Scene Index check
        if (sceneIndexToLoad < 0 || sceneIndexToLoad >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"Invalid Scene Index {sceneIndexToLoad} ... using defalut value of {_sceneIndex}");
        }
        else
        {
            _sceneIndex = sceneIndexToLoad;
        }

        // Setup system GameObject
        GameObject systemGO = new GameObject("[Services]");
        systemGO.tag = "Services";
        Transform systemParent = systemGO.transform;
        DontDestroyOnLoad(systemGO);

        loadingScreen.UpdateLoadingStep("Loading Game Systems");

        // Enqueue RoutineInfos to my queue;
        Enqueue(InitializeCoreSystems(systemParent), 50, UpdateCoreSystemsProgress);
        Enqueue(InitializeModularSystems(systemParent), 50, UpdateModularSystemsProgress);

        // Setthing the completion callback
        CallOnComplete(OnComplete);

    }

    private float _coreLoadTotalSteps = 200.0f;
    private float _coreLoadCurrentStep = 0.0f;

    private float UpdateCoreSystemsProgress()
    {
        return _coreLoadCurrentStep / _coreLoadTotalSteps;
    }

    private float _modularLoadTotalSteps = 300.0f;
    private float _modularLoadCurrentStep = 0.0f;

    private float UpdateModularSystemsProgress()
    {
        return _modularLoadCurrentStep / _modularLoadTotalSteps;
    }

    private IEnumerator InitializeCoreSystems(Transform systemsParent)
    {
        Debug.Log("Loading Core Systems...");

        _coreLoadCurrentStep = 1.0f;

        GameObject uiGO = GameObject.Instantiate(UIManagerPrefab);
        uiGO.transform.SetParent(systemsParent);
        var uiMngr = uiGO.GetComponent<UIManager>();
        ServiceLocator.Register<UIManager>(uiMngr);
        yield return null;

        for (int i = 0; i < _coreLoadTotalSteps - 1; ++i)
        {
            _coreLoadCurrentStep += 1.0f;
            yield return null;
        }
    }

    private IEnumerator InitializeModularSystems(Transform systemsParent)
    {
        Debug.Log("Loading Modular Systems...");

        // Fake loadingsteps for the loading bar
        for (int i = 0; i < _modularLoadTotalSteps; ++i)
        {
            _modularLoadCurrentStep += 1.0f;
            yield return null;
        }
    }

    private void OnComplete()
    {
        Debug.Log("GameLoader Completed");
        StartCoroutine(LoadInitialScene(_sceneIndex));
    }

    private IEnumerator LoadInitialScene(int index)
    {
        Debug.Log("GameLoader Starting Scene Load");
        var loadOp = SceneManager.LoadSceneAsync(index);

        loadingScreen.UpdateLoadingStep("Loading Scene: " + index.ToString());

        while (!loadOp.isDone)
        {
            loadingScreen.UpdateLoadingBar(loadOp.progress);
            yield return loadOp;
        }
    }
}

