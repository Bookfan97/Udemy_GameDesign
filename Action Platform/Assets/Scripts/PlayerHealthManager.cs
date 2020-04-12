using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {
    
	public int startingHealth;
	public int currentHealth;
	private PlayerController thePC;
	public SpriteRenderer[] bodyParts;
	public float flashLength;
	private float flashCounter;

	// Use this for initialization
	void Start () {
		thePC = GetComponent<PlayerController>();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <= 0)
		{
			gameObject.SetActive(false);
		}
		if (flashCounter > 0)
		{
			flashCounter -= Time.deltaTime;
			if (flashCounter <= 0)
			{
				for (int i = 0; i < bodyParts.Length; i++)
				{
					bodyParts[i].material.SetFloat("_FlashAmount", 0);
				}
			}
		}
	}

	public void HurtPlayer(int damage)
	{
		if (!thePC.knockBack)
		{
			currentHealth -= damage;
			thePC.Knockback();
			Flash();
		}
	}

	public void Flash()
	{
		for (int i = 0; i < bodyParts.Length; i++)
		{
			bodyParts[i].material.SetFloat("_FlashAmount", 1);
		}
		flashCounter = flashLength;

	}
}