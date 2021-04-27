using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D heroBody;
    public float Moveforce = 100;
    private float fInput = 0.0f;
    public float MaxSpeed = 5;
    private bool bFaceRight=true;
    private bool bGrounded = false;
    private bool bJump = true;
    private float JumpForce = 500f;
    Transform mGroundCheck;
    void Start()

    {
        heroBody=GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("GroundCheck");
        
    }

    // Update is called once per frame
    void Update()

    {
        fInput = Input.GetAxis("Horizontal");
        //转向
        if (fInput > 0 && !bFaceRight)
        {
            flip();
        }
        else if (fInput < 0 && bFaceRight)
        {
            flip();
        }
        //跳跃
        bGrounded = Physics2D.Linecast(transform.position,mGroundCheck.position,
                           1 << LayerMask.NameToLayer("Ground"));
        if(bGrounded)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            heroBody.AddForce(new Vector2(0, JumpForce));
         }
       



    }
    private void FixedUpdate()
    {

        
        //控制移动
        if (fInput * heroBody.velocity.x < MaxSpeed)
        {
            heroBody.AddForce(Vector2.right * fInput * Moveforce);
        }
        //限制最大速度
        if (Mathf.Abs(heroBody.velocity.x) > MaxSpeed)
        {
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * MaxSpeed, heroBody.velocity.y);

        }

        

    }
    void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
}
