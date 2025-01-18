using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class CollisionHandler : MonoBehaviour
{
    bool isContrallable = true;
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    bool isColliable = true;
    private void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "friendly":
                Debug.Log("Everthing is looking good!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void Update() 
    {
        RespondToDebugKeys();
    }
    
    void RespondToDebugKeys()
    {
        if(Keyboard.current.lKey.isPressed)
        {
            LevelLoad();
        }
        else if(Keyboard.current.cKey.isPressed)
        {
            isColliable = !isColliable;
        }
    }

    private void StartSuccessSequence()
    {
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LevelLoad",LevelLoadDelay);
    }

    void StartCrashSequence()
    {
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",LevelLoadDelay);
    }

    void LevelLoad()
        {
            int CurretScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = CurretScene + 1;
            if(nextScene == SceneManager.sceneCountInBuildSettings)
            {
                nextScene = 0;

            }
            SceneManager.LoadScene(nextScene);
        }

        void ReloadLevel()
        {
            int CurretScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(CurretScene);

        }
}
