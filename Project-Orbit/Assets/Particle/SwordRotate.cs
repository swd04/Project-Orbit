using DG.Tweening;
using System.Collections;
using UnityEngine;

public class SwordRotate : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVecter = Vector3.zero;

    [SerializeField] private Vector3 startRotate = Vector3.zero;

    [SerializeField] private bool isMoving = false;

    [SerializeField] private ParticleSystem[] flame = null;

    [SerializeField] private float particleSpeed = 0.0f;

    [Header("メニュー管理")]
    [SerializeField] private MenuManager menuManager = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startRotate = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //メニュー中は操作されない
        if (menuManager.IsMenuOpen)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1) && !isMoving || Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SwordAttack());
        }

    }

    private IEnumerator SwordAttack()
    {
        isMoving = true;
        for (int i = 0; i < flame.Length; i++)
        {
            flame[i].startSpeed = particleSpeed;
        }
        yield return new WaitForSeconds(0.1f);
        transform.DORotate(rotationVecter, 0.2f,RotateMode.WorldAxisAdd);
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < flame.Length; i++)
        {
            flame[i].startSpeed = 1.0f;
        }
        yield return new WaitForSeconds(2f);

        transform.DORotate(startRotate, 1f);
        
        isMoving = false;
    }
}
