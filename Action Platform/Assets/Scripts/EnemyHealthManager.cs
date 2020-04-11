using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {
    
	public int startingHealth;
	private int currentHealth;
	public SpriteRenderer[] bodyParts;
	public float flashLength;
	private float flashCounter;
	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0)
		{
			gameObject.SetActive(false);
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