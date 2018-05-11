using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	private Rigidbody2D rb;
	private Animator animator;
	private Collider2D coll;

	public LayerMask whatIsGround;
	public bool grounded;
	public bool canMove = true;
	public bool walking = true;

	public Gun gun;

	[SerializeField]
	private float dashDistance;
	[SerializeField]
	private float dashTime;
	[SerializeField]
	private float timeNextDash;
	private float dashTimer;
	private bool canDash;
	private bool isDashing;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		coll = GetComponent<Collider2D>();

		dashTimer = dashTime;
		canDash = true;
	}
	
	// Update is called once per frame
	protected override void Update () {

		if (!canMove) {
			return;
		} else {
			GetInput();
		}

		// while bool is true execute
		if (isDashing) {
			Dash();
		}

		grounded = Physics2D.IsTouchingLayers (coll, whatIsGround);

		if (grounded != true) {
			Die();
		}

		base.Update();

		if (direction != Vector2.zero && canMove) {
			walking = true;
		} else {
			walking = false;
		}

		animator.SetBool("Walking", walking);
		animator.SetBool("Dashing", isDashing);

	}

	private void Dash () {

		// Move player while timer is running
		canDash = false;
		dashTimer -= Time.deltaTime;
		float step = dashDistance * Time.deltaTime;
		Vector3 destination = transform.position + (Vector3)base.direction;
		transform.position = Vector3.MoveTowards(transform.position, destination, step);

		// If timer is 0 stop dashing and allow next dash 
		if (dashTimer < 0) {
			isDashing = false;
			StartCoroutine("NextDash");
		}
	}

	private void Die() {
		canMove = false;
	}

	private void GetInput () {

		direction = Vector2.zero;

		if (Input.GetKey(KeyCode.W)) {

			direction += Vector2.up;
		}

		if (Input.GetKey(KeyCode.A)) {

			direction += Vector2.left;
		}

		if (Input.GetKey(KeyCode.S)) {

			direction += Vector2.down;
		}

		if (Input.GetKey(KeyCode.D)) {

			direction += Vector2.right;
		}

		if (Input.GetKeyDown(KeyCode.Space) && canDash)
		{
			// If allowed and press space, dashing is true
			isDashing = true;
		}
	}

	public IEnumerator NextDash() {

		yield return new WaitForSeconds(timeNextDash);
		dashTimer = dashTime;
		canDash = true;
	}
}
