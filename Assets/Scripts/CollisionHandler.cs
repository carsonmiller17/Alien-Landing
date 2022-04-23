using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelayTime = 2f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    Rigidbody rb;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        //audioSource.Stop();
    }

    void Update() {
        ProcessLoad();
        ProcessCollisionToggle();
    }

    private void ProcessLoad()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isTransitioning)
            {
                LoadNextLevel();
                isTransitioning = true;
            }
        }
    }

    private void ProcessCollisionToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggles collisions
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) { return; }
        switch(other.gameObject.tag)
        {
            case "Friendly":
            {
                Debug.Log("Collided with Friendly object");
                break;
            }
            case "Finish":
            {
                //Debug.Log("Collided with Finish object");
                
                StartSuccessSequence();
                
                //Invoke("LoadNextLevel", levelDelayTime);
                break;
            }
            case "Fuel":
            {
                Debug.Log("Collided with Fuel object");
                break;
            }
            default:
            {
               
                StartCrashSequence();    
            
                //Debug.Log("Collided with obstacle");
                
                //ReloadLevel();
                break;
            }

        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        successParticles.Play();
        Movement comp = GetComponent<Movement>();//.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        comp.enabled = false;
        Invoke("LoadNextLevel", levelDelayTime);
        //isTransitioning = !isTransitioning;
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        crashParticles.Play();
        Movement comp = GetComponent<Movement>();//.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        comp.enabled = false;
        Debug.Log("<Movement>().enabled = false");
        Invoke("ReloadLevel", levelDelayTime);
        //isTransitioning = !isTransitioning;
    }

    void LoadNextLevel()
    {
        GetComponent<Movement>().enabled = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
