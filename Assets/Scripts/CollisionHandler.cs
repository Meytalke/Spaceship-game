using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool CollisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugCase();
    }

    void RespondToDebugCase()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CollisionDisable = !CollisionDisable;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || CollisionDisable)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequernce();
                break;
            case "Fuel":
                Debug.Log("You picked up fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void ReloadLevel()
    {
        int cussentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene(cussentSceneIndex);
    }

    void LoadNextLevel()
    {
       int cussentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       int nextSceneIndex = cussentSceneIndex + 1;
       if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
       {
        nextSceneIndex = 0; 
       }
       SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence()
    {
        isTransitioning = true; 
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);  
        crashParticle.Play();
    }

    void StartSuccessSequernce()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);    
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay); 
        successParticle.Play();   
    }
}
