using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rd;

    public GameObject bombParticle;

    public Animator animator;

    private bool isBlock = true;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private bool isCanJump; 

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 3.0f;

        Vector3 v = rd.velocity;

        float stick = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.RightArrow) || stick > 0)
        {
            v.x = moveSpeed;
            transform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("Walk", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || stick < 0)
        {
            v.x = -moveSpeed; 
            transform.rotation = Quaternion.Euler(0, -90, 0);
            animator.SetBool("Walk", true);
        }
        else
        {
            v.x = 0;
            animator.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Jump") && isBlock == true)
        {
            v.y += 10.0f;
        }

        if (GoalScript.isGameClear == true)
        {
            v.x = 0; 
            animator.SetBool("Walk", false);
        }

        rd.velocity = v;

        //レイ
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.8f, 0.0f);
        Ray ray = new Ray(rayPosition,Vector3.down);

        float distance = 0.9f;
       
        isBlock = Physics.Raycast(ray, distance);

        if(isBlock == true)
        {
            Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
            animator.SetBool("jump", false);
        }
        else
        {
            Debug.DrawRay(rayPosition, Vector3.down * distance, Color.yellow);
            animator.SetBool("jump", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "COIN")
        {
            other.gameObject.SetActive(false);
            //コインの音の再生
            audioSource.Play();
            //スコアの追加
            GameManagerScript.score += 1;
            //爆発パーティクル発生
            Instantiate(bombParticle, transform.position, Quaternion.identity);
        }
    }
}
