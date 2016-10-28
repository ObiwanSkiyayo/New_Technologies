using UnityEngine;
using System.Collections;

public class spotLightScript : MonoBehaviour {

	private Light spotLight;

	// Soft Action Ambience Hue (Red/Blue)
	public Color color1 = new Color( 0.9f, 0.9f, 1.0f, 0.5f ); // Blue Hue
	public Color color2 = new Color( 1.0f, 0.9f, 0.9f, 0.5f ); // Red Hue

	void Start () {
	
		spotLight = GetComponent<Light> ();
		spotLight.intensity = 0.10f;

	}

	void Update () {

		// Make the color of the light lerp between Color 1 and Color 2.
		// PingPongs the _Time value between 0 and 1.
		float _Time = Mathf.PingPong(Time.time, 1);
		spotLight.color = Color.Lerp(color1, color2, _Time);

	}
}
