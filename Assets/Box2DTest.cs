using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential)] [System.Serializable]
public struct b2WorldId
{
    public ushort index1; 
    public ushort generation; 
}

[StructLayout(LayoutKind.Sequential)] [System.Serializable]
public struct b2BodyId
{
    public int index1;
    public ushort world0;
    public ushort generation;
}

static class B2 {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    const string DLL = "box2d_unity"; // 파일명과 일치(확장자 제외)
    const CallingConvention CC = CallingConvention.Cdecl;

    [DllImport(DLL, CallingConvention = CC)] public static extern b2WorldId b2u_CreateWorld(float gx, float gy);
    [DllImport(DLL, CallingConvention = CC)] public static extern b2WorldId b2u_WorldStep(b2WorldId w, float dt, int subSteps);
    [DllImport(DLL, CallingConvention = CC)] public static extern b2BodyId b2u_CreateBody(b2WorldId w);
    [DllImport(DLL, CallingConvention = CC)] public static extern void b2u_GetBody(b2BodyId w, out float out_x, out float out_y, out float angle);
#endif
}

public class Box2DTest : MonoBehaviour {
    public b2WorldId _w;
    public b2BodyId _b;
    
    public Transform _c;

    void Awake()
    {
        _w = B2.b2u_CreateWorld(0f, -10f);
        _b = B2.b2u_CreateBody(_w);
    }

    private void FixedUpdate()
    {
        B2.b2u_WorldStep(_w, Time.fixedDeltaTime, 4);
    }

    private void Update()
    {
        B2.b2u_GetBody(_b, out var x, out var y, out var angle);
        _c.position = new Vector3(x, y, 0);
    }
}

