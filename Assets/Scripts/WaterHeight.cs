using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// An attempt at making noise that matches the water shader
/// 
/// If I were to attempt again I would try either calculate the displacements on the GPU through a compute shader then send back here
/// or calculate the displacement here on the cpu, then send the displacements to the shader graph on the gpu
/// </summary>
public class WaterHeight : MonoBehaviour
{
    [Header("Variables matching the water shader's material")]
    [SerializeField] float MacroAmplitude1;
    [SerializeField] float MacroAmplitude2;

    [SerializeField] Vector2 MacroVelocity1;
    [SerializeField] Vector2 MacroVelocity2;

    [SerializeField] float MacroFrequency1;
    [SerializeField] float MacroFrequency2;

    Material mat;
    float time;

    void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        time = Time.timeSinceLevelLoad;
        mat.SetFloat("_MyTime", time);    
    }

    void OnDrawGizmosSelected()
    {
        for (int i = -500; i <= 500; i += 20)
        {
            for (int j = -500; j <= 500; j += 20)
            {
                float height = GetWaterHeightAtPosition(new Vector3(i, 0f, j));
                Gizmos.DrawSphere(new Vector3(i, height, j), 1f);
            }
        }
    }

    float GetWaterHeightAtPosition(Vector3 position)
    {
        Vector3 localPos = transform.InverseTransformPoint(position);

        float u = (localPos.x / 10f) + 0.5f;
        float v = (localPos.z / 10f) + 0.5f;
        Vector2 uv = new(u, v);

        return GetWaterHeight(uv);
    }

    float GetWaterHeight(Vector2 uv)
    {
        float macroHeight1 = GetScrollingNoise(uv, MacroVelocity1, MacroFrequency1, MacroAmplitude1);
        float macroHeight2 = GetScrollingNoise(uv, MacroVelocity2, MacroFrequency2, MacroAmplitude2);

        return macroHeight1 + macroHeight2;
    }

    float GetScrollingNoise(Vector2 uv, Vector2 velocity, float frequency, float affect)
    {
        Vector2 offset = time * velocity;
        Vector2 tiledAndOffsetUV = TilingAndOffset(uv, Vector2.one * frequency, offset);

        float height = GradientNoiseShaderNode.GradientNoise(tiledAndOffsetUV, 1f);
        height *= affect;

        return height;
    }

    Vector2 TilingAndOffset(Vector2 uv, Vector2 tiling, Vector2 offset)
    {
        return (uv * tiling) + offset;
    }
}