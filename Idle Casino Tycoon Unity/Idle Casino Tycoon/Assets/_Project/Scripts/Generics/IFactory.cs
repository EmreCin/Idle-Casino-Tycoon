using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory<T, T2, T3>
{
    void Create(int level,T controller, T2 data, Transform parent);

    T3 GetData(int level, T2 data);
}