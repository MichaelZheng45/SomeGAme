using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreSpawner : MonoBehaviour {

    public TextMeshProUGUI totalScore;
    float currentScore;

    public void addScore(float score)
    {
        currentScore += score;
        totalScore.text = "Score: " + currentScore.ToString();
    }

	public int getScore()
	{
		return (int)currentScore;
	}
}
