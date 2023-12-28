using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffectable
{
    public void StartBurnRoutine(float damage, float duration, float tickTime);
    public IEnumerator BurnRoutine(float damage, float duration, float tickTime);
    public IEnumerator AttributeBuffRoutine();
}
