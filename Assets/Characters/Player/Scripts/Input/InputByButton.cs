using System;

namespace UInput
{
[Serializable]
public class InputByButton : InputComponet
{
    InputAxis Horizontal = new InputAxis("Horizontal");
    InputButton Jump = new InputButton("Jump");
    public override float GetHorizontal()
    {
        return Horizontal.GetValue();
    }

    public override bool GetJump()
    {
        return Jump.GetDown();
    }
}
} // namespace Input
