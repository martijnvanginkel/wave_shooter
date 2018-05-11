using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

	private Player player;
	public Collider2D playerColl;

	public GameObject[] platforms;

	public string platformName;

	// Use this for initialization
	void Start () {

		player = FindObjectOfType<Player>();
		//playerColl = player.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

		// Make child of platform player is on
		foreach (GameObject platform in platforms){
			Collider2D coll = platform.GetComponent<Collider2D>();

			if (playerColl.IsTouching(coll)) {
				player.transform.SetParent(platform.transform);
			}
		}
		
	}
}
