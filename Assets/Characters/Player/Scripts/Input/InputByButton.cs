using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UInput
{
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
