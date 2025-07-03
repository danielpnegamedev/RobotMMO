using System;
using UnityEngine;
public class TickManager : MonoBehaviour
{
    public static event Action OnTick;
    private float tickTimer = 0f;
    private float tickInterval = 0.05f; // 50 ms

    void FixedUpdate()
    {
        tickTimer += Time.fixedDeltaTime;

        if (tickTimer >= tickInterval)
        {
            OnTick?.Invoke(); // Chama o evento de tick
            tickTimer -= tickInterval; // Reseta o timer sem perder o tempo extra
        }
    }
}
