using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	[HideInInspector]
	public bool playerOnPlatform = false;		// Check if the player is standing on the platform.

	// Public variables.
	[Tooltip("Select the platform's type.")]
	public PlatformTypes platformType;

	// Private variables.
	private PlatformSink sinking;				// Get the platform sinking class.

	// Use this for initialization.
	void Start () {
		sinking = GetComponent<PlatformSink>();
	}
	
	// Update is called once per frame.
	void Update () {
	
	}

	// Check if the player is on the platform.
	public void SetPlayerOnPlatform(bool onPlatform) {
		playerOnPlatform = onPlatform;

		// If the platform is a sinking platform...
		if (platformType == PlatformTypes.Sinking && sinking) {
			// Start or reset the timer based on the sinking variables for the platform.
			if (onPlatform) {
				sinking.StartSinkTimer();
			} else {
				sinking.ResetSinkTimer();
			}
		}
	}

	// Shake the camera.
	public void ShakeCamera() {
		sinking.ShakeCamera();
	}
}
