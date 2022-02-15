using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNameConstants
{
    public static string TitleScene = "TitleScene";
    public static string LoadingScene = "LoadingScene";
    public static string InGame = "InGame";
}

public class SceneController : MonoBehaviour
{
    private static SceneController instance = null;

    public static SceneController Instance
    {
        get
        {
            if (instance == null)
            {
                //���� ���� Ŭ�������� ���� �̸��� ���� ������Ʈ�� ���� attach
                GameObject go = GameObject.Find("SceneController");
                if (go == null)
                {
                    go = new GameObject("SceneController");

                    SceneController controller = go.AddComponent<SceneController>();
                    return controller;
                }
                else
                {
                    instance = go.GetComponent<SceneController>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Can't have two instance of singleton.");
            DestroyImmediate(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        //Scene ��ȭ�� ���� �̺�Ʈ �޼ҵ� ����
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, LoadSceneMode.Single));
    }

    public void LoadSceneAdditve(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }

    IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

        while (!asyncOperation.isDone)
            yield return null;

        Debug.Log("LoadSceneAsync is complete");
    }

    public void OnActiveSceneChanged(Scene scene0, Scene scene1)
    {
        Debug.Log("OnActiveSceneChanged is called! scene0 = " + scene0.name + ", scene1 = " + scene1.name);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("OnSceneLoaded is called! scene = " + scene.name + ", loadSceneMode = " + loadSceneMode.ToString());
    }

    public void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("OnSceneUnloaded is called! scene = " + scene.name);
    }
}