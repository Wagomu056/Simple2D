using System.Collections;
#if TEST
using NUnit.Framework;

namespace Tests
{
    public class ActionStateMachineTest
    {
        [Test]
        public void ChangeState()
        {
            var stateMachine = new Action.StateMachine();
            var actionHigh = new Action.TestHigh(null, null, null);
            var actionLow = new Action.TestLow(null, null, null);
            stateMachine.Register(actionHigh);
            stateMachine.Register(actionLow);

            stateMachine.Start();
            stateMachine.Update();

            // 何もなければデフォルトで開始するLowが選ばれるはず
            Assert.NotNull(stateMachine.GetCurrentState() as Action.TestLow);

            // Highを有効にして切り替わっているか
            actionHigh.IsEnableStart = true;
            stateMachine.Update();
            Assert.NotNull(stateMachine.GetCurrentState() as Action.TestHigh);
        }
    }
}
#endif // TEST