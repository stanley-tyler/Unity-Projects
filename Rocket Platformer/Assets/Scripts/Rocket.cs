using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;
	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip deathSound;
	[SerializeField] AudioClip victoryNoise;
	[SerializeField] ParticleSystem mainEngineParticles;
	[SerializeField] ParticleSystem deathParticles;
	[SerializeField] ParticleSystem victoryParticles;
	[SerializeField] float loadDelay = 3f;

	private Rigidbody rocketShip;
	private AudioSource audioSource;

	enum State
	{
		Alive, Dying, Trancending
	}
	State state = State.Alive;
	
	void Start () {
		rocketShip = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		if (state == State.Alive)
		{
			RespondToRotateInput();
			RespondToThrustInput();
		}
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (state != State.Alive)
		{
			return;
		}
		switch (collision.gameObject.tag)
		{
			case "Friendly": //do nothing
				break;
			case "Finish":
				VictorySequence();
				break;
			default:
				DeathSequence();
				break;
		}
	}

	private void VictorySequence()
	{
		state = State.Trancending;
		audioSource.Stop();
		audioSource.PlayOneShot(victoryNoise);
		victoryParticles.Play();
		Invoke("LoadNextScene", loadDelay);
	}

	private void DeathSequence()
	{
		float volume = .25f;
		state = State.Dying;
		audioSource.Stop();
		mainEngineParticles.Stop();
		deathParticles.Play();        
		audioSource.PlayOneShot(deathSound, volume);
		//gameObject.SetActive(false);
		Invoke("LoadFirstLevel", loadDelay);
	}

	private void LoadFirstLevel()
	{
		
		SceneManager.LoadScene(0);
	}

	private void LoadNextScene()
	{
		
		SceneManager.LoadScene(1);
	}

	private void RespondToRotateInput()
	{
		rocketShip.freezeRotation = true; //manual control of rotation

		
		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D)))
		{
			
			transform.Rotate(Vector3.forward * rotationThisFrame);
		}

		if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.A)))
		{
			transform.Rotate(-Vector3.forward * rotationThisFrame); ;
		}

		rocketShip.freezeRotation = false; //returns rotation control to physics
	}

	private void RespondToThrustInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			ApplyThrust();
		}
		else
		{
			audioSource.Stop();
			mainEngineParticles.Stop();
		}
	}

	private void ApplyThrust()
	{
		rocketShip.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngine);
			mainEngineParticles.Play();
		}
		
	}
}
