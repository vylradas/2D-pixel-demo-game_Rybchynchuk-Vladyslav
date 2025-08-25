using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int mana = 5;

    public Image[] MP;
    public Sprite fullMana;
    public Sprite emptyMana;

    private Coroutine manaRegenerationCoroutine;

    void Awake()
    {
        mana = 5;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Image img in MP)
        {
            img.sprite = emptyMana;
        }
        for (int i = 0; i < mana; i++)
        {
            MP[i].sprite = fullMana;
        }

        if (mana < 5 && manaRegenerationCoroutine == null)
        {
            manaRegenerationCoroutine = StartCoroutine(RegenerateMana());
        }
    }

    public void RestoreMana(int amount)
    {
        mana = Mathf.Min(mana + amount, 5); // Ensure mana doesn't exceed 5
    }

    private IEnumerator RegenerateMana()
    {
        while (mana < 5)
        {
            yield return new WaitForSeconds(2f);
            mana++;
        }
        manaRegenerationCoroutine = null;
    }
}
