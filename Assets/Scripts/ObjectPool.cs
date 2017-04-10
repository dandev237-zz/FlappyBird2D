using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	[SerializeField] GameObject objectPrefab;
	[SerializeField] int objectPoolSize = 5;
	[SerializeField] float spawnRate = 4.0f;									//Seconds between spawns
	[SerializeField] Vector2 yAxisPositionRange = new Vector2(-1.0f, 3.5f);     //Since Tuples are not supported by Unity yet
	[SerializeField] float xAxisSpawnPosition = 10.0f;
	[SerializeField] Vector2 poolPosition = new Vector2(-15.0f, -25.0f);

	private GameObject[] objects;
	private bool isAPickup = false;
	private System.Type pickupType;
	private Object[] pickupComponents;
	private float timeSinceLastSpawn;
	private int currentObject = 0;

	void Start ()
	{
		objects = new GameObject[objectPoolSize];
		Coin coinComponent = objectPrefab.GetComponent<Coin>();
		if(coinComponent)
		{
			isAPickup = true;
			pickupType = coinComponent.GetType();
			pickupComponents = new Object[objectPoolSize];
		}

		for (int i = 0; i < objectPoolSize; ++i)
		{
			objects[i] = (GameObject)Instantiate(objectPrefab, poolPosition, Quaternion.identity);
			if(coinComponent)
			{
				pickupComponents[i] = objects[i].GetComponent<Coin>();
			}
		}
	}
	
	void Update ()
	{
		timeSinceLastSpawn += Time.deltaTime;

		if(!GameController.Instance.GameOver && timeSinceLastSpawn > spawnRate)
		{
			timeSinceLastSpawn = 0;
			float ySpawnPosition = Random.Range(yAxisPositionRange.x, yAxisPositionRange.y);
			objects[currentObject].transform.position = new Vector2(xAxisSpawnPosition, ySpawnPosition);
	
			if(isAPickup)
			{
				if(pickupType.Equals(typeof(Coin)))
				{
					((Coin)pickupComponents[currentObject]).ShowCoinAgain();
				}
				//'else if' for more pickup types!
			}

			if(++currentObject >= objectPoolSize)
			{
				currentObject = 0;
			}
		}
	}
}
