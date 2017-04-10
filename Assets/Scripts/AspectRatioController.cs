using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioController : MonoBehaviour {

	[SerializeField] Vector2 targetAspect = new Vector2(16.0f, 9.0f);
	private float targetAspectRatio, screenRatio;
	private Resolution resolution;

	void Start ()
	{
		ConfigureViewport();
	}

	private void LateUpdate()
	{
		if (Screen.currentResolution.Equals(resolution))
		{
			ConfigureViewport();
		}
	}

	private void ConfigureViewport()
	{
		CheckViewportRatio();
		resolution = Screen.currentResolution;
	}

	private void CheckViewportRatio()
	{
		//Logical orientation of the screen defined to horizontal
		Screen.orientation = ScreenOrientation.Landscape;

		targetAspectRatio = targetAspect.x / targetAspect.y;

		screenRatio = (float)Screen.width / (float)Screen.height;

		float differenceRatio = screenRatio / targetAspectRatio;

		if (differenceRatio < 1.0f)
		{
			ModifyViewport(1.0f, differenceRatio);
		}
		else if (differenceRatio > 1.0f)
		{
			float widthScale = 1.0f / differenceRatio;

			ModifyViewport(widthScale, 1.0f);
		}
	}

	private static void ModifyViewport(float widthScale, float heightScale)
	{
		Rect letterbox = Camera.main.rect;

		letterbox.width = widthScale;
		letterbox.height = heightScale;
		if(widthScale < heightScale)
		{
			letterbox.x = (1.0f - widthScale) / 2.0f;
			letterbox.y = 0.0f;
		}
		else
		{
			letterbox.x = 0.0f;
			letterbox.y = (1.0f - heightScale) / 2.0f;
		}

		Camera.main.rect = letterbox;
	}
}
