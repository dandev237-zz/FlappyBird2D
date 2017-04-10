using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	[SerializeField] Canvas startMenu;
	[SerializeField] Canvas quitMenu;
	[SerializeField] Button playButton;
	[SerializeField] Button optionsButton;
	[SerializeField] Button exitButton;

	private void Start()
	{
		quitMenu.enabled = false;
	}

	public void ExitPressed()
	{
		startMenu.enabled = false;
		playButton.enabled = false;
		optionsButton.enabled = false;
		exitButton.enabled = false;
		quitMenu.enabled = true;
	}

	public void NoPressed()
	{
		startMenu.enabled = true;
		playButton.enabled = true;
		optionsButton.enabled = true;
		exitButton.enabled = true;
		quitMenu.enabled = false;
	}

	public void YesPressed()
	{
		Application.Quit();
	}

	public void PlayPressed()
	{
		SceneManager.LoadScene("MainLevel");
	}
}
