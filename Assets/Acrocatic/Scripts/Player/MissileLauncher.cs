using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {


  public GameObject firePoint;
  public Projectile projectile;
  public bool isTimed;
  public float fireCooldown = 3.0f;
  private bool isOnCooldown = false;
  private float cooldownTimer = 0.0f;
	// Use this for initialization
	void Start () {
    projectile.Fire();
	}
	
	// Update is called once per frame
	void Update () {

    if(isOnCooldown){
      cooldownTimer -= Time.deltaTime;

      if(cooldownTimer < 0){
        isOnCooldown = false;
      }
    }
    else if(Input.GetKeyDown("PrimaryFire")){
      Fire();
    }

	}

  void Fire(){
    isOnCooldown = true;
    cooldownTimer = fireCooldown;
    projectile.Fire();
  }
}
