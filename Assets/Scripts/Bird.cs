using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Bird : MonoBehaviour {

	[SerializeField] float upForce = 200.0f;
	[SerializeField] AudioClip flapAudioClip;

	private bool isDead = false;
	private Rigidbody2D birdRigidbody;
	private Animator animator;
	private AudioSource audioSource;

	void Awake ()
	{
		birdRigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update ()
	{
		if(!isDead)
		{
			//Keyboard
			if(Input.GetKeyDown(KeyCode.Space))
			{
				ResetBirdVelocity();
				birdRigidbody.AddForce(new Vector2(0.0f, upForce));
				animator.SetTrigger("Flap");
				PlayAudioClip(flapAudioClip);
			}

			foreach (Touch touch in Input.touches)
			{
				if(touch.phase == TouchPhase.Began)
				{
					ResetBirdVelocity();
					birdRigidbody.AddForce(new Vector2(0.0f, upForce));
					animator.SetTrigger("Flap");
					PlayAudioClip(flapAudioClip);
				}
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(!other.collider.gameObject.CompareTag("Sky") && !isDead)
		{
			birdRigidbody.constraints = RigidbodyConstraints2D.None;
			isDead = true;
			animator.SetTrigger("Die");

			GameController.Instance.BirdDied();
		}
		
	}

	private void ResetBirdVelocity()
	{
		birdRigidbody.velocity = Vector2.zero;
	}

	private void PlayAudioClip(AudioClip clip)
	{
		audioSource.Stop();
		audioSource.PlayOneShot(clip);
	}
}
