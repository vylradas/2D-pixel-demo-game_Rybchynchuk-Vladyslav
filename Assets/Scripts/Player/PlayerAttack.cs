using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject cometPrefab; // Префаб комети
    [SerializeField] private GameObject strikePrefab; // Префаб страйку
    [SerializeField] private int manaCostPerAttack = 1; // Вартість мани за атаку
    [SerializeField] private int manaCostPerSpecialAttack = 1; // Вартість мани за спеціальну атаку

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

        // Витрати мани
        ManaManager.mana -= manaCostPerAttack;

        // Запуск корутини для затримки комети
        StartCoroutine(LaunchCometWithDelay(0.5f, cometPrefab));

        // Встановлюємо Comet на true
        Comet = true;
        // Встановлюємо Strike на false
        Strike = false;
    }

    private void SpecialAttack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        // Витрати мани
        ManaManager.mana -= manaCostPerSpecialAttack;

        // Запуск корутини для затримки спеціальної комети
        StartCoroutine(LaunchStrikeWithDelay(0.5f, strikePrefab));

        // Встановлюємо Strike на true
        Strike = true;
        // Встановлюємо Comet на false
        Comet = false;
    }

    private IEnumerator LaunchCometWithDelay(float delay, GameObject cometPrefab)
    {
        yield return new WaitForSeconds(delay);

        // Створення та запуск комети
        GameObject comet = Instantiate(cometPrefab, attackPoint.position, Quaternion.identity);
        comet.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));


    }

    private IEnumerator LaunchStrikeWithDelay(float delay, GameObject strikePrefab)
    {
        yield return new WaitForSeconds(delay);

        // Створення та запуск страйку
        GameObject strike = Instantiate(strikePrefab, attackPoint.position, Quaternion.identity);
        strike.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
}
