using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.PixelFantasy.PixelMonsters.Common.Scripts.ExampleScripts;
using Assets.PixelFantasy.PixelMonsters.Common.Scripts;
using Assets.PixelFantasy.Common.Scripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;

    public Transform groundCheckSpot;
    public float groundCheckRadius;
    public LayerMask whatLayerIsGround;

    public Rigidbody2D myRB;
    public bool canMove;
    public Vector3 respawnPos;
    public bool isGrounded;
    public bool isCrouched;

    public float RBY;

    public bool facingRight;

    public GameObject CrouchBox;

    public GameObject CrystalWandOrb;

    public bool remove;

    private LevelManager levelManager;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private CharacterAnimation _animation;

    private Animator myAnimator;


    private bool _jump;
    private bool _crouch;
    private float xInput;

    private BoxCollider2D myBC;

    public float orbCooldown;
    private float orbCooldownCounter;

    

    void Start()
    {
        //want to get our rigidbody
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        _animation = GetComponent<CharacterAnimation>();

        respawnPos = transform.position;

        levelManager = FindObjectOfType<LevelManager>();

        canMove = true;

        myBC = gameObject.GetComponent<BoxCollider2D>();
        myBC.enabled = true;

        CrouchBox.SetActive(false);

        facingRight = true;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            RBY = myRB.velocity.y;
            isGrounded = Physics2D.OverlapCircle(
                   groundCheckSpot.position,
                   groundCheckRadius,
                   whatLayerIsGround);

            myAnimator.SetBool("isGrounded", isGrounded);

            orbCooldown = (5f - PlayerPrefs.GetInt("AmountUpgrade")) / 10;
            orbCooldownCounter -= Time.deltaTime;

            if (orbCooldownCounter <= 0f && Input.GetButtonDown("Fire1"))
            {
                orbCooldownCounter = orbCooldown;

                _animation.Slash();
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                GameObject currOrb =
                            Instantiate(CrystalWandOrb, pos, transform.rotation);
                Rigidbody2D cORB = currOrb.GetComponent<Rigidbody2D>();

                if (facingRight)
                {
                    Vector3 move = new Vector3(8f, 0f, 0f);
                    cORB.velocity = move;

                }
                else
                {
                    Vector3 move = new Vector3(-8f, 0f, 0f);
                    cORB.velocity = move;

                }


            }


            if (Input.GetAxisRaw("Horizontal") > 0f)
            {

                myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0f);

                transform.localScale = new Vector3(1f, 1f, 1f); //or Vector3.one

                facingRight = true;
            }


            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRB.velocity = new Vector3(-moveSpeed, myRB.velocity.y, 0f);

                transform.localScale = new Vector3(-1f, 1f, 1f);

                facingRight = false;
            }

            else
            {
                myRB.velocity = new Vector3(0f, myRB.velocity.y, 0f);
            }


            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRB.velocity = new Vector3(myRB.velocity.x, jumpHeight, 0f);
                //jumpSound.Play();
                _jump = true;
                _animation.Jump();
            }

            if (Input.GetAxisRaw("Vertical") < 0f)
            {
                //we should crouch
                isCrouched = true;
                _animation.Crouch();
            }
            else if (Input.GetAxisRaw("Vertical") > 0f)
            {
                //we should uncrouch
                isCrouched = false;

            }

            if (myRB.velocity.y < -5f)
            {
                _jump = true;
                _animation.Fall();
            }
            if (myRB.velocity.x == 0 && myRB.velocity.y == 0)
            {
                if (isCrouched)
                {
                    _animation.Crouch();

                    CrouchBox.SetActive(true);
                    myBC.enabled = false;
                }
                else
                {
                    _animation.Ready();
                    myBC.enabled = true;
                    CrouchBox.SetActive(false);
                }
            }
            else if (myRB.velocity.y > -0.5f && myRB.velocity.y < 0.5f && myRB.velocity.x != 0)
            {
                myAnimator.SetBool("Fall", false);
                if (isCrouched)
                {
                    _animation.Crawl();

                    CrouchBox.SetActive(true);
                    myBC.enabled = false;
                }
                else
                {

                    _animation.Run();

                    myBC.enabled = true;
                    CrouchBox.SetActive(false);

                }
            }

            if (isGrounded)
            {
                _jump = false;

            }

            //now that our counter is over
            //we can take damage again
            levelManager.invincibilityFrames = false;



            if (isGrounded)
            {

                //if (!_jump)
                //{
                //    if (Input.x == 0)
                //    {

                //    }
                //    else
                //    {
                //        if (isCrouched)
                //        {
                //            _animation.Crawl();
                //        }
                //        else
                //        {
                //            _animation.Run();
                //        }
                //    }
                //}

                //if (Input.y > 0 && !_jump)
                //{
                //    _jump = true;
                //_rigidbody.AddForce(Vector2.up * JumpForce);

                //}
            }
            else
            {
                //velocity.y -= Gravity * Time.fixedDeltaTime;

                //if (velocity.y < 0)
                //{

                //}
            }



        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            _animation.Land();
        }

        
    }


}

