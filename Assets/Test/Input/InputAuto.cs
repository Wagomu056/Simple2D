using UnityEngine;

namespace UInput
{
public class InputAuto : InputComponet
{
    float requestHorizontalDir = 0.0f;
    float requestHorizontalTimeLimit = 0.0f;

    float requestJumpTimeLimit = 0.0f;

    public void RequestHorizontal(float dir, float time)
    {
        requestHorizontalDir = dir;
        requestHorizontalTimeLimit = Time.time + time;
    }

    public void RequestJump(float time)
    {
        requestJumpTimeLimit = Time.time + time;
    }

    public override float GetHorizontal()
    {
        if (Time.time < requestHorizontalTimeLimit)
        {
            return requestHorizontalDir;
        }

        return 0.0f;
    }

    public override bool GetJump()
    {
        return (Time.time <= requestJumpTimeLimit);
    }
}
} // namespace UInput
