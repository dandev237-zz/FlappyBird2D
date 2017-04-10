﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Column : MonoBehaviour, IScorable {

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Bird>())
		{
			(this as IScorable).OnScored();
		}
	}

	void IScorable.OnScored()
	{
		GameController.Instance.BirdScored();
	}
}
