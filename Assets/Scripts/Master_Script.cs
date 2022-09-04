using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Master_Script : MonoBehaviour
{
    private TitleScreenScript titleScreenScript;
    [SerializeField] GameObject textHelp, textFinish, fruit1, fruit2, fruit3, fruit4;
    // ENCAPSULATION
    public Color color { get; protected set; }
    public int checkpoint;
    public bool gameDefeated, fruit1taken, fruit2taken, fruit3taken, fruit4taken;
    private void Awake()
    {
        if (gameDefeated)
        {
            ResetProgress();
        }
        if (GameObject.Find("TitleScreen") != null)
        {
            titleScreenScript = GameObject.Find("TitleScreen").GetComponent<TitleScreenScript>();
            if (titleScreenScript.selectedContinue && !gameDefeated)
            {
                LoadState();
                CheckFruits();
            }
            GameObject titleScreenObj = GameObject.Find("TitleScreen");
            Destroy(titleScreenObj);
        }
        else
        {
            LoadState();
            CheckFruits();
        }
    }
    protected void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.color = color;
        Invoke("RemoveHelpText", 2);
    }
    private void Update()
    {
        if (fruit1taken && fruit2taken && fruit3taken && fruit4taken)
        {
            ShowCongratsText();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetProgress();
            SceneManager.LoadScene("Title Screen");
        }
    }

    // ABSTRACTION
    public void CheckFruits()
    {
        if (fruit1taken)
        {
            GameObject.Destroy(fruit1);
        }
        if (fruit2taken)
        {
            GameObject.Destroy(fruit2);
        }
        if (fruit3taken)
        {
            GameObject.Destroy(fruit3);
        }
        if (fruit4taken)
        {
            GameObject.Destroy(fruit4);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void RemoveHelpText()
    {
        GameObject.Destroy(textHelp);
    }
    private void ShowCongratsText()
    {
        textFinish.SetActive(true);
        gameDefeated = true;
    }

    private void ResetProgress()
    {
        checkpoint = 0;
        fruit1taken = false;
        fruit2taken = false;
        fruit3taken = false;
        fruit4taken = false;
        gameDefeated = false;
    }

    [System.Serializable]
    class SaveData
    {
        public int checkpointStore;
        public bool GameDefeated, isFruit1Taken, isFruit2Taken, isFruit3Taken, isFruit4Taken;
    }

    // ABSTRACTION
    public void SaveState()
    {
        SaveData data = new SaveData();
        data.GameDefeated = gameDefeated;
        data.checkpointStore = checkpoint;
        data.isFruit1Taken = fruit1taken;
        data.isFruit2Taken = fruit2taken;
        data.isFruit3Taken = fruit3taken;
        data.isFruit4Taken = fruit4taken;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // ABSTRACTION
    public void LoadState()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            gameDefeated = data.GameDefeated;
            checkpoint = data.checkpointStore;
            fruit1taken = data.isFruit1Taken;
            fruit2taken = data.isFruit2Taken;
            fruit3taken = data.isFruit3Taken;
            fruit4taken = data.isFruit4Taken;
        }
    }
}
