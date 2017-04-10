using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, IScorable {

	private SpriteRenderer coinRenderer;
	private Collider2D coinCollider;

	private void Awake()
	{
		coinRenderer = GetComponent<SpriteRenderer>();
		coinCollider = GetComponent<Collider2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Bird>())
		{
			(this as IScorable).OnScored();
		}
	}

	public void ShowCoinAgain()
	{
		coinRenderer.enabled = true;
		coinCollider.enabled = true;
	}

	void IScorable.OnScored()
	{
		GameController.Instance.BirdScored();

		//Remove the coin from the scene without destroying it (we would be messing with the object pool otherwise)
		coinRenderer.enabled = false;
		coinCollider.enabled = false;
	}
}
