using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	[SerializeField]
	protected float speed;

	private Player player;
	private WaveManager waveManager;

	void Awake()
	{
		player = FindObjectOfType<Player>();
		waveManager = FindObjectOfType<WaveManager>();
	}

	protected virtual void Update()
	{
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
	}

	void OnDestroy()
	{
		waveManager.currentEnemyCount -= 1;	
	}

	void DestroyMe() {


		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag("Bullet")) {

			DestroyMe();
		}
	}
}
