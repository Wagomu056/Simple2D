namespace UInput
{
public class InputAutoMove : InputComponet
{
    public override float GetHorizontal()
    {
        return 1.0f;
    }

    public override bool GetJump()
    {
        return false;
    }
}
} // namespace UInput
