using System;
using UnityEngine;

public class DrivenVector3Spring : ISpring<Vector3>
{
    private readonly Vector3Spring Spring = new(1, 1);
    private readonly Spring.Config config;

    public DrivenVector3Spring(Func<Vector3> RestingPosDriver, float angularFrequency = 1f, float dampingRatio = 1f) : this(angularFrequency, dampingRatio) => this.RestingPosDriver = RestingPosDriver;
    public DrivenVector3Spring(float angularFrequency = 1f, float dampingRatio = 1f) : this(new(angularFrequency, dampingRatio)) { }
    public DrivenVector3Spring(Func<Vector3> RestingPosDriver, Spring.Config config) : this(config) => this.RestingPosDriver = RestingPosDriver;
    public DrivenVector3Spring(Spring.Config config)
    {
        Spring = new(config);
        this.config = config;
        Spring.OnSpringUpdated += f => OnSpringUpdated?.Invoke(f);
    }

    public Vector3 Position { get => Spring.Position; set => Spring.Position = value; }
    public Vector3 Velocity { get => Spring.Velocity; set => Spring.Velocity = value; }
    public Func<Vector3> RestingPosDriver { get; set; }
    public Vector3 RestingPos { get => RestingPosDriver(); set => RestingPosDriver = () => value; }
    public float AngularFrequency { get => Spring.AngularFrequency; set => Spring.AngularFrequency = value; }
    public float DampingRatio { get => Spring.DampingRatio; set => Spring.DampingRatio = value; }

    public event Action<Vector3> OnSpringUpdated;

    public void Step(float deltaTime)
    {
        Spring.RestingPos = RestingPosDriver?.Invoke() ?? Spring.RestingPos;
        Spring.Step(deltaTime);
    }
}