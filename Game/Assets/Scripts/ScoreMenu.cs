using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // added to change score number

public class ScoreMenu : MonoBehaviour
{
    public static bool GameIsPaused = true;
    public GameObject pauseMenuUI;
    public List<GameObject> playerList;
    public List<Text> scoreList;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pause());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Resume()
    {
        Debug.Log("Started Resume at timestamp : " + Time.time);

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Reset the player scores everytime the game begins
        foreach (GameObject player in playerList)
        {
            player.GetComponent<CharacterController2D>().coins = 0;
            player.GetComponent<CharacterController2D>().coinText.text = "0";
        }

        yield return new WaitForSecondsRealtime(10f);

        Debug.Log("Finished Resume at timestamp : " + Time.time);
        StartCoroutine(Pause());
    }

    private IEnumerator Pause()
    {
        Debug.Log("Started Pause at timestamp : " + Time.time);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Set the scoreboard scores everytime the score board appears
        for (int score=0;  score < playerList.Count; score++)
        {
            scoreList[score].text = playerList[score].GetComponent<CharacterController2D>().coinText.text;
        }

        yield return new WaitForSecondsRealtime(30f);

        Debug.Log("Finished Pause at timestamp : " + Time.time);
        StartCoroutine(Resume());
        
    }
}
