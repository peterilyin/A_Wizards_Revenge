using Assets.PixelFantasy.Common.Scripts;
using UnityEngine;
using System.Collections;

namespace Assets.PixelFantasy.PixelMonsters.Common.Scripts.ExampleScripts
{
    [RequireComponent(typeof(Monster))]
    [RequireComponent(typeof(MonsterController2D))]
    [RequireComponent(typeof(MonsterAnimation))]
    public class MonsterControls : MonoBehaviour
    {
        private Monster _monster;
        private MonsterController2D _controller;
        private MonsterAnimation _animation;

        public GameObject character;
        public Vector3 playerPos;

        public bool touchingPlayer;

        public bool canJump;

        public void Start()
        {
            _monster = GetComponent<Monster>();
            _controller = GetComponent<MonsterController2D>();
            _animation = GetComponent<MonsterAnimation>();
            character = GameObject.FindWithTag("Player");

            playerPos = character.transform.position;

        }

        public void Update()
        {

            playerPos = character.transform.position;
            if (!touchingPlayer)
            {
                Move();
            } else
            {
                _controller.Input = Vector2.zero;
            }
            
            Attack();

            // Play other animations, just for example.
            //if (Input.GetKeyDown(KeyCode.I)) { _animation.SetState(MonsterState.Idle); }
            if (Input.GetKeyDown(KeyCode.R)) { _animation.SetState(MonsterState.Ready); }
            //if (Input.GetKeyDown(KeyCode.D)) _animation.SetState(MonsterState.Die);
            //if (Input.GetKeyUp(KeyCode.H)) _animation.Hit();
            //if (Input.GetKeyUp(KeyCode.L)) EffectManager.Instance.Blink(_monster);
        }

        private void Move()
        {
            _controller.Input = Vector2.zero;

            if (playerPos.x < transform.position.x)
            {
                _controller.Input.x = -0.5f;
            }
            else if (playerPos.x > transform.position.x)
            {
                _controller.Input.x = 0.5f;
            }

            if (((playerPos.y - 0.5f) > transform.position.y) && canJump)
            {
                Debug.Log("Jumping");
                _controller.Input.y = 1;
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
                _animation.Attack();
            }

            if (other.tag == "Ground")
            {
                canJump = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(FollowDelay());              
            }
            if (other.tag == "Ground")
            {
                canJump = false;
            }
        }

        IEnumerator FollowDelay()
        {
            //Wait for 0.5 seconds
            yield return new WaitForSeconds(0.5f);
            touchingPlayer = false;
        }


    }
}