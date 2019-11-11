using UnityEngine;

namespace Character
{
public class Player : MonoBehaviour, ISerializationCallbackReceiver 
{    
    Animator animator;
    Rigidbody2D rigid;

    [SerializeField]
    UInput.InputComponet input;

    [SerializeField,HideInInspector]
    Action.StateMachine actionFSM;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        actionFSM = new Action.StateMachine();
        RegisterAction();
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
    
    public void OnBeforeSerialize()
    {
    }
    
    public void OnAfterDeserialize()
    {
        RegisterAction();
    }

    void RegisterAction()
    {
        actionFSM.Register(new Action.Jump(animator, input, rigid));
        actionFSM.Register(new Action.Move(animator, input, rigid));
        actionFSM.Register(new Action.Idle(animator, input, rigid));
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
