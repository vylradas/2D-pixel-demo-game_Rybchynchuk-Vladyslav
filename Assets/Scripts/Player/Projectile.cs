using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.tag == "Int")
        {
            if (PlayerAttack.Comet == true)
            {
                PlayerManager.score += 30;
                Destroy(collision.gameObject);
            }
            if (PlayerAttack.Strike == true)
            {
                if((PlayerManager.score - 10) >= 0)
                {
                    PlayerManager.score -= 10;
                }
            }
        }

        if (collision.tag == "String")
        {
            if (PlayerAttack.Strike == true)
            {
                PlayerManager.score += 30;
                Destroy(collision.gameObject);
            }
            if (PlayerAttack.Comet == true)
            {
                if ((PlayerManager.score - 10) >= 0)
                {
                    PlayerManager.score -= 10;
                }
            }
        }
        PlayerAttack.Comet = false;
        PlayerAttack.Strike = false;

        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
