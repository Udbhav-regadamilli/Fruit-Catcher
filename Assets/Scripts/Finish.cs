using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name == "Player" && !levelCompleted){
            finishSound.Play();
            levelCompleted=true;
            Invoke("CompleteLevel", 2f);
        }
    }

    private void CompleteLevel(){
        Debug.Log("Level Completed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
