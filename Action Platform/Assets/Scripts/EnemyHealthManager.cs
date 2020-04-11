using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {
    
	public int startingHealth;
	private int currentHealth;
	public SpriteRenderer[] bodyParts;
	public float flashLength;
	private float flashCounter;
	private EnemyController theEC;
	private Animator anim;
	private Rigidbody2D myRB;
	public float deathSpin;
	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		theEC = GetComponent<EnemyController>();
		anim = GetComponent<Animator>();
		myRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0)
		{
			//gameObject.SetActive(false);
			theEC.enabled = false;
			anim.enabled = false;
			myRB.constraints = RigidbodyConstraints2D.None;
			myRB.AddTorque(deathSpin * -transform.localScale.x);
			gameObject.layer = LayerMask.NameToLayer("Dead");
		}
		if(flashCounter > 0)
		{
			flashCounter -= Time.deltaTime;
			if(flashCounter<= 0)
			{
				for (int i = 0; i < bodyParts.Length; i++)
				{
					bodyParts[i].material.SetFloat("_FlashAmount", 0);
				}
			}
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		theEC.Knockback();
		Flash();
	}

	public void Flash()
	{
		for (int i=0; i<bodyParts.Length; i++)
		{
			bodyParts[i].material.SetFloat("_FlashAmount", 1);
		}
		flashCounter = flashLength;

	}
}