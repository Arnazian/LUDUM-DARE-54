using System;
using UnityEngine;

public interface ISpring<T>
{
    T Position { get; set; }
    T Velocity { get; set; }
    T RestingPos { get; set; }
    event Action<T> OnSpringUpdated;
    void Step(float deltaTime);
}

[System.Serializable]
public class Spring
{
    [System.Serializable]
    public class Config
    {
        [field: SerializeField] public float AngularFrequency { get; set; }
        [field: SerializeField] public float DampingRatio { get; set; }

        public Config(float angularFrequency, float dampingRatio)
        {
            AngularFrequency = angularFrequency;
            DampingRatio = dampingRatio;
        }
    }
}