using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rig;

    public bool isRight;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRight)
        {
            rig.velocity= Vector2.right * speed;
        }
        else{
            rig.velocity= Vector2.left * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            collision.GetComponent<Skeleton>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
