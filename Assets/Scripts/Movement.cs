using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;
    Rigidbody rigidbody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTheust();
        ProcessRotation();
    }

    void ProcessTheust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            startTrusting();
        }
        else
        {
            stopTrusting();
            
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ratateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ratateRight();
        }
        else
        {
            stopRotation();
        }
    }
    
    void startTrusting()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }  
            if (!mainEngineParticle.isPlaying)
            {
                mainEngineParticle.Play();
            } 
    }

    void stopTrusting()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }

    void ratateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticle.isPlaying)
        {
            rightThrusterParticle.Play();
        }   
    }

    void ratateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticle.isPlaying)
        {
            leftThrusterParticle.Play();
        } 
    }

    void stopRotation()
    {
        rightThrusterParticle.Stop();
        leftThrusterParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidbody.freezeRotation = false;
    }
}
