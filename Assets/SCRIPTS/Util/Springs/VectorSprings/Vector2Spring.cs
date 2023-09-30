using System;
using UnityEngine;

public class Vector2Spring : ISpring<Vector2>
{
    private readonly BaseSpring XSpring;
    private readonly BaseSpring YSpring;
    private readonly Spring.Config config;

    public Vector2Spring(float angularFrequency, float dampingRatio) : this(new(angularFrequency, dampingRatio)) { }
    public Vector2Spring(Spring.Config config)
    {
        this.config = config;
        XSpring = new(config);
        YSpring = new(config);
    }

    public Vector2 Position { get => new(XSpring.Position, YSpring.Position); set { XSpring.Position = value.x; YSpring.Position = value.y; } }
    public Vector2 Velocity { get => new(XSpring.Velocity, YSpring.Velocity); set { XSpring.Velocity = value.x; YSpring.Velocity = value.y; } }
    public Vector2 RestingPos { get => new(XSpring.RestingPos, YSpring.RestingPos); set { XSpring.RestingPos = value.x; YSpring.RestingPos = value.y; } }
    public event Action<Vector2> OnSpringUpdated;

    public float AngularFrequency { get => config.AngularFrequency; set { config.AngularFrequency = value; } }
    public float DampingRatio { get => config.DampingRatio; set { config.DampingRatio = value; } }

    public void Step(float deltaTime)
    {
        XSpring.Step(deltaTime);
        YSpring.Step(deltaTime);
        OnSpringUpdated?.Invoke(Position);
    }
}
