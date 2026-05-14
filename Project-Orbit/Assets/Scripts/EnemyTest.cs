using DG.Tweening;
using UnityEngine;

/// <summary>
/// “G‚ÌƒeƒXƒg—pƒNƒ‰ƒX
/// </summary>
public class EnemyTest : MonoBehaviour
{
    [Header("‰½‰ñUŒ‚‚³‚ê‚½‚ç“|‚ê‚é‚©")]
    [SerializeField] private int hitCount = 3;

    [Header("•‚‚­‚‚³")]
    [SerializeField] private float floatPower = 0.3f;

    [Header("•‚‚­ŠÔ")]
    [SerializeField] private float floatDuration = 0.1f;

    //Œ»İ‚Ì”í’e‰ñ”
    private int currentHitCount = 0;

    private bool isDead = false;

    /// <summary>
    /// UŒ‚‚ª“–‚½‚Á‚½
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }

        //•Ší‚É“–‚½‚Á‚½
        if (other.CompareTag("Weapon"))
        {
            currentHitCount++;

            Debug.Log($"“G”í’e‰ñ” : {currentHitCount}");

            //­‚µ•‚‚©‚¹‚é
            transform
            .DOMoveY(transform.position.y + floatPower, floatDuration)
            .SetLoops(2, LoopType.Yoyo);

            //ˆê’è‰ñ””í’e‚µ‚½‚çíœ
            if (currentHitCount >= hitCount)
            {
                isDead = true;

                //WaveManager‚Ö’Ê’m
                FindObjectOfType<WaveManager>().OnEnemyDead();

                Destroy(gameObject, 0.2f);
            }
        }
    }
}