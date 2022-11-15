using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jump;
    public Rigidbody2D rb;
    public bool isGrounded = true;
    public float speedcap;
    public bool right;
    public float dash;

    private bool hasDashed;
    private bool dashlock;
    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator groundCheck()
    {
        yield return new WaitForSeconds(0.15f);
        
        yield return new WaitForSeconds(0f);
    }
    IEnumerator dashCheck()
    {
        rb.gravityScale = 0;
        dashlock = true;
        yield return new WaitForSeconds(0.15f);
        rb.gravityScale = 5;
        dashlock = false;
    }
    void FixedUpdate()
    {
        RaycastHit2D Lhit = Physics2D.Raycast(new Vector2(transform.position.x - 0.60f, transform.position.y - 0.61f), -Vector2.up, 0.01f);
        RaycastHit2D Rhit = Physics2D.Raycast(new Vector2(transform.position.x + 0.60f, transform.position.y - 0.61f), -Vector2.up, 0.01f);
        Debug.DrawRay(new Vector2(transform.position.x - 0.60f, transform.position.y - 0.61f), -Vector2.up * 0.01f, Color.green);
        Debug.DrawRay(new Vector2(transform.position.x + 0.60f, transform.position.y - 0.61f), -Vector2.up * 0.01f, Color.red);
        if (Lhit.collider != null || Rhit.collider != null)
        {
            isGrounded = true;
            hasDashed = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(groundCheck());
        if (Input.GetKey("left") && dashlock == false)
        {
            right = false;
            if (rb.velocity.x >= -speedcap)
            {
                rb.velocity += new Vector2(-speed, 0.0f);
            }
            //rb.AddForce(-transform.right * speed);
        }
        else if (Input.GetKey("right") && dashlock == false)
        {
            right = true;
            if (rb.velocity.x <= speedcap)
            {
                rb.velocity += new Vector2(speed, 0.0f);
            }
            
            //rb.AddForce(transform.right * speed);
        }
        if (Input.GetKeyDown("up") && isGrounded == true)
        {
            isGrounded = false;
            rb.velocity = new Vector2(0.0f, jump);
            /*
            rb.gravityScale = 0;
            rb.AddForce(transform.up * jump);
            rb.gravityScale = 5;
             */
            Debug.Log("Pog");
            
        }
        if (Input.GetKeyDown("space"))
        {
            if (hasDashed == false)
            {
                if (right is true)
                {
                    rb.velocity = new Vector2(dash, 0.0f);
                    StartCoroutine(dashCheck());
                    hasDashed = true;
                }
                else if (right is false)
                {
                    rb.velocity = new Vector2(-dash, 0.0f);
                    StartCoroutine(dashCheck());
                    hasDashed = true;
                }
            }
            
        }
    }
}
