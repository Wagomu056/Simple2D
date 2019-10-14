using UnityEngine;

namespace Character
{
public class Player : MonoBehaviour
{    
    [SerializeField]
    UInput.InputComponet input;

    Animator animator;
    Rigidbody2D rigid;

    Action.StateMachine actionFSM;

    enum ActionIndex
    {
        Jump,
        Move,
        Idle,
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        actionFSM = new Action.StateMachine();
        actionFSM.Register((int)ActionIndex.Jump, new Action.Jump(animator, input, rigid));
        actionFSM.Register((int)ActionIndex.Move, new Action.Move(animator, input, rigid));
        actionFSM.Register((int)ActionIndex.Idle, new Action.Idle(animator, input, rigid));
    }

    void OnEnable()
    {
        actionFSM.Start();
    }

    void OnDisable()
    {
        actionFSM.End();
    }

    void Update()
    {
        actionFSM.Update();
    }

#if TEST
    public void SwapInput(UInput.InputComponet input)
    {
        this.input = input;
        actionFSM.SwapInput(input);
    }

    public bool CheckAnimatorStateName(string clipName)
    {
        string layerName = "Base Layer.";
        return animator.GetCurrentAnimatorStateInfo(0).IsName(layerName + clipName);
    }
#endif
}
} // namespace Character
