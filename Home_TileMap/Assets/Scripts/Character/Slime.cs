using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    /// <summary>
    /// ����� ����Ǵ� �ð�(���� �� ������)
    /// </summary>
    public float phaseDuration = 0.5f;

    /// <summary>
    /// �����갡 ����Ǵ� �ð�(��� ���Ŀ� ������ ����� �������� �ð�)
    /// </summary>
    public float dissolveDuration = 1.0f;

    /// <summary>
    /// �ƿ������� �β�
    /// </summary>
    const float Outline_Thickness = 0.005f;

    /// <summary>
    /// ���̴��� ������Ƽ�� ������ �ϱ� ���� ��Ƽ����
    /// </summary>
    Material mainMaterial;

    /// <summary>
    /// ����� ������ �� ����� ��������Ʈ
    /// </summary>
    public Action onPhaseEnd;

    /// <summary>
    /// �������� �׾��� �� ����� ��������Ʈ
    /// </summary>
    public Action onDie;

    /// <summary>
    /// �� ������Ʈ�� ��Ȱ��ȭ �� ��(�����갡 �� ���� ���Ŀ�) ����� ��������Ʈ
    /// </summary>
    public Action onDisable;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        mainMaterial = renderer.material;   // ��Ƽ���� �̸� ã�� ����
    }

    private void OnEnable()
    {
        ShowOutline(false);                             // �ƿ����� ����
        mainMaterial.SetFloat("_Dissolve_Fade", 1.0f);  // ������ ���� �ȵ� ���·� �����
        StartCoroutine(StartPhase());                   // ������ ����
    }

    private void OnDisable()
    {
        onDisable?.Invoke();    // ��Ȱ��ȭ �Ǿ��ٰ� �˸�(����ť�� �ٽ� �����ֶ�� ��ȣ�� �����ִ� ���� �� �뵵)
    }

    /// <summary>
    /// �� �������� ���� �� ������ �Լ�
    /// </summary>
    public void Die()
    {
        // ������ ����
        StartCoroutine(StartDissolve());
        // �׾��ٰ� ��ȣ ������
        onDie?.Invoke();
    }

    /// <summary>
    /// ����� �����ϱ� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator StartPhase()
    {
        mainMaterial.SetFloat("_Phase_Thickness", 0.1f);    // ������ ���� �� ���� �߻� ���� �β� ����(���̰� �����)
        mainMaterial.SetFloat("_Phase_Split", 0.0f);        // ������ ���� ��ġ �ʱ�ȭ

        float timeElipsed = 0.0f;                           // ���� �ð� �ʱ�ȭ
        float phaseDurationNormalize = 1 / phaseDuration;   // split�� ������ 0~1�̱� ������ �ð��� 0~1 ������ ������ ����ȭ ��Ű�� ���� �뵵

        while (timeElipsed < phaseDuration)                 // Ư�� �ð��� ��ǥ�ð��� ������ �� ���� �ݺ�
        {
            timeElipsed += Time.deltaTime;                  // Ư�� �ð��� �����Ӻ��� ������Ű��

            mainMaterial.SetFloat("_Phase_Split", timeElipsed * phaseDurationNormalize);    // ���� �ð��� ����ȭ ���� split ������ ����

            yield return null;      // ���� �����ӱ��� ��ٸ���
        }

        mainMaterial.SetFloat("_Phase_Split", 1.0f);        // split�� �ִ�ġ�� ����
        mainMaterial.SetFloat("_Phase_Thickness", 0.0f);    // ������ ���� �� ���� �߰� ���� �β� ���� (�Ⱥ��̰� �����)
        onPhaseEnd?.Invoke();                               // ����� �����ٰ� �˸�
    }

    /// <summary>
    /// �����긦 �����ϱ� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator StartDissolve()
    {
        mainMaterial.SetFloat("_Dissolve_Fade", 1.0f);          // ������ ���� ���� �ʱ�ȭ(�����갡 ���� ������� �ʰ� ����)

        float timeElipsed = 0.0f;                               // ���� �ð� �ʱ�ȭ
        float dessolveDurationNormalize = 1 / dissolveDuration; // fade�� ������ 0~1�̱� ������ �ð��� 0~1������ ������ ����ȭ ��Ű�� ���� �뵵

        while (timeElipsed < dissolveDuration)                  // ���� �ð��� ��ǥ�ð��� ������ �� ���� �ݺ�
        {   
            timeElipsed += Time.deltaTime;                      // ���� �ð��� �����Ӻ��� ������Ű��

            mainMaterial.SetFloat("_Dissolve_Fade", 1 - timeElipsed * dessolveDurationNormalize);   // ���� �ð��� ����ȭ���� (1-����ȭ �� ��)�� fade ������ ����

            yield return null;                                  // ���� �����ӱ��� ���
        }
        this.gameObject.SetActive(false);                       // ���� ������Ʈ ��Ȱ��ȭ(������Ʈ Ǯ�� �ǵ�����)
    }

    /// <summary>
    /// �ƿ����� ǥ�ÿ� �Լ�
    /// </summary>
    /// <param name="isShow">true�� �ƿ����� ǥ��, false�� �ƿ����� ����</param>
    public void ShowOutline(bool isShow)
    {
        if (isShow)
        {
            mainMaterial.SetFloat("_OutLine_Thickness", Outline_Thickness); // �ƿ����� �β� �����ؼ� ���̰� �����
        }
        else
        {
            mainMaterial.SetFloat("_OutLine_Thickness", 0.0f);              // �ƿ������� �β� �����ؼ� �Ⱥ��̰� �����
        }
    }
}
