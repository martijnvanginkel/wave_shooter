using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

	[SerializeField]
	private float speed;

	[SerializeField]
	private int pointA;
	[SerializeField]
	private int pointB;

	[SerializeField]
	private bool dirRight;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		if (dirRight) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		} else {
			transform.Translate(Vector3.right * -speed * Time.deltaTime);
		}

		if (transform.position.x >= pointB) {
			dirRight = false;
		}

		if (transform.position.x <= pointA) {
			dirRight = true;
		}
	
	}
}

