using UnityEngine;

public class DestroyParticleSystemAfterFinish : MonoBehaviour
{
    public ParticleSystem ps;
    public float          timeLeft;

    public void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}