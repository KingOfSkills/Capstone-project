using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVisual : MonoBehaviour
{
    private MeshRenderer zombieMeshRenderer;
    private Color color;
    private float flashTime = .1f;

    private void Start()
    {
        zombieMeshRenderer = GetComponent<MeshRenderer>();
        color = zombieMeshRenderer.material.color;
    }

    public void Flash()
    {
        StartCoroutine(EDamageFlash());
    }

    IEnumerator EDamageFlash() // Damage Indicator
    {
        zombieMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashTime);
        zombieMeshRenderer.material.color = color;
    }
}
