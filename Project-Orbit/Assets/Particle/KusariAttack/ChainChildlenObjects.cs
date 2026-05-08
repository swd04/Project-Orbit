using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainChildlenObjects : MonoBehaviour
{
    [SerializeField] private GameObject startChain = null;

    [SerializeField] private GameObject centerChain = null; 

    [SerializeField] private GameObject endChain = null;

    [SerializeField] private float chainSpwanTime = 0.0f;

    [SerializeField] private List<float> chainChildlensSpwanTime = new List<float>(); 

    [SerializeField] private ParticleSystem chainLight = null;

    [SerializeField] private bool startChainSpwan = false;

    [SerializeField] private float chainActiveTimeDelta = 0.0f;

    [SerializeField] private float chainDestroyTime = 0.0f;

    private void Start()
    {
        //StartCoroutine(ChainSpwan());
    }
    private void Update()
    {
        if (startChainSpwan)
        {
            chainActiveTimeDelta += Time.deltaTime;

            if (chainChildlensSpwanTime[0] <= chainActiveTimeDelta)
            {
                startChain.gameObject.SetActive(true);

                if (chainChildlensSpwanTime[1] <= chainActiveTimeDelta)
                {
                    centerChain.gameObject.SetActive(true);

                    if (chainChildlensSpwanTime[2] <= chainActiveTimeDelta)
                    {
                        endChain.gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                chainActiveTimeDelta = 0.0f;
                StartCoroutine(ChainSpwan());
            }
        }
    }
    private IEnumerator ChainSpwan()
    {
        yield return new WaitForSeconds(chainSpwanTime);
        startChainSpwan = true;
        chainLight.Play();


        yield return new WaitForSeconds(chainDestroyTime);
        startChainSpwan = false;
        startChain.gameObject.SetActive(false);
        centerChain.gameObject.SetActive(false);
        endChain.gameObject.SetActive(false);
    }
}
