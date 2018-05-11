using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Gun {

	private LineRenderer aimLine;

	// Use this for initialization
	void Start () {

		aimLine = GetComponent<LineRenderer>();
		
	}
	
	// Update is called once per frame
	protected override void Update () {

		SniperAim();
		base.Update();

		if (canShoot)
		{
			if (Input.GetMouseButtonDown(0) && base.nextShotTimer <= 0)
			{
				base.Fire();	
			}
		}

	}

	private void SniperAim() { 
		
		GetComponent<LineRenderer> ().positionCount = 2;
		GetComponent<LineRenderer> ().SetPosition(0, new Vector2(base.bulletSpawn.position.x, base.bulletSpawn.position.y));
		GetComponent<LineRenderer> ().SetPosition(1, new Vector2(base.mousePos.x, base.mousePos.y));
		GetComponent<LineRenderer> ().material.color = Color.red;
	}
}
