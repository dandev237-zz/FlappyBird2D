using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour {

	[SerializeField] GameObject gameOverText;
	[SerializeField] AudioClip backgroundMusic;
	[SerializeField] AudioClip scoreAudioClip;
	[SerializeField] AudioClip deathAudioClip;
	[SerializeField] Text scoreText;
	private AudioSource[] audioSources;
	private int score = 0;

	[SerializeField] float backgroundScrollSpeed = -3.5f;
	public float BackGroundScrollSpeed
	{
		get
		{
			return backgroundScrollSpeed;
		}
	}

	private bool mGameOver = false;
	public bool GameOver
	{
		get
		{
			return mGameOver;
		}

		set
		{
			mGameOver = value;
		}
	}

	private static GameController mInstance;
	public static GameController Instance
	{
		get
		{
			return mInstance;
		}
	}

	void Awake()
	{
		//Singleton pattern implementation to avoid having two GameControllers simultaneously
		if (Instance == null)
		{
			mInstance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}

		audioSources = GetComponents<AudioSource>();
	}

	private void Start()
	{
		//Transparent objects will be sorted based on distance along the camera's view
		Camera.main.transparencySortMode = TransparencySortMode.Orthographic;

		PlayAudioClip(backgroundMusic, audioSources[0]);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}

		if (GameOver && Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		foreach (Touch touch in Input.touches)
		{
			if (GameOver && touch.phase == TouchPhase.Began)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}	
	}

	public void BirdDied()
	{
		gameOverText.SetActive(true);
		PlayAudioClip(deathAudioClip, audioSources[1]);
		audioSources[0].Stop();
		mGameOver = true;
	}

	public void BirdScored()
	{
		if(GameOver)
		{
			return;
		}

		PlayAudioClip(scoreAudioClip, audioSources[1]);
		scoreText.text = "Score: " + ++score;
	}

	private void PlayAudioClip(AudioClip clip, AudioSource source)
	{
		if(source.isPlaying)
		{
			source.Stop();
		}
		source.PlayOneShot(clip);
	}
}