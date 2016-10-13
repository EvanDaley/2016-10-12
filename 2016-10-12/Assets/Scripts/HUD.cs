using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Player localPlayer;
	public Slider healthSlider;

	void Update()
	{
		healthSlider.value = localPlayer.HealthNormalized;
	}
}
