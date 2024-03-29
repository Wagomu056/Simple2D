﻿using System;
using UnityEngine;

namespace UInput
{
[Serializable]
public class InputButton
{
    public string ButtonName {get; private set;}

    public InputButton(string buttonName)
    {
        ButtonName = buttonName;
    }

    public bool GetDown()
    {
        return (Input.GetButtonDown(ButtonName));
    }
}
} // namespace UInput
