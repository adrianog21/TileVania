using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] GameObject winLabel;
    int index;

    private void Start()
    {
        winLabel.SetActive(false);
        index = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        winLabel.SetActive(true);
       // Time.timeScale = 0.5f;
        StartCoroutine(NextLevel());
    }
    

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index + 1);
    }
}
