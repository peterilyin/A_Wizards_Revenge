using Assets.PixelFantasy.Common.Scripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using UnityEngine;

namespace Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterController2D))]
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterControls : MonoBehaviour
    {
        private Character _character;
        private CharacterController2D _controller;
        private CharacterAnimation _animation;
        


        public void Start()
        {
            _character = GetComponent<Character>();
            _controller = GetComponent<CharacterController2D>();
            _animation = GetComponent<CharacterAnimation>();

        }

        public void Update()
        {
            Move();
            Attack();


            // Play other animations, just for example.
            if (Input.GetKeyDown(KeyCode.I)) _animation.Idle();
            //if (Input.GetKeyDown(KeyCode.R)) _animation.Ready();
            if (Input.GetKey(KeyCode.Mouse1)) _animation.Block();
            //if (Input.GetKeyDown(KeyCode.C)) _animation.Climb();
            if (Input.GetKeyDown(KeyCode.C)) _animation.Crouch();
            //if (Input.GetKeyDown(KeyCode.U)) _animation.Die();
            if (Input.GetKeyDown(KeyCode.LeftShift)) _animation.Roll();
            //if (Input.GetKeyDown(KeyCode.H)) _animation.Hit();
            //if (Input.GetKeyUp(KeyCode.L)) EffectManager.Instance.Blink(_character);
        }

        private void Move()
        {
            _controller.Input = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
            {
                _controller.Input.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _controller.Input.x = 1;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _controller.Input.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _controller.Input.y = -1;
            }

            
        }

        private void Attack()
        {
            //if (Input.GetKeyDown(KeyCode.J)) _animation.Jab();
            if ((Input.GetButtonDown("Fire1"))) _animation.Slash();
            //if (Input.GetKeyDown(KeyCode.P)) _animation.Push();
            if (Input.GetButtonDown("Fire1")) _animation.Shot();
            if (Input.GetButtonDown("Fire1")) Fire();
            if (Input.GetButtonDown("Fire1")) Fire(power: true);
        }

        private float _fireTime;
        
        public void Fire(bool power = false)
        {
            if (Time.time - _fireTime < 0.5f) return;

            if (_animation.GetState() == CharacterState.Idle)
            {
                _animation.Ready();
            }

            _fireTime = Time.time;

            if (_character.Firearm.Detached)
            {
                _character.Firearm.Transform.gameObject.SetActive(true);
                _character.Firearm.Animator.SetTrigger(power ? "PowerFire" : "Fire");
            }
            else
            {
                _character.Animator.SetTrigger("Fire");
            }

            _character.AudioSource.pitch = Random.Range(0.9f, 1.1f);
            _character.AudioSource.PlayOneShot(EffectManager.Instance.FireAudioClip);
            EffectManager.Instance.CreateSpriteEffect(_character, power ? "FireMuzzleM" : "FireMuzzleS", direction: 1, parent: _character.Firearm.FireMuzzle);
        }




    }
}