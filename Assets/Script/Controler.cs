using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine.Jobs;
using Unity.Jobs;

[System.Serializable]
public enum LoadType
{
    Classic,
    PureECS,
    Job,
}

public class Controler : MonoBehaviour {

    public float depthMin = -6;
    public float depthMix = 10;
    public int addCount = 10;

    public LoadType loadType = LoadType.Classic;
    public GameObject moveClassic;
    public GameObject moveECS;
    public GameObject moveECSWithJobs;
    public FPS fps;
    public bool willRender=false;
    public Mesh renderMesh;
    public Material renderMat;

    private Vector3 randomInitPosLeft()
    {
        float z = UnityEngine.Random.Range(depthMin, depthMix);
        float xMin = -z - 12;
        return new Vector3(xMin, 0, z);
    }

    private float3 randomInitPosLeftF3()
    {
        float z = UnityEngine.Random.Range(depthMin, depthMix);
        float xMin = -z - 12;
        return new float3(xMin, 0, z);
    }

    public void AddGameObjectClassic(int addCount)
    {
        for (int i = 0; i < addCount; ++i)
        {
            var go = Instantiate(moveClassic);
            go.transform.position = randomInitPosLeft();
            go.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (willRender)
            {
                var mr = go.AddComponent<MeshRenderer>();
                mr.material = renderMat;
                var mesh = go.AddComponent<MeshFilter>();
                mesh.mesh = renderMesh;
            }
        }
        fps.AddElementCount(addCount);
    }

    public void AddECS(int addCount)
    {
        NativeArray<Entity> entities = new NativeArray<Entity>(addCount, Allocator.Temp);

        EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();
        manager.Instantiate(moveECS, entities);
        for (int i = 0; i < addCount; ++i)
        {
            var pos = randomInitPosLeftF3();
            manager.SetComponentData(entities[i], new Position { Value = pos });
            manager.SetComponentData(entities[i], new Move(pos.x));
            if (willRender)
            {
                manager.AddSharedComponentData(entities[i], new MeshInstanceRenderer() { mesh = renderMesh, material = renderMat });
            }
        }

        entities.Dispose();
        fps.AddElementCount(addCount);
    }

    public void AddECSWithJobs(int addCount)
    {
        if(willRender && addCount > 1000)
        {
            addCount = 1000;
        }
        NativeArray<Entity> entities = new NativeArray<Entity>(addCount, Allocator.Temp);
        EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();
        manager.Instantiate(moveECSWithJobs, entities);
        for (int i = 0; i < addCount; ++i)
        {
            var pos = randomInitPosLeftF3();
            manager.SetComponentData(entities[i], new Position { Value = pos });
            manager.SetComponentData(entities[i], new MoveWithJob(pos.x));
            if (willRender)
            {
                manager.AddSharedComponentData(entities[i], new MeshInstanceRenderer() { mesh = renderMesh, material = renderMat });
            }
        }

        entities.Dispose();
        fps.AddElementCount(addCount);
    }


}
