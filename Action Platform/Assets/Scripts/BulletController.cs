using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    
	public float bulletSpeed;

	private Rigidbody2D myRB;
	private PlayerController thePlayer;

	public int damageToGive;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D>();
		thePlayer = FindObjectOfType<PlayerController>();
		if(transform.position.x < thePlayer.transform.position.x)
		{
			bulletSpeed = -bulletSpeed;
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}
	
	// Update is called once per frame
	void Update () {

		myRB.velocity = new Vector2(bulletSpeed, myRB.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyHealthManager>().TakeDamage(damageToGive);
		}
		Destroy(gameObject);
	}
}