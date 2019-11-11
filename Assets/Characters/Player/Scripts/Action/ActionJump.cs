using System;
using UnityEngine;

namespace Action
{
public class Jump : State
{
    StatusWithAnimFlag status;

    ContactChecker contactChecker;

    // ひっかかりチェック君
    FallingChecker fallingChecker;
    const float ForceDropTime = 0.1f;

    float velX = 0.0f;

    const float MaxVelX = 3.0f;
    const float AddVelX = (MaxVelX / 0.01f);

    public Jump(Animator animator, UInput.InputComponet input, Rigidbody2D rigid)
    : base(animator, input, rigid)
    {
        status = new StatusWithAnimFlag(animator);
        contactChecker = new ContactChecker();
        fallingChecker = new FallingChecker();
    }
    
    public override void Start()
    {
        rigid.velocity += new Vector2(0.0f, 10.0f);
        velX = rigid.velocity.x;
        status.Set(StatusWithAnimFlag.Type.Jumping);

        fallingChecker.Init();
    }

    public override void Update()
    {
        UpdateStatus();
        UpdateRigid();

        fallingChecker.Update(rigid.position.y);
    }

    void UpdateStatus()
    {
        switch(status.Current)
        {
            case StatusWithAnimFlag.Type.Jumping:
            {
                if (rigid.velocity.y < 0.0f)
                {
                    status.Set(StatusWithAnimFlag.Type.Falling);
                }
            }
            break;
            case StatusWithAnimFlag.Type.Falling:
            {
                if (contactChecker.IsTouching(rigid, ContactChecker.Type.Ground))
                {
                    status.Set(StatusWithAnimFlag.Type.Landing);
                }
            }
            break;
        }
    }

    void UpdateRigid()
    {
        velX += AddVelX * input.GetHorizontal() * Time.deltaTime;
        velX = Mathf.Clamp(velX, -MaxVelX, MaxVelX);

        if (contactChecker.IsTouching(rigid, ContactChecker.Type.Right))
        {
            velX = (velX >= 0.0f) ? 0.0f : velX;
        }
        if (contactChecker.IsTouching(rigid, ContactChecker.Type.Left))
        {
            velX = (velX <= 0.0f) ? 0.0f : velX;
        }

        if (fallingChecker.IsNotFalling(ForceDropTime))
        {
            velX = 0.0f;
        }

        rigid.velocity = new Vector2(velX, rigid.velocity.y);
    }

    public override bool ShouldStart()
    {
        return input.GetJump();
    }
    
    public override bool ShouldEnd()
    {
        return (status.Current == StatusWithAnimFlag.Type.Landing);
    }

    [Serializable]
    class StatusWithAnimFlag
    {
        Animator animator;

        public enum Type
        {
            Landing,
            Jumping,
            Falling,
        }
        public Type Current {get; private set;} = Type.Landing;

        public StatusWithAnimFlag(Animator animator)
        {
            this.animator = animator;
        }

        ~StatusWithAnimFlag()
        {
            animator = null;
        }

        public void Set(Type type)
        {
            Current = type;
            SetAnimatorFlagByType(type);
        }

        void SetAnimatorFlagByType(Type type)
        {
            animator.SetBool("isJump", (type == Type.Jumping));
            animator.SetBool("isFall", (type == Type.Falling));
            animator.SetBool("isLand", (type == Type.Landing));
        }
    }

    [Serializable]
    class ContactChecker
    {
        public enum Type : int
        {
            Ground,
            Right,
            Left,
        }
        static readonly int TypeCount = Enum.GetNames(typeof(Type)).Length;

        ContactFilter2D[] filters = new ContactFilter2D[TypeCount];

        public ContactChecker()
        {
            for (var i = 0; i < TypeCount; i++)
            {
                filters[i] = new ContactFilter2D();
                filters[i].useNormalAngle = true;
            }

            filters[(int)Type.Ground].minNormalAngle = 70.0f;
            filters[(int)Type.Ground].maxNormalAngle = 110.0f;

            filters[(int)Type.Right].minNormalAngle = 90.0f;
            filters[(int)Type.Right].maxNormalAngle = 270.0f;

            filters[(int)Type.Left].minNormalAngle = -90.0f;
            filters[(int)Type.Left].maxNormalAngle = 90.0f;
        }

        public bool IsTouching(Rigidbody2D rigid, Type type)
        {
            return rigid.IsTouching(filters[(int)type]);
        }
    }

    [Serializable]
    class FallingChecker
    {
        float y = 0.0f;

        float notFallingTime = 0.0f;

        public void Init()
        {
            y = 0.0f;
            notFallingTime = 0.0f;
        }

        public void Update(float positionY)
        {
            if (Mathf.Approximately(y, positionY))
            {
                notFallingTime += Time.deltaTime;
            }
            else
            {
                notFallingTime = 0.0f;
            }

            y = positionY;
        }

        public bool IsNotFalling(float thresholdTime)
        {
            return (notFallingTime >= thresholdTime);
        }
    }
}
} // namespace Action