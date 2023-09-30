using System;

public class DrivenSpring : ISpring<float>
{
    private readonly BaseSpring Spring;
    private readonly Spring.Config config;

    public DrivenSpring(Func<float> RestingPosDriver, float angularFrequency = 1f, float dampingRatio = 1f) : this(angularFrequency, dampingRatio) => this.RestingPosDriver = RestingPosDriver;
    public DrivenSpring(float angularFrequency = 1f, float dampingRatio = 1f) : this(new(angularFrequency, dampingRatio)) { }
    public DrivenSpring(Func<float> RestingPosDriver, Spring.Config config) : this(config) => this.RestingPosDriver = RestingPosDriver;
    public DrivenSpring(Spring.Config config)
    {
        Spring = new(config);
        this.config = config;
        Spring.OnSpringUpdated += f => OnSpringUpdated?.Invoke(f);
    }


    public float Position { get => Spring.Position; set => Spring.Position = value; }
    public float Velocity { get => Spring.Velocity; set => Spring.Velocity = value; }
    public Func<float> RestingPosDriver
    {
        get;
        set;
    }
    public float RestingPos { get => RestingPosDriver(); set => RestingPosDriver = () => value; }
    public float AngularFrequency { get => config.AngularFrequency; set => config.AngularFrequency = value; }
    public float DampingRatio { get => config.DampingRatio; set => config.DampingRatio = value; }

    public event Action<float> OnSpringUpdated;

    public void Step(float deltaTime)
    {
        Spring.RestingPos = RestingPosDriver?.Invoke() ?? Spring.RestingPos;
        Spring.UpdateDampedSpringMotion(deltaTime);
    }
}