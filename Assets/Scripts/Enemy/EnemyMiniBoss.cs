using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class EnemyMiniBoss : ViewController, IEnemy
    {
        public float HP = 50;
        public float MovementSpeed = 2.0f;

        public enum States
        {
            FollowingPlayer,
            Warning, //警戒状态
            Dash, //冲向主角
            Wait,//等待
        }

        private Transform playerTrans;

        public FSM<States> FSM = new FSM<States>();
        private void Awake()
        {
            playerTrans = GameObject.Find("Player").transform;

            EnemyGenerator.EnemyCount.Value++;

            FSM.State(States.FollowingPlayer)
                .OnFixedUpdate(()=> 
                {
                    if (playerTrans != null)
                    {
                        var direction = (playerTrans.position - transform.position).normalized;

                        SelfRigidbody2D.velocity = direction * MovementSpeed;

                        if ((playerTrans.position - transform.position).magnitude <= 15)
                        {
                            FSM.ChangeState(States.Warning);
                        }
                    }
                    else
                    {
                        SelfRigidbody2D.velocity = Vector2.zero;
                    }
                });
            FSM.State(States.Warning)
                .OnEnter(()=> 
                {
                    //设置速度为0
                    SelfRigidbody2D.velocity = Vector2.zero;
                })
                .OnUpdate(()=> 
                {
                    //21 ~ 3
                    var frame = 3 + (60 * 3 - FSM.FrameCountOfCurrentState) / 10;

                    if (FSM.FrameCountOfCurrentState / frame % 2 == 0)
                    {
                        Sprite.color = Color.red;
                    }
                    else 
                    {
                        Sprite.color = Color.white;
                    }

                    if (FSM.FrameCountOfCurrentState >= 60 * 3)
                    {
                        FSM.ChangeState(States.Dash);
                    }
                })
                .OnExit(()=>
                {
                    Sprite.color = Color.white;
                });

            Vector3 dashStartPos = Vector3.zero;
            float dashToPlayerDistance = 0f;
            FSM.State(States.Dash)
                .OnEnter(()=> 
                {
                    if (playerTrans != null)
                    {
                        var direction = (playerTrans.position - transform.position).normalized;

                        SelfRigidbody2D.velocity = direction * 15;

                        dashToPlayerDistance = (playerTrans.position - transform.position).magnitude;
                        dashStartPos = transform.position;
                    }
                })
                .OnUpdate(()=> 
                {
                    var distance = (transform.position - dashStartPos).magnitude;
                    if (distance >= dashToPlayerDistance + 5)
                    {
                        FSM.ChangeState(States.Wait);
                    }
                       
                    //if (FSM.FrameCountOfCurrentState >= 60)
                    //{
                    //    FSM.ChangeState(States.FollowingPlayer);
                    //}
                });
            //
            FSM.State(States.Wait)
                .OnEnter(()=> 
                {
                    SelfRigidbody2D.velocity = Vector2.zero;
                })
                .OnUpdate(()=>
                {
                    if (FSM.FrameCountOfCurrentState >= 30)
                    {
                        FSM.ChangeState(States.FollowingPlayer);
                    }
                });
            FSM.StartState(States.FollowingPlayer);
        }


        private void FixedUpdate()
        {
            FSM.FixedUpdate();
        }

        private void Update()
        {
            FSM.Update();
            if (HP <= 0)
            {
                Global.GeneratePowUp(gameObject);
                this.DestroyGameObjGracefully();
            }
        }

        private bool isIngore = false;
        public void Hurt(float value, bool force = false)
        {
            if (isIngore && !force) return;
            isIngore = true;
            Sprite.color = Color.red;
            //
            AudioKit.PlaySound("Hit");
            //
            FloaingTextController.Play(transform.position + Vector3.up * 0.5f, value.ToString());
            //
            ActionKit.Delay(0.2f, () =>
            {
                HP -= value;
                Sprite.color = Color.white;
                isIngore = false;
            }).Start(this);
        }

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
    }
}
