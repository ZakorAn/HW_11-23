using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobExamples : MonoBehaviour
{
    public struct ModifyIntArrayJob : IJob
    {
        public NativeArray<int> dataArray;

        public void Execute()
        {
            for (int i = 0; i < dataArray.Length; i++)
            {
                if (dataArray[i] > 10)
                {
                    dataArray[i] = 0;
                }
            }
        }
    }

    public struct SumVectorsJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Vector3> positions;
        [ReadOnly] public NativeArray<Vector3> velocities;
        public NativeArray<Vector3> finalPositions;

        public void Execute(int index)
        {
            finalPositions[index] = positions[index] + velocities[index];
        }
    }

    void Start()
    {
        NativeArray<int> intArray = new NativeArray<int>(10, Allocator.TempJob);
        for (int i = 0; i < intArray.Length; i++)
        {
            intArray[i] = Random.Range(1, 20);
        }

        ModifyIntArrayJob modifyJob = new ModifyIntArrayJob
        {
            dataArray = intArray
        };

        JobHandle modifyHandle = modifyJob.Schedule();
        modifyHandle.Complete();

        Debug.Log("Результат после задачи 1:");
        for (int i = 0; i < intArray.Length; i++)
        {
            Debug.Log(intArray[i]);
        }

        intArray.Dispose();

        NativeArray<Vector3> positionsArray = new NativeArray<Vector3>(5, Allocator.TempJob);
        NativeArray<Vector3> velocitiesArray = new NativeArray<Vector3>(5, Allocator.TempJob);
        NativeArray<Vector3> finalPositionsArray = new NativeArray<Vector3>(5, Allocator.TempJob);

        for (int i = 0; i < positionsArray.Length; i++)
        {
            positionsArray[i] = new Vector3(i, i, i);
            velocitiesArray[i] = new Vector3(1, 1, 1);
        }

        SumVectorsJob sumJob = new SumVectorsJob
        {
            positions = positionsArray,
            velocities = velocitiesArray,
            finalPositions = finalPositionsArray
        };

        JobHandle sumHandle = sumJob.Schedule(positionsArray.Length, 1);
        sumHandle.Complete();

        Debug.Log("Результат после задачи 2:");
        for (int i = 0; i < finalPositionsArray.Length; i++)
        {
            Debug.Log(finalPositionsArray[i]);
        }

        positionsArray.Dispose();
        velocitiesArray.Dispose();
        finalPositionsArray.Dispose();
    }
}