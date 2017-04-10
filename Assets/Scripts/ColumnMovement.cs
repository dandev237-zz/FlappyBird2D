using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnMovement : MonoBehaviour {

	[SerializeField] float oscillationDistance = 1.0f;
	[SerializeField] float distanceMargin = 0.5f;
	[SerializeField] float semiPeriod = 1.0f;
	private Vector2 originalPosition;
	private Vector2 targetPosition;

	private void Start()
	{
		originalPosition = transform.localPosition;
		targetPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - oscillationDistance);

		StartCoroutine(MoveColumn());
	}

	IEnumerator MoveColumn()
	{
		Vector2 target = targetPosition;

		while (true)
		{
			while (Vector2.Distance(transform.localPosition, target) > distanceMargin)
			{
				transform.localPosition = Vector2.Lerp(transform.localPosition, target, Time.deltaTime);

				yield return null;
			}

			//Swap target
			if(target == targetPosition)
			{
				target = originalPosition;
			}
			else
			{
				target = targetPosition;
			}

			yield return new WaitForSeconds(semiPeriod);
		}
	}
}