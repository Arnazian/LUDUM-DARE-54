using System;
using UnityEngine;

public class DrivenVector2Spring : ISpring<Vector2>
{
    private readonly Vector2Spring Spring = new(1, 1);
    private readonly Spring.Config config;

    public DrivenVector2Spring(Func<Vector2> RestingPosDriver, float angularFrequency = 1f, float dampingRatio = 1f) : this(angularFrequency, dampingRatio) => this.RestingPosDriver = RestingPosDriver;
    public DrivenVector2Spring(float angularFrequency = 1f, float dampingRatio = 1f) : this(new(angularFrequency, dampingRatio)) { }
    public DrivenVector2Spring(Func<Vector2> RestingPosDriver, Spring.Config config) : this(config) => this.RestingPosDriver = RestingPosDriver;
    public DrivenVector2Spring(Spring.Config config)
    {
        Spring = new(config);
        this.config = config;
        Spring.OnSpringUpdated += f => OnSpringUpdated?.Invoke(f);
    }

    public Vector2 Position { get => Spring.Position; set => Spring.Position = value; }
    public Vector2 Velocity { get => Spring.Velocity; set => Spring.Velocity = value; }
    public Func<Vector2> RestingPosDriver { get; set; }
    public Vector2 RestingPos { get => RestingPosDriver(); set => RestingPosDriver = () => value; }
    public float AngularFrequency { get => Spring.AngularFrequency; set => Spring.AngularFrequency = value; }
    public float DampingRatio { get => Spring.DampingRatio; set => Spring.DampingRatio = value; }

    public event Action<Vector2> OnSpringUpdated;

    public void Step(float deltaTime)
    {
        Spring.RestingPos = RestingPosDriver?.Invoke() ?? Spring.RestingPos;
        Spring.Step(deltaTime);
    }
}