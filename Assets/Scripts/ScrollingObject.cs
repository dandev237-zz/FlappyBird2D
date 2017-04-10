using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScrollingObject : MonoBehaviour {

	private Rigidbody2D objectRigidbody;

	void Start ()
	{
		objectRigidbody = GetComponent<Rigidbody2D>();
		objectRigidbody.velocity = new Vector2(GameController.Instance.BackGroundScrollSpeed, 0.0f);
	}
	
	void Update () {
		if(GameController.Instance.GameOver)
		{
			objectRigidbody.velocity = Vector2.zero;
		}
	}
}
