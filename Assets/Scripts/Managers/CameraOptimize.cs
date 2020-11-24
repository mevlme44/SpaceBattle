using System;
using UnityEngine;

public class CameraOptimize
{
    public static Tuple<float, float> ComputeResolution()
    {
        var _height = 2f * Camera.main.orthographicSize;
        var _width = _height * Camera.main.aspect;
        return new Tuple<float, float>(_width,_height);
    }
}
