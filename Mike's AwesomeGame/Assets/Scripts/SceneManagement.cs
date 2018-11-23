using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour {

	public string nextScene;

	public void Start()
	{
		Cursor.visible = true;
	}
	public void startGame()
	{
		SceneManager.LoadScene(nextScene);
	}

	public void goToOptions()
	{
		SceneManager.LoadScene("keysOptions");
	}
	public void quit()
	{
		Application.Quit();
	}
}
