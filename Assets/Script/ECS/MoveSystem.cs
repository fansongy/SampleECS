using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public class MoveSystem : ComponentSystem {

    struct Group
    {
        public ComponentDataArray<Position> position;
        public ComponentDataArray<Rotation> rotation;
        [ReadOnly] public ComponentDataArray<Move> move;
        public readonly int Length;
    }

    [Inject]
    Group entities;

    protected override void OnUpdate()
    {
        for (int i = 0; i < entities.Length; ++i)
        {
            Position pos = entities.position[i];
            Move move = entities.move[i];

            if (pos.Value.x + move.Speed * Time.deltaTime > -move.MinX)
            {
                pos.Value.x = move.MinX;
            }
            else
            {
                pos.Value.x = pos.Value.x + move.Speed * Time.fixedDeltaTime;
            }

            entities.position[i] = pos;
            Rotation r = new Rotation();
            r.Value = quaternion.EulerXYZ(new float3(0,-90,0));
            entities.rotation[i] = r;
        }
    }
}
