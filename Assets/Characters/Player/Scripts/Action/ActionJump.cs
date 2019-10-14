using UnityEngine;

namespace Action
{
public class Jump : State
{
    StatusWithAnimFlag status;
    ContactFilter2D filter2D = new ContactFilter2D();

    float velX = 0.0f;

    const float MaxVelX = 5.0f;
    const float AddVelX = (MaxVelX / 0.2f);

    public Jump(Animator animator, UInput.InputComponet input, Rigidbody2D rigid)
    : base(animator, input, rigid)
    {
        status = new StatusWithAnimFlag(animator);
    }
    
    public override void Start()
    {
        rigid.velocity += new Vector2(0.0f, 5.0f);
        velX = rigid.velocity.x;
        status.Set(StatusWithAnimFlag.Type.Jumping);
    }

    public override void Update()
    {
        UpdateStatus();
        UpdateRigid();
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
                if (rigid.IsTouching(filter2D))
                {
                    status.Set(StatusWithAnimFlag.Type.Landing);
                }
            }
            break;
        }
    }

    void UpdateRigid()
    {
        if (status.Current != StatusWithAnimFlag.Type.Falling)
        {
            return;
        }

        velX += AddVelX * input.GetHorizontal() * Time.deltaTime;
        velX = Mathf.Clamp(velX, -MaxVelX, MaxVelX);
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
}
} // namespace Action