using UnityEngine;

namespace UInput
{
public class InputKey
{
    public KeyCode Key {get; private set;}

    public InputKey(KeyCode key)
    {
        Key = key;
    }

    public bool GetDown()
    {
        return (Input.GetKeyDown(Key));
    }
}
} // namespace UInput
