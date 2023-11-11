using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text healthText;

    public static GameController invocar;

    // Start is called before the first frame update
    void Awake()
    {
        invocar = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int value)
    {
        healthText.text = "x " + value.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
