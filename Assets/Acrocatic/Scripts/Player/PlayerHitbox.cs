﻿using UnityEngine;
using System.Collections;

// Class that controls the player's hitbox.
public class PlayerHitbox : MonoBehaviour {
	// Group default hitbox variables.
	[System.Serializable]
	public class DefaultHitbox {
		public Vector2 boxSize;					// Size for the default hitbox.
		public Vector2 boxCenter;				// Center for the default hitbox.
	}

	// Group crouch hitbox variables.
	[System.Serializable]
	public class CrouchHitbox {
		public Vector2 boxSize;					// Size for the crouch hitbox.
		public Vector2 boxCenter;				// Center for the crouch hitbox.
	}
	
	// Group jump/fall hitbox variables.
	[System.Serializable]
	public class JumpHitbox {
		public Vector2 boxSize;					// Size for the jump/fall hitbox.
		public Vector2 boxCenter;				// Center for the jump/fall hitbox.
	}
	
	// Public variables that shouldn't be shown in the inspector.
	[HideInInspector]
	public string currentHitbox = "default";	// Set the current hitbox value.

	// Public variables.
	[Tooltip("This is the player's default hitbox.")]
	public DefaultHitbox defaultHitbox;
	[Tooltip("This is the player's hitbox while crouching.")]
	public CrouchHitbox crouchHitbox;
	[Tooltip("This is the player's hitbox while in the air.")]
	public JumpHitbox jumpHitbox;
	// Got more hitboxes? Add them here!

	// Private variables.
	private BoxCollider2D boxCollider;			// Get the player's box collider.
	private Player player;						// Get the Player class.

	// Also need to change the circle collider? Here's an example how to achieve this:
	// public class DefaultHitbox {
	//		...
	// 		public float circleRadius;
	// 		public Vector2 circleSize;
	// }
	// private CircleCollider2D circleCollider;
	// 
	// Inside the Start function:
	// circleCollider = GetComponent<CircleCollider2D>();
	// 
	// Inside the ChangeHitbox function:
	// circleCollider.radius = defaultHitbox.circleRadius;
	// circleCollider.size = defaultHitbox.circleSize;

	// Use this for initialization.
	void Start () {
		// Setting up references.
		boxCollider = GetComponent<BoxCollider2D>();
		player = GetComponent<Player>();

		// Set default hitbox.
		ChangeHitbox("default");
	}
	
	// Public function to change the hitbox.
	public void ChangeHitbox(string type) {
		// Only change hitbox if it's another hitbox.
		if (currentHitbox == type) return;

		// Set the current hitbox value.
		currentHitbox = type;

		// Switch statement to check what hitbox should be used.
		// Change the hitboxes based on the type.
		// Got more hitboxes? Add them here!
		switch(type) {
			case "default":
				boxCollider.size = defaultHitbox.boxSize;
				boxCollider.center = defaultHitbox.boxCenter;
				break;
			case "crouch":
				boxCollider.size = crouchHitbox.boxSize;
				boxCollider.center = crouchHitbox.boxCenter;
				break;
			case "jump":
				boxCollider.size = jumpHitbox.boxSize;
				boxCollider.center = jumpHitbox.boxCenter;
				break;
		}

		// Change the sideCheck position based on the player's hitbox.
		Vector2 pos = transform.position;
		if (player.facingRight) {
			player.sideCheckTop.transform.position = new Vector2 (pos.x + boxCollider.center.x + (boxCollider.size.x / 2), pos.y + boxCollider.center.y + (boxCollider.size.y / 2));
			player.sideCheckBot.transform.position = new Vector2 (pos.x + boxCollider.center.x + (boxCollider.size.x / 2) + 0.1f, pos.y + boxCollider.center.y - (boxCollider.size.y / 2));
		} else {
			player.sideCheckTop.transform.position = new Vector2 (pos.x + boxCollider.center.x - (boxCollider.size.x / 2), pos.y + boxCollider.center.y + (boxCollider.size.y / 2));
			player.sideCheckBot.transform.position = new Vector2 (pos.x + boxCollider.center.x - (boxCollider.size.x / 2) - 0.1f, pos.y + boxCollider.center.y - (boxCollider.size.y / 2));
		}
	}

	// Public function to check if the player's default hitbox collides with the ground layer.
	// This is used to determine if the player is allowed to stand up while crouching.
	public bool AllowedToStandUp() {
		// Get the player's current position.
		Vector2 pos = transform.position;

		/* Use this (instead of the code below) when the crouch hitbox is wider than the default hitbox.
		// Set point A and point B to draw a rectangle. This is the difference between the default and crouching hitboxes.
		Vector2 pointA;
		Vector2 pointB;

		// Point A and point B depend on if the player is facing left or right.
		if (player.facingRight) {
			pointA = new Vector2 (pos.x + crouchHitbox.boxCenter.x + (crouchHitbox.boxSize.x / 2) - defaultHitbox.boxSize.x, pos.y + defaultHitbox.boxCenter.y + (defaultHitbox.boxSize.y / 2));
			pointB = new Vector2 (pos.x + crouchHitbox.boxCenter.x + (crouchHitbox.boxSize.x / 2), pos.y + crouchHitbox.boxCenter.y + (crouchHitbox.boxSize.y / 2));
		} else {
			pointA = new Vector2 (pos.x + defaultHitbox.boxCenter.x - (defaultHitbox.boxSize.x / 2), pos.y + defaultHitbox.boxCenter.y + (defaultHitbox.boxSize.y / 2));
			pointB = new Vector2 (pos.x + defaultHitbox.boxCenter.x + (defaultHitbox.boxSize.x / 2), pos.y + crouchHitbox.boxCenter.y + (crouchHitbox.boxSize.y / 2));
		}
		*/

		// Set point A and point B to draw a rectangle. This is the difference between the default and crouching hitboxes.
		Vector2 pointA = new Vector2(pos.x + defaultHitbox.boxCenter.x - (defaultHitbox.boxSize.x / 2), pos.y + defaultHitbox.boxCenter.y + (defaultHitbox.boxSize.y / 2));
		Vector2 pointB = new Vector2(pos.x + crouchHitbox.boxCenter.x + (crouchHitbox.boxSize.x / 2), pos.y + crouchHitbox.boxCenter.y + (crouchHitbox.boxSize.y / 2));

		// Check if the player is hitting its head.
		bool hittingHead = Physics2D.OverlapArea(pointA, pointB, player.groundLayer);

		// Return if the player is hitting its head or not.
		return !hittingHead;
	}
}
