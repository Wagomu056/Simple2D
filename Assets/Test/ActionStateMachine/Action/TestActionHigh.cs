#if TEST
using UnityEngine;

namespace Action
{
public class TestHigh : State
{
    public bool IsEnableStart {get; set;} = false;

    public TestHigh(Animator animator, UInput.InputComponet input, Rigidbody2D rigid)
    : base(animator, input, rigid)
    {
    }
    
    public override void Start()
    {
        Debug.Log("Start TestHigh");
    }

    public override void Update()
    {}

    public override void End()
    {}

    public override bool ShouldStart()
    {
        return IsEnableStart;
    }
    
    public override bool ShouldEnd()
    {
        return false;
    }
}
} // namespace Action
#endif // TEST