using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

	private Player player;

	public Bullet bullet;
	public GameObject particle;

	public Transform bulletSpawn;

	protected Vector3 direction;
	protected Vector3 mousePos;

	public bool canShoot = true;

	[SerializeField]
	private float nextShotTime;
	protected float nextShotTimer;

	[SerializeField]
	private float reloadTime;

	public float bulletSpeed;
	public float shotDistance;

	[SerializeField]
	private int magazineSize;
	private int bulletsInMagazine;

	// Use this for initialization
	void Awake () {

		bulletsInMagazine = magazineSize;
		player = FindObjectOfType<Player>();
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {

		FaceDirection();
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		nextShotTimer -= Time.deltaTime;
	}

	public void Fire() {

		if (bulletsInMagazine > 0) {
			Bullet newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
			GameObject bulletParticle = Instantiate(particle, bulletSpawn.position, Quaternion.identity);
			Destroy(bulletParticle, 0.3f);

			newBullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
			bulletsInMagazine -= 1;

            //Destroy(newBullet, shotDistance);
			nextShotTimer = nextShotTime;
		} else {
			StartCoroutine("Reload");
		}

	}

	void DestroyBullet(GameObject newBullet) {
		if (Vector3.Distance(transform.position, newBullet.transform.position) > shotDistance)
		{
			Destroy(newBullet);
		}
	}

	private IEnumerator Reload() {
		canShoot = false;
		yield return new WaitForSeconds(reloadTime);
		bulletsInMagazine = magazineSize; 
		canShoot = true;
	}

	void FaceDirection() { 

		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
		direction = (Input.mousePosition - sp).normalized;
		float gunRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, gunRotation));
		
		if (direction.x < 0) {
			player.transform.localScale = new Vector2(-1, 1);
			transform.localScale = new Vector2(-1, -1);
		} else { 
			player.transform.localScale = new Vector2(1, 1);
			transform.localScale = new Vector2(1, 1);
		}
	}
}
