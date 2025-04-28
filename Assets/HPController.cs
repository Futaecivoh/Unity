using System.Collections;
using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] private int HP = 3;
    private int currentHP;
    [SerializeField] private GameObject prefabHP;
    private Animator animator;
    private Coroutine runable; 
    private void initHP()
    {
        animator = GetComponent<Animator>();
        currentHP = HP;
        runable = null;
        int countSideEl = HP / 2;
        int[] massOffset = new int[HP];
        for (int i = -countSideEl, j = 0; i <= countSideEl; i++, j++)
        {
            massOffset[j] = i;
        }
        for (int i = 0; i < HP; i++)
        {
            GameObject hp = Instantiate(prefabHP, gameObject.transform);
            hp.transform.localPosition = new Vector3(massOffset[i] * 0.1f, 0.2f, 0);
        }
    }

    private void clearHP()
    {
        int countHP = gameObject.transform.childCount - 1;
        for (int i = 0; i < countHP;i++)
        {
            Destroy(gameObject.transform.GetChild(1 + i).gameObject);
        }
    }

    void Start()
    {
        initHP();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && runable == null)
        {
            GameObject lastHP = gameObject.transform.GetChild(currentHP).gameObject;
            lastHP.GetComponent<HPAnimationController>().DestroyAnim();
            runable = StartCoroutine(destroyHp(lastHP));
        }
        else if (currentHP == 0 || collision.gameObject.name == "DeadZone")
        {
            clearHP();
            transform.position = gameObject.GetComponent<CheckpointManager>().LastCheckpoint.transform.position + Vector3.up;
            initHP();
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Traps"  && runable == null)
        {
            GameObject lastHP = gameObject.transform.GetChild(currentHP).gameObject;
            lastHP.GetComponent<HPAnimationController>().DestroyAnim();
            runable = StartCoroutine(destroyHp(lastHP));
        }
        else if (currentHP == 0 || collision.gameObject.name == "DeadZone")
        {
            clearHP();
            transform.position = gameObject.GetComponent<CheckpointManager>().LastCheckpoint.transform.position + Vector3.up;
            initHP();
        }
    }

    IEnumerator destroyHp(GameObject obj)
    {
        currentHP--;
        animator.SetTrigger("isDamaged");
        yield return new WaitForSeconds(0.6f);
        Destroy(obj);
        runable = null;
    }
}