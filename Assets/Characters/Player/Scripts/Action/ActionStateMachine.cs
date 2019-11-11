using System;
using System.Collections.Generic;
using UnityEngine;

namespace Action
{
[Serializable]
public class StateMachine
{
    List<State> states = new List<State>();

    State currentState;

    int statesCount = 0;

    public void Register(State state)
    {
        states.Add(state);
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
        foreach (var state in states)
        {
            state.SwapInput(input);
        }
    }

    public State GetCurrentState()
    {
        return currentState;
    }
#endif //TEST
}
} // namespace Action
