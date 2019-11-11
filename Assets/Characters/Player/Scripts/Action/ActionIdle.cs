using System;
using UnityEngine;

namespace Action
{
public class Idle : State
{
    public Idle(Animator animator, UInput.InputComponet input, Rigidbody2D rigid)
    : base(animator, input, rigid)
    {
    }
    
    public override void Start()
    {
        animator.SetBool("isRun", false);
        rigid.velocity = new Vector2(0.0f, rigid.velocity.y);
    }

    public override void Update()
    {}

    public override void End()
    {}

    public override bool ShouldStart()
    {
        return true;
    }
    
    public override bool ShouldEnd()
    {
        return false;
    }
}
} // namespace Action