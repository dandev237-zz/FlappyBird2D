using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RepeatingBackground : MonoBehaviour {

	private BoxCollider2D groundCollider;
	private float groundHorizontalLength;

	private void Start()
	{
		groundCollider = GetComponent<BoxCollider2D>();
		groundHorizontalLength = groundCollider.size.x;
	}

	private void Update()
	{
		//If the ground has scrolled to its full length to the left
		if(transform.position.x < -groundHorizontalLength)
		{
			RepositionBackground();
		}
	}

	private void RepositionBackground()
	{
		transform.position = new Vector2(groundHorizontalLength, transform.position.y);
	}
}
