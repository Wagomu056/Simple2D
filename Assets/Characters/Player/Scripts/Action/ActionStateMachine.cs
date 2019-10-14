using System;
using System.Collections.Generic;
using UnityEngine;

namespace Action
{
public class StateMachine
{
    Dictionary<int, State> states = new Dictionary<int, State>();
    int statesCount = 0;

    State currentState;

    public void Register(int index, State state)
    {
        states.Add(index, state);
        statesCount = states.Count;
    }

    public void Start()
    {
        var startState = GetShouldStartState(limitState:null);
        ChangeState(startState);
    }

    public void Update()
    {
        var shouldChangeState = GetShouldStartState(currentState);
        if (shouldChangeState != null)
        {
            ChangeState(shouldChangeState);
        }

        if (currentState == null)
        {
            return;
        }

        currentState.Update();

        if (currentState.ShouldEnd())
        {
            var nextState = GetShouldStartState(limitState:null);
            if (nextState == null)
            {
                Debug.LogError("Not found start state. There must be a state to start.");
                return;
            }
            ChangeState(nextState);
        }
    }

    public void End()
    {
        currentState?.End();
    }

    void ChangeState(State changeState)
    {
        currentState?.End();
        currentState = changeState;
        currentState.Start();
    }

    State GetShouldStartState(State limitState)
    {
        var isSetLimit = (limitState != null);
        for (var checkIdx = 0; checkIdx < statesCount; checkIdx++)
        {
            var state = states[checkIdx];
            if (isSetLimit && state == limitState)
            {
                break;
            }

            if (state.ShouldStart())
            {
                return state;
            }
        }

        return null;
    }

#if TEST
    public void SwapInput(UInput.InputComponet input)
    {
        foreach (var pair in states)
        {
            pair.Value.SwapInput(input);
        }
    }
#endif //TEST
}
} // namespace Action
