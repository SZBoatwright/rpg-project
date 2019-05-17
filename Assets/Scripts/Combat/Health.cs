using UnityEngine;

namespace RPG.Combat
{
  public class Health : MonoBehaviour
  {
    [SerializeField] float health = 100f;
    bool isDead = false;

    public bool IsDead()
    {
      return isDead;
    }

    public void TakeDamage(float amount)
    {
      health = Mathf.Max(health - amount, 0);

      if (health <= 0 && !isDead)
      {
        GetComponent<Animator>().SetTrigger("die");
        isDead = true;
      }
    }
  }
}