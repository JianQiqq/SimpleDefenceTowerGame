using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ENDUI;
    public Text GameMessage;
    public static GameManager instance;
    private Enemyspanner enemySpawner;

    void Awake()
    {
        instance = this;
        enemySpawner = GetComponent<Enemyspanner>();
    }


    public void Win()
    {
        ENDUI.SetActive(true);
        GameMessage.text = "YOU WIN";

    }
    public void Lose()
    {
        enemySpawner.Stop();
        ENDUI.SetActive(true);
        GameMessage.text = "GAME OVER";
    }

    public void OnButtonRetry()
    {
        ENDUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
