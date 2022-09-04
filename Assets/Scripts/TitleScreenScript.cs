using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreenScript : MonoBehaviour
{
    public bool selectedContinue;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartNewGame();
        }
        if (Input.GetKeyDown("return"))
        {
            Continue();
        }
        if (Input.GetKeyDown("escape"))
        {
            Quit();
        }
    }
    public void StartNewGame()
    {
        selectedContinue = false;
        SceneManager.LoadScene("Level");
    }
    public void Continue()
    {
        selectedContinue = true;
        SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


}
