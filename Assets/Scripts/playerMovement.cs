using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private enum MovementState {idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x*7f, rb.velocity.y);

        if(Input.GetButtonDown("Jump")){
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, 15f);
        }

        updateGraphics(x);
    }

    private void OnCollisionEnter2D(Collision2D Collision){
        if(Collision.gameObject.CompareTag("trap")){
            deathSoundEffect.Play();
            deathSoundEffect.Play();
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
        }
    }

    private void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void updateGraphics(float x){
        MovementState state;
        if(x>0f){
            state = MovementState.running;
            sr.flipX=false;
        }else if(x<0f){
            state = MovementState.running;
            sr.flipX=true;
        }else{
            state = MovementState.idle;
        }

        if(rb.velocity.y > 0.1f){
            state = MovementState.jumping;
        }else if(rb.velocity.y < -0.1f){
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
} 