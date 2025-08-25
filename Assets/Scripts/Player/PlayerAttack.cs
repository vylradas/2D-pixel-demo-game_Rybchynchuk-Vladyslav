using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject cometPrefab; // ������ ������
    [SerializeField] private GameObject strikePrefab; // ������ �������
    [SerializeField] private int manaCostPerAttack = 1; // ������� ���� �� �����
    [SerializeField] private int manaCostPerSpecialAttack = 1; // ������� ���� �� ���������� �����

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    public static bool Comet = false;
    public static bool Strike = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack() && ManaManager.mana >= manaCostPerAttack)
        {
            Attack();
        }
        else if (Input.GetMouseButton(1) && cooldownTimer > attackCooldown && playerMovement.canAttack() && ManaManager.mana >= manaCostPerSpecialAttack)
        {
            SpecialAttack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        // ������� ����
        ManaManager.mana -= manaCostPerAttack;

        // ������ �������� ��� �������� ������
        StartCoroutine(LaunchCometWithDelay(0.5f, cometPrefab));

        // ������������ Comet �� true
        Comet = true;
        // ������������ Strike �� false
        Strike = false;
    }

    private void SpecialAttack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        // ������� ����
        ManaManager.mana -= manaCostPerSpecialAttack;

        // ������ �������� ��� �������� ���������� ������
        StartCoroutine(LaunchStrikeWithDelay(0.5f, strikePrefab));

        // ������������ Strike �� true
        Strike = true;
        // ������������ Comet �� false
        Comet = false;
    }

    private IEnumerator LaunchCometWithDelay(float delay, GameObject cometPrefab)
    {
        yield return new WaitForSeconds(delay);

        // ��������� �� ������ ������
        GameObject comet = Instantiate(cometPrefab, attackPoint.position, Quaternion.identity);
        comet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));


    }

    private IEnumerator LaunchStrikeWithDelay(float delay, GameObject strikePrefab)
    {
        yield return new WaitForSeconds(delay);

        // ��������� �� ������ �������
        GameObject strike = Instantiate(strikePrefab, attackPoint.position, Quaternion.identity);
        strike.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
}
