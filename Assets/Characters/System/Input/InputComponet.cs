using UnityEngine;

namespace UInput
{
public abstract class InputComponet : MonoBehaviour
{
    public abstract float GetHorizontal();

    public abstract bool GetJump();
}
} // namespace UInput

