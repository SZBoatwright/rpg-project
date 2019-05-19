using UnityEngine;

namespace RPG.Core
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
        Die();
      }
    }

    private void Die()
    {
      if (isDead) return;

      GetComponent<ActionScheduler>().CancelCurrentAction();
      GetComponent<Animator>().SetTrigger("die");
      isDead = true;
    }
  }
}