using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.PixelFantasy.PixelMonsters.Common.Scripts.ExampleScripts;
using Assets.PixelFantasy.PixelMonsters.Common.Scripts;
//using System.Random;



public class BlackBearManager : MonoBehaviour
{



    private Monster _monster;
    private MonsterController2D _controller;
    private MonsterAnimation _animation;

    public GameObject character;
    public Vector3 playerPos;

    public bool touchingPlayer;

    public bool canJump;
    public bool playerExit;
    public bool takingDamage;


    public int livesCountStart;
    public int currLives;

    public float fadeTime;


    public SpriteRenderer mySR;


    public GameObject AttackBoxObj;

    public float attackCoolDown;

    public bool canAttack;

    public GameObject IronCoin;
    public GameObject GoldCoin;
    public GameObject smallSilverKey;

    //public Random rand;
    public int coinChance;

    public float moveSpeed;

    private Animator myAnimator;
    public Rigidbody2D myRB;

    public float followDelay;

    public float followCounter;

    public float attackDelay;
    public float attackCounter;

    public bool keySpawn;


    public void Start()
    {
        _monster = GetComponent<Monster>();
        _controller = GetComponent<MonsterController2D>();
        _animation = GetComponent<MonsterAnimation>();
        character = GameObject.FindWithTag("Player");

        playerPos = character.transform.position;

        //mySR = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        AttackBoxObj.SetActive(false);

        myAnimator = GetComponent<Animator>();

        myRB = GetComponent<Rigidbody2D>();

        followCounter = followDelay;

        //rand = new Random();
    }

    public void Update()
    {
        Debug.Log(touchingPlayer);

        if (takingDamage)
        {
            followCounter -= Time.deltaTime;
            if (followCounter <= 0)
            {
                takingDamage = false;
                followCounter = followDelay;
            }
        }

        playerPos = character.transform.position;
        if (!touchingPlayer && !takingDamage && playerExit)
        {
            myAnimator.SetBool("Run", true);
            Move();
            
        }
        else
        {
            myAnimator.SetBool("Run", false);
            myRB.velocity = new Vector3(0f, myRB.velocity.y, 0f);
        }

        Attack();

        // Play other animations, just for example.
        //if (Input.GetKeyDown(KeyCode.I)) { _animation.SetState(MonsterState.Idle); }
        //if (Input.GetKeyDown(KeyCode.R)) { _animation.SetState(MonsterState.Ready); }
        //if (Input.GetKeyDown(KeyCode.D)) _animation.SetState(MonsterState.Die);
        //if (Input.GetKeyUp(KeyCode.H)) _animation.Hit();
        //if (Input.GetKeyUp(KeyCode.L)) EffectManager.Instance.Blink(_monster); 

        if (currLives < livesCountStart)
        {
            float randDirection = Random.Range(-0.5f, 0.5f);
            float randomSpeed = Random.Range(5f, 10f);

            if (currLives > 0)
            {
                //_animation.Hit();
                livesCountStart -= 1;

                coinChance = Random.Range(2, 5);

                

                if (coinChance == 4)
                {
                    GameObject currCoin =
                        Instantiate(GoldCoin, transform.position, transform.rotation);
                    Rigidbody2D cCRB = currCoin.GetComponent<Rigidbody2D>();

                    Vector3 move = new Vector3(randDirection * randomSpeed, randomSpeed, 0f);

                    cCRB.velocity = move;
                }
                else
                {

                    GameObject currCoin =
                        Instantiate(IronCoin, transform.position, transform.rotation);
                    Rigidbody2D cCRB = currCoin.GetComponent<Rigidbody2D>();

                    //Instantiate(IronCoin, transform.position, transform.rotation);

                    //currCoin.AddRelativeForce(new Vector2(0f + randDirection, 1f) * randomSpeed);
                    Vector3 move = new Vector3(randDirection * randomSpeed, randomSpeed, 0f);

                    cCRB.velocity = move;
                }

            }
            else
            {
                //myAnimator.SetBool("Die", true); 

                if (keySpawn)
                {
                    GameObject currKey =
                        Instantiate(smallSilverKey, transform.position, transform.rotation);
                    Rigidbody2D cKRB = currKey.GetComponent<Rigidbody2D>();

                    Vector3 move = new Vector3(randDirection * randomSpeed, randomSpeed, 0f);

                    cKRB.velocity = move;

                    keySpawn = false;
                }

                StartCoroutine("FadeAway");


            }
        }

        if (!canAttack)
        {
            
            attackCounter -= Time.deltaTime;

            if (attackCounter <= 0)
            {
                canAttack = true;
            }
        }

        // bool started = false
        // if (!started) {
        // started = true
        // startcorutine
        // }
        // whenever you need to call again, set started to false

        if (canAttack && touchingPlayer)
        {
            
            StartCoroutine("AttackBox");
            touchingPlayer = false;
            canAttack = false;
        }
    }

    private void Move()
    {
       

        if (playerPos.x < transform.position.x)
        {
            myRB.velocity = new Vector3(-moveSpeed, myRB.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);

        }
        else if (playerPos.x > transform.position.x)
        {
            myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);

        }

        
        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    _controller.Input.y = -1;
        //}
    }

    private void Attack()
    {
        //if (Input.GetKeyDown(KeyCode.A)) _animation.Attack();
        //if (Input.GetKeyDown(KeyCode.F)) _animation.Fire();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            touchingPlayer = true;
            playerExit = false;

            //canAttack = true;

            Debug.Log("entering attackBox blackBear");
            //StartCoroutine("AttackBox");
            //canAttack = false;

        }

       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            touchingPlayer = false;
            canAttack = false;
            StartCoroutine(FollowDelay());
        }
        
    }

    IEnumerator FollowDelay()
    {
        //Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        playerExit = true;
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(0.2f);

        while (fadeTime > 0f)
        {
            mySR.color = new Color(1f, 1f, 1f, fadeTime / 100);

            yield return new WaitForSeconds(0.01f);
            fadeTime -= 1;
        }

        Destroy(gameObject);

    }

    IEnumerator AttackBox()
    {
        if (canAttack)
        {
            myAnimator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.1f);
            AttackBoxObj.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            AttackBoxObj.SetActive(false);

            //Debug.Log("waiting for " + attackCoolDown);
            //yield return new WaitForSeconds(attackCoolDown);
            //Debug.Log("done waiting");

            yield return new WaitForSeconds(attackCoolDown);
            myAnimator.SetBool("Attack", false);
            canAttack = false;
            attackCounter = attackDelay;
        }

    }

    IEnumerator AttackCoolDown(int attackCoolDown)
    {
        yield return new WaitForSeconds(0.5f);
    }
}
