using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour {

    public int maxHealth;
    int curHealth;
    // Use this for initialization
    public Slider playerHealthUI;
	void Start () {
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

        playerHealthUI.value = curHealth;
		if(curHealth <= 0)
        {
			SceneManager.LoadScene("EndScene");
        }

		if(curHealth > maxHealth)
		{
			curHealth = maxHealth;
		}
	}

   public void reduceHealthPoints(int points)
    {
        Debug.Log("<color=red>Player HP: </color>" + curHealth);
		if(points < 0)
		{
			soundManage.Instance.useSound(sounds.TAKEHIT);
			gameManager.Instance.playerHit(true);
			gameManager.Instance.shake();
		}
		else
		{
			soundManage.Instance.useSound(sounds.PICKUP);
			gameManager.Instance.playerHit(false);
		}

        curHealth += points;
    }
}
