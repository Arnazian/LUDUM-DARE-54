using System;
using UnityEngine;

public class Vector3Spring : ISpring<Vector3>
{
    private readonly BaseSpring XSpring;
    private readonly BaseSpring YSpring;
    private readonly BaseSpring ZSpring;
    private readonly Spring.Config config;

    public Vector3Spring(float angularFrequency, float dampingRatio) : this(new(angularFrequency, dampingRatio)) { }
    public Vector3Spring(Spring.Config config)
    {
        this.config = config;
        XSpring = new(config);
        YSpring = new(config);
        ZSpring = new(config);
    }

    public Vector3 Position { get => new(XSpring.Position, YSpring.Position, ZSpring.Position); set { XSpring.Position = value.x; YSpring.Position = value.y; ZSpring.Position = value.z; } }
    public Vector3 Velocity { get => new(XSpring.Velocity, YSpring.Velocity, ZSpring.Velocity); set { XSpring.Velocity = value.x; YSpring.Velocity = value.y; ZSpring.Velocity = value.z; } }
    public Vector3 RestingPos { get => new(XSpring.RestingPos, YSpring.RestingPos, ZSpring.RestingPos); set { XSpring.RestingPos = value.x; YSpring.RestingPos = value.y; ZSpring.RestingPos = value.z; } }
    public event Action<Vector3> OnSpringUpdated;


   public float AngularFrequency { get => config.AngularFrequency; set { config.AngularFrequency = value; } }
    public float DampingRatio { get => config.DampingRatio; set { config.DampingRatio = value; } }

    public void Step(float deltaTime)
    {
        XSpring.Step(deltaTime);
        YSpring.Step(deltaTime);
        ZSpring.Step(deltaTime);
        OnSpringUpdated?.Invoke(Position);
    }
}