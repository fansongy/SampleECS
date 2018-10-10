using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[System.Serializable]
public struct MoveWithJob : IComponentData
{
    public float Speed;
    public float MinX;

    public MoveWithJob(float x)
    {
        Speed = Random.Range(2f, 5f);
        MinX = x;
    }
}

public class MoveWithJobComponent : ComponentDataWrapper<MoveWithJob> { }

