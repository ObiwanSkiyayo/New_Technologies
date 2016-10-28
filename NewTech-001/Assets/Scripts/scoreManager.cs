using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour {

    public GameObject scoreBoard;
    public int score;

    public bool scored;
    private int lastScore;

    // Use this for initialization
    void Start () {

        scoreBoard = GameObject.Find("ScoreBoard");

    }
	
	// Update is called once per frame
	void Update () {

        if (scored)
        {
            Text scoreBoardText = scoreBoard.GetComponent<Text>();
            score = score + 1000;
            scoreBoardText.text = "" + score;
            scored = false;
        }


    }
}
