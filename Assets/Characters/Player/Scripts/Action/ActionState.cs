using UnityEngine;

namespace Action
{
public abstract class State
{
    protected Animator animator;
    protected UInput.InputComponet input;
    protected Rigidbody2D rigid;

    public State(Animator animator, UInput.InputComponet input, Rigidbody2D rigid)
    {
        this.animator = animator;
        this.input = input;
        this.rigid = rigid;
    }

    ~State()
    {
        animator = null;
        input = null;
        rigid = null;
    }

    public virtual void Start(){}

    public virtual void Update(){}

    public virtual void End(){}

    public abstract bool ShouldStart();
    
    public abstract bool ShouldEnd();

#if TEST
    public void SwapInput(UInput.InputComponet input)
    {
        this.input = input;
    }
#endif //TEST
}
} // namespace Action
