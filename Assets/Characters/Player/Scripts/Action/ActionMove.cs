using UnityEngine;

namespace Action
{
public class Move : State
{   
    public Move(Animator animator, UInput.InputComponet input, Rigidbody2D rigid)
    : base(animator, input, rigid)
    {
    }

    public override void Start()
    {
        animator.SetBool("isRun", true);
    }

    public override void Update()
    {
        float direction = (GetHorizontal() > 0.0f) ? 1.0f : -1.0f;
        float speed = 5.0f;
        
        rigid.velocity = new Vector2((speed * direction), rigid.velocity.y);
    }

    public override void End()
    {
        animator.SetBool("isRun", false);
    }

    public override bool ShouldStart()
    {
        return IsInputting();
    }
    
    public override bool ShouldEnd()
    {
        return !IsInputting();
    }

    bool IsInputting()
    {
        return (GetHorizontal() != 0.0f);
    }

    float GetHorizontal()
    {
        return input.GetHorizontal();
    }
}
} // namespace Action