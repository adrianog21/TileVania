using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistence : MonoBehaviour
{
    int sceneIndex;

    private void Awake()
    {
        int numGamePersitence = FindObjectsOfType<GameSession>().Length;
        if (numGamePersitence > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int newSceneIndex = SceneManager.GetActiveScene().buildIndex;
         if (sceneIndex != newSceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
