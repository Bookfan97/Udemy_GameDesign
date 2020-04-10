using UnityEngine;
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

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);
	}
	
	// Update is called once per frame
	void Update () {
		
		myRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, myRB.velocity.y);

		if(Input.GetButtonDown("Jump") && grounded)
		{
			myRB.velocity = new Vector2(myRB.velocity.x, jumpSpeed);
		}

		if(Input.GetAxisRaw("Horizontal") > 0 && transform.localScale.x < 0)
			transform.localScale = new Vector3(1f, 1f, 1f);
		if(Input.GetAxisRaw("Horizontal") < 0 && transform.localScale.x > 0)
			transform.localScale = new Vector3(-1f, 1f, 1f);
		
		if(Input.GetButtonDown("Fire1"))
		{
			Instantiate(bullet, bulletPoint.position, transform.rotation);
		}

		anim.SetFloat("Speed", Mathf.Abs(myRB.velocity.x));
		anim.SetBool("Grounded", grounded);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			Debug.Log("On Enemy");
			myRB.velocity = new Vector2(myRB.velocity.x, jumpSpeed * 0.75f);
		}
	}
}