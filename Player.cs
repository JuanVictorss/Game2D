using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public int health = 5;
    public float speed;
    public float jumpForce;

    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    private bool isJumping;
    private bool isAttacking;
    private float movement;

    public GameObject charge;
    public Transform fireCharge;

    private Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!isAttacking)
        {
            //se n precionar nada valor é 0. pressionar direita valor maximo é 1. esuqerda valor maximo -1
            movement = Input.GetAxis("Horizontal");

            //adiciona velocidade ao corpo do personagem no eixo x
            rig.velocity = new Vector2(movement * speed, rig.velocity.y);

            //andando pra direita
            if (movement > 0)
            {
                if (!isJumping)
                {
                    anim.SetInteger("transition", 1);
                }
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            //andando pra esquerda
            if (movement < 0)
            {
                if (!isJumping)
                {
                    anim.SetInteger("transition", 1);
                }
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            if (movement == 0 && !isJumping && !isAttacking)
            {
                anim.SetInteger("transition", 0);
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }
    }

    void Attack()
    {
        StartCoroutine("Atk");
    }

    IEnumerator Atk()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextFire && !isJumping)
        {
            rig.velocity = Vector2.zero;
            isAttacking = true;
            Debug.Log(isAttacking);
            anim.SetInteger("transition", 3);
            nextFire = Time.time + fireRate;

            yield return new WaitForSeconds(0.8f);

            GameObject Charge = Instantiate(charge, fireCharge.position, fireCharge.rotation);
            if (transform.rotation.y == 0)
            {
                Charge.GetComponent<Charge>().isRight = true;
            }
            if (transform.rotation.y == 180)
            {
                Charge.GetComponent<Charge>().isRight = false;
            }
            isAttacking = false;
            Debug.Log(isAttacking);

            anim.SetInteger("transition", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }
}