using System;
using UnityEngine;

namespace UInput
{
[Serializable]
public class InputAxis
{
    public string AxisName {get; private set;}
    
    public InputAxis(string axis)
    {
        AxisName = axis;
    }

    public float GetValue()
    {
        return Input.GetAxis(AxisName);
    }
}
} // namespace UInput

