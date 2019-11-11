#if TEST
using UnityEngine;

namespace Action
{
public class TestLow : State
{
    public TestLow(Animator animator, UInput.InputComponet input, Rigidbody2D rigid)
    : base(animator, input, rigid)
    {
    }
    
    public override void Start()
    {
        Debug.Log("Start TestLow");
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
#endif // TEST