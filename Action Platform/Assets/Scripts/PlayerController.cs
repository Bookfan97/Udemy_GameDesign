﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
	public float moveSpeed;
	public float jumpSpeed;

	private Rigidbody2D myRB;

	private Animator anim;

	public GameObject bullet;
	public Transform bulletPoint;

	public Transform groundPoint;
	public LayerMask whatIsGround;
	public float groundRadius;

	public bool grounded;

	public AudioSource jumpSound;
	public AudioSource gunSound;

	public float waitBetweenShots = .5f;
	private float betweenShotCounter = .5f;

	public GameObject muzzleFlash;

	private CameraController theCamera;
	public float shakeAmount;

	public float knockbackForce;
	public float knockbackLength;
	private float knockbackCounter;
	public bool knockBack;

	private PlayerHealthManager playerHealth;
	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		theCamera = FindObjectOfType<CameraController>();
		playerHealth = GetComponent<PlayerHealthManager>();
	}

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);
	}
	
	// Update is called once per frame
	void Update () {

		muzzleFlash.SetActive(false);
		if (knockBack)
		{
			knockbackCounter -= Time.deltaTime;
			myRB.velocity = new Vector2(-knockbackForce * transform.localScale.x, myRB.velocity.y);
			if (knockbackCounter <= 0)
			{
				knockBack = false;
			}
		}
		else if (!playerHealth.dead)
		{
			myRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, myRB.velocity.y);

			if (Input.GetButtonDown("Jump") && grounded)
			{
				myRB.velocity = new Vector2(myRB.velocity.x, jumpSpeed);
				jumpSound.Play();
			}

			if (Input.GetAxisRaw("Horizontal") > 0 && transform.localScale.x < 0)
				transform.localScale = new Vector3(1f, 1f, 1f);
			if (Input.GetAxisRaw("Horizontal") < 0 && transform.localScale.x > 0)
				transform.localScale = new Vector3(-1f, 1f, 1f);

			if (Input.GetButtonDown("Fire1"))
			{
				Shoot();
			}

			if (Input.GetButton("Fire1"))
			{
				betweenShotCounter -= Time.deltaTime;
				if (betweenShotCounter <= 0)
				{
					Shoot();
				}
			}
		}

		if(playerHealth.dead)
		{
			if(myRB.velocity.x>.1f)
			{
				myRB.velocity = new Vector2(myRB.velocity.x / 2, myRB.velocity.y);
			}
			else
			{
				myRB.velocity = new Vector2(0,myRB.velocity.y);
			}
			
		}

		anim.SetFloat("Speed", Mathf.Abs(myRB.velocity.x));
		anim.SetBool("Grounded", grounded);
	}

	public void Shoot()
	{
		Instantiate(bullet, bulletPoint.position, transform.rotation);
		gunSound.Play();
		betweenShotCounter = waitBetweenShots;
		muzzleFlash.SetActive(true);
		theCamera.ScreenShake(shakeAmount);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			Debug.Log("On Enemy");
			myRB.velocity = new Vector2(myRB.velocity.x, jumpSpeed * 0.75f);
		}
	}

	public void Knockback()
	{
		knockbackCounter = knockbackLength;
		myRB.velocity = new Vector2(-knockbackForce * transform.localScale.x, knockbackForce);
		knockBack = true;
	}
}