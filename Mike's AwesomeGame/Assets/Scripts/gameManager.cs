using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class gameManager : MonoBehaviour {

	private static gameManager _instance;
	public static gameManager Instance { get { return _instance; } }

	public Transform playerTransform;
	public PlayerHealth playerHp;
	public TextMeshProUGUI totalScore;
	public TextMeshProUGUI timerUI;
	float currentScore;

	public Transform playerParticle;
	public ParticleSystem playerPS;

	public Slider rightAmmo;
	public Slider leftAmmo;
	public TextMeshProUGUI rightAmmoCount;
	public TextMeshProUGUI leftAmmoCount;

	public CameraFollow mainCamera;
	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	public Slider slider;
	public Image fill;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timerUI.text = Time.timeSinceLevelLoad.ToString("0.00");
	}

	public void damagePlayer(int dmg)
	{
		playerHp.reduceHealthPoints(dmg);
	}

	public Vector2 mousePos()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public void sliderAddData(float max,float amt, Color color)
	{
		slider.maxValue = max;
		slider.value = amt;
		fill.color = color;
	}

	public Vector3 GetTransformPos()
	{
		return playerTransform.position;
	}

	public void addScore(float score)
	{
		currentScore += score;
		totalScore.text = "Score: " + currentScore.ToString();
	}

	public int getScore()
	{
		return (int)currentScore;
	}

	public void playerHit(bool damage, int amt = 5)
	{
		if(damage)
		{
			playerPS.startColor = new Color32(255, 150, 0, 255);
		}
		else
		{
			playerPS.startColor = new Color32(0, 253, 253, 255);
		}
		playerParticle.transform.position = playerTransform.position;
		playerPS.Emit(amt);
	}

	public void setRightAmmoCount(int cur, int max)
	{
		rightAmmo.maxValue = max;
		rightAmmo.value = cur;

		rightAmmoCount.text = "Right: " + cur.ToString();
	}

	public void setLeftAmmoCount(int cur, int max)
	{
		leftAmmo.maxValue = max;
		leftAmmo.value = cur;

		leftAmmoCount.text = "Left: " + cur.ToString();
	}

	public void shake()
	{
		mainCamera.cameraShake();
	}
}
