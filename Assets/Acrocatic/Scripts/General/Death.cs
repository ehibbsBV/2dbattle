using UnityEngine;
using System.Collections;

// Class that handles the player's death.
public class Death : MonoBehaviour {
	// Private variables.
	private bool isDead = false;
	private float deathTimer = 1f;

	// Check if the player is entering the trigger.
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && !isDead) {
			// Make sure the player is dead.
			isDead = true;

			// Show the 'fall' animation for the player.
			other.gameObject.GetComponent<Player>().Fall();
		}
	}

	void Update() {
		// If the player is dead...
		if (isDead) {
			// ... run a timer.
			if (deathTimer > 0) {
				deathTimer -= Time.deltaTime;
			} else {
				// When the timer is complete: reload the same level.
				Application.LoadLevel(Application.loadedLevelName);
			}
		}

	}
}
