using System;

public class BaseSpring : ISpring<float>
{

    public float Position { get; set; } = default;
    public float Velocity { get; set; } = default;
    public float RestingPos { get; set; } = default;

    public float AngularFrequency { get => config.AngularFrequency; set => config.AngularFrequency = value; }
    public float DampingRatio { get => config.DampingRatio; set => config.DampingRatio = value; }

    private readonly Spring.Config config;
    public event Action<float> OnSpringUpdated;


    public BaseSpring(Spring.Config config) { this.config = config; }
    public BaseSpring(float angularFrequency, float dampingRatio) => config = new(angularFrequency, dampingRatio);

    public void Step(float deltaTime) => UpdateDampedSpringMotion(deltaTime);
    public void UpdateDampedSpringMotion(float deltaTime)
    {
        if (AngularFrequency <= 0) return;
        var oldPos = Position - RestingPos;
        var oldVel = Velocity;

        if (DampingRatio < 0.0f) DampingRatio = 0.0f;
        if (AngularFrequency < 0.0f) AngularFrequency = 0.0f;

        if (DampingRatio > 1.0f)
        {
            var za = -AngularFrequency * DampingRatio;
            var zb = AngularFrequency * (float)Math.Sqrt(DampingRatio * DampingRatio - 1.0f);
            var z1 = za - zb;
            var z2 = za + zb;

            var e1 = (float)Math.Exp(z1 * deltaTime);
            var e2 = (float)Math.Exp(z2 * deltaTime);

            var invTwoZb = 1.0f / (2.0f * zb);

            var e1OverTwoZb = e1 * invTwoZb;
            var e2OverTwoZb = e2 * invTwoZb;

            var z1e1OverTwoZb = z1 * e1OverTwoZb;
            var z2e2OverTwoZb = z2 * e2OverTwoZb;

            Position = oldPos * (e1OverTwoZb * z2 - z2e2OverTwoZb + e2) + oldVel * (-e1OverTwoZb + e2OverTwoZb) + RestingPos;
            Velocity = oldPos * ((z1e1OverTwoZb - z2e2OverTwoZb + e2) * z2) + oldVel * (-z1e1OverTwoZb + z2e2OverTwoZb);
            OnSpringUpdated?.Invoke(Position);
            return;
        }
        if (DampingRatio < 1.0f)
        {
            var omegaZeta = AngularFrequency * DampingRatio;
            var alpha = AngularFrequency * (float)Math.Sqrt(1.0f - DampingRatio * DampingRatio);

            var expTerm = (float)Math.Exp(-omegaZeta * deltaTime);
            var cosTerm = (float)Math.Cos(alpha * deltaTime);
            var sinTerm = (float)Math.Sin(alpha * deltaTime);

            var invAlpha = 1.0f / alpha;

            var expSin = expTerm * sinTerm;
            var expCos = expTerm * cosTerm;
            var expOmegaZetaSinOverAlpha = expTerm * omegaZeta * sinTerm * invAlpha;

            Position = oldPos * (expCos + expOmegaZetaSinOverAlpha) + oldVel * (expSin * invAlpha) + RestingPos;
            Velocity = oldPos * (-expSin * alpha - omegaZeta * expOmegaZetaSinOverAlpha) + oldVel * (expCos - expOmegaZetaSinOverAlpha);
            OnSpringUpdated?.Invoke(Position);
            return;
        }
        //Damping == 1f
        {
            var expTerm = (float)Math.Exp(-AngularFrequency * deltaTime);
            var timeExp = deltaTime * expTerm;
            var timeExpFreq = timeExp * AngularFrequency;
            Position = oldPos * (timeExpFreq + expTerm) + oldVel * timeExp + RestingPos;
            Velocity = oldPos * (-AngularFrequency * timeExpFreq) + oldVel * (-timeExpFreq + expTerm);
            OnSpringUpdated?.Invoke(Position);
        }
    }
}