namespace UInput
{
public class InputAutoJump : InputComponet
{
    bool isPushed = false;

    public override float GetHorizontal()
    {
        return 0.0f;
    }

    public override bool GetJump()
    {
        if (isPushed)
        {
            return false;
        }

        isPushed = true;
        return true;
    }
}
} // namespace UInput
