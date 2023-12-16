using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory<T, T2, T3>
{
    void Create(T controller, T2 data, T3 parent);

}