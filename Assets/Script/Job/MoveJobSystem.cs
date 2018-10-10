using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class MoveJobSystem : JobComponentSystem {
   
    [BurstCompile]
    struct MoveJob : IJobProcessComponentData<Position, Rotation, MoveWithJob>
    {
        public float deltaTimeFromMainThread;

        public void Execute(ref Position pos, ref Rotation rot, [ReadOnly]ref MoveWithJob move)
        {

            float3 posf = pos.Value;
            if (posf.x + move.Speed * deltaTimeFromMainThread > -move.MinX)
            {
                posf.x = move.MinX;
            }
            else
            {
                posf.x = posf.x + move.Speed * deltaTimeFromMainThread;
            }
            pos.Value = posf;

            rot.Value = quaternion.EulerXYZ(new float3(0, -90, 0));
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        MoveJob mj = new MoveJob()
        {
            deltaTimeFromMainThread = Time.fixedDeltaTime
        };
        JobHandle moveHandle = mj.Schedule(this, inputDeps);
        return moveHandle;
    }

}
