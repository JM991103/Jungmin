using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Shader : TestBase
{
    public GameObject phaseSlime;
    public GameObject phaseReverseSlime;
    public GameObject dissolveSlime;
    public GameObject AllSlime;

    public float phaseDuration = 2.0f;
    public float dissolveDuration = 2.0f;
    public float allDuration = 1.5f;

    protected override void Test1(InputAction.CallbackContext _)
    {
        StartCoroutine(StartPhase(phaseSlime));
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        StartCoroutine(StartPhase(phaseReverseSlime));
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        StartCoroutine(StartDissolve(dissolveSlime));
    }

    protected override void Test4(InputAction.CallbackContext _)
    {
        StartCoroutine(StartAllTest(AllSlime));
    }

    IEnumerator StartPhase(GameObject target)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        Material material = renderer.material;
        material.SetFloat("_Thickness", 0.1f);
        material.SetFloat("_Split", 0.0f);

        float timeElipsed = 0.0f;
        float phaseDurationNormalize = 1 / phaseDuration;

        while (timeElipsed < phaseDuration)
        {
            timeElipsed += Time.deltaTime;

            material.SetFloat("_Split", timeElipsed * phaseDurationNormalize);

            yield return null;
        }
        material.SetFloat("_Thickness", 0.0f);
    }

    IEnumerator StartDissolve(GameObject target)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        Material material = renderer.material;
        material.SetFloat("_Fade", 1.0f);

        float timeElipsed = 0.0f;
        float DissolveDurationNormalize = 1 / dissolveDuration;

        while (timeElipsed < dissolveDuration)
        {
            timeElipsed += Time.deltaTime;

            material.SetFloat("_Fade", 1 - timeElipsed * DissolveDurationNormalize);

            yield return null;
        }
    }

    IEnumerator StartAllTest(GameObject target)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        Material material = renderer.material;

        material.SetFloat("_Phase_Split", 0.0f);
        material.SetFloat("_Dessolve_Fade", 0.0f);

        float timeElipsed = 0.0f;
        float allDurationNormalize = 1 / allDuration;

        while (timeElipsed < allDuration)
        {
            timeElipsed += Time.deltaTime;
            material.SetFloat("_Dessolve_Fade", 1 - timeElipsed * allDurationNormalize);
            material.SetFloat("_Phase_Split", timeElipsed * allDurationNormalize);

            yield return null;
        }
    }
}
