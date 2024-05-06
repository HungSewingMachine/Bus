
using System;
using System.Collections.Generic;

public enum Color
{
    Red,
    Green,
    Blue,
    Yellow,
    Gray,
    Pink,
}

public enum CellOrientation
{
    Up,
    Down,
    Left,
    Right,
}

[Serializable]
public class Passenger
{
    public Color color;
}

[Serializable]
public class Cell
{
    public CellOrientation cellOrientation;
    public List<Passenger> passengers;
    public bool isDisable;
    public bool isTunnel;
}

[Serializable]
public class CameraPosition
{
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class CameraRotation
{
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class CameraPreset
{
    public CameraPosition cameraPosition;
    public CameraRotation cameraRotation;
    public double cameraFov;
}

[Serializable]
public class BusConfig
{
    public int color;
    public int passengerCapacity;
    public int reservedCount;
}

[Serializable]
public class CellArray
{
    public List<Cell> cellArray;
}

[Serializable]
public class SavedData
{
    public int gridX;
    public int gridY;
    public List<CellArray> cellMatrix;
    public List<BusConfig> busConfigs;
    public CameraPreset cameraPreset;
}