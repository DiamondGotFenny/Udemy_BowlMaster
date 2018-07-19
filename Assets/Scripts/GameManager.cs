using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameManager : MonoBehaviour {
    private List<int> bowls = new List<int>();

    private BallMovement ball;
    private PinSetter pinSetter;
    private ScoreDisplay scoreBoard;
    public GameObject EndgamePanel;
    public GameObject TouchinputPanel;
    public Text EndScore, finalFrameDis;

	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<BallMovement>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        scoreBoard = GameObject.FindObjectOfType<ScoreDisplay>();
        EndgamePanel.SetActive (false);
	}

    private void Update()
    {
        EndScore.text = finalFrameDis.text;
    }

    public void Bowl(int pinFall)
    {
        try
        {
            bowls.Add(pinFall);

            ActionMasterOld.myAction nextAction = ActionMasterOld.NextAction(bowls);
            pinSetter.PerformAction(nextAction);
            ball.ballReset();

        }
        catch (System.Exception)
        {
            Debug.LogWarning("something wrong with Bowl()");
            throw;
        }

        try
        {
            scoreBoard.FillRolls(bowls);
            scoreBoard.FillFrames(ScoreMaster.ScoreCumulative(bowls));
        }
        catch (System.Exception)
        {
            Debug.LogWarning("FillRollCard failed");
            throw;
        }
    }

    public void EndGame()
    {
        TouchinputPanel.SetActive(false);
        EndgamePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

}
