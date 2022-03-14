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
    public float pauseTime = 30f;
    public float resumeTime = 120f;
    private float pauseTimeRemaining;
    private float resumeTimeRemaining;
    public Text pauseTimeText;
    public Text resumeTimeText;

    void Start()
    {
        StartCoroutine(Pause());
    }

    void Update()
    {
        // Update text timers for each screen when active
        if (pauseMenuUI.activeInHierarchy)
        {
            if (pauseTimeRemaining > 0)
            {
                pauseTimeText.text = (Mathf.FloorToInt(pauseTimeRemaining % pauseTime)).ToString();
                pauseTimeRemaining -= Time.unscaledDeltaTime;
            }
        } else 
        {
            if (resumeTimeRemaining > 0)
            {
                resumeTimeText.text = (Mathf.FloorToInt(resumeTimeRemaining % resumeTime)).ToString();
                resumeTimeRemaining -= Time.unscaledDeltaTime;
            }
        }
    }       

    private IEnumerator Resume()
    {
        // Destroy existing player effectors
        DestroyWithTag("JumpUp");
        DestroyWithTag("SpeedUp");
        DestroyWithTag("DecreaseCoin");

        resumeTimeRemaining = resumeTime;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Reset the player scores everytime the game begins
        foreach (GameObject player in playerList)
        {
            player.GetComponent<CharacterController2D>().coins = 0;
            player.GetComponent<CharacterController2D>().coinText.text = "0";
        }

        yield return new WaitForSecondsRealtime(resumeTime);
        StartCoroutine(Pause());
    }

    private IEnumerator Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Set the scoreboard scores everytime the score board appears
        for (int score=0;  score < playerList.Count; score++)
        {
            scoreList[score].text = playerList[score].GetComponent<CharacterController2D>().coinText.text;
        }
        pauseTimeRemaining = pauseTime;
        yield return new WaitForSecondsRealtime(pauseTime);
        
        StartCoroutine(Resume());
    }

    private void DestroyWithTag(string tag)
    {
        GameObject[] objectList = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject item in objectList)
        {
            Destroy(item);
        }
    }
}
