using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncStateCondition : StateCondition
{
    public override bool IsConditionSatisfied()
    {
        return returnValue.Invoke();
    }

    private Func<bool> returnValue;

    public FuncStateCondition(Func<bool> returnValue)
    {
        this.returnValue = returnValue;
    }
}
