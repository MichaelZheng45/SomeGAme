using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponSound
{
	NORMAL,
	REPEATER,
	HIVE,
	ROCKET,
	TRI,
	BLAST,
	RAPID,
	RIPPER,
	DRAGONFIRE,
	STORM
}

public enum sounds
{
	ROCKETLAUNCH,
	TAKEHIT,
	PICKUP,
	ENEMYDEATH,
	GUNCOCK
}
public class soundManage : MonoBehaviour {


	private static soundManage _instance;
	public static soundManage Instance { get { return _instance; } }

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

	public List<AudioSource> weaponSoundAudio;
	public List<AudioSource> soundAudio;

	public void weaponSounds(weaponSound sound, bool isMissile = false)
	{
		if((sound == weaponSound.HIVE || sound == weaponSound.RIPPER || sound == weaponSound.ROCKET) && isMissile == false)
		{
			soundAudio[(int)sounds.ROCKETLAUNCH].Play();
		}
		else
		{
			weaponSoundAudio[(int)sound].Play();
		}
	}

	public void useSound(sounds sound)
	{
		soundAudio[(int)sound].Play();
	}
}
