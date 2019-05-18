using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] float weaponRange = 2f;
    [SerializeField] float weaponDamage = 4f;
    [SerializeField] float timeBetweenAttacks = 1f;

    Health target;
    float timeSinceLastAttack = 0;

    Mover mover;

    private void Start()
    {
      mover = gameObject.GetComponent<Mover>();
    }

    private void Update()
    {
      timeSinceLastAttack += Time.deltaTime;

      if (target == null || target.IsDead()) return;

      if (!GetIsInRange()) mover.MoveTo(target.transform.position);
      else
      {
        mover.Cancel();
        AttackBehavior();
      }
    }

    private void AttackBehavior()
    {
      transform.LookAt(target.transform);
      if (timeSinceLastAttack > timeBetweenAttacks)
      {
        // this will trigger the Hit() event
        GetComponent<Animator>().SetTrigger("attack");
        timeSinceLastAttack = 0;
      }
    }

    // animation event
    void Hit()
    {
      target.TakeDamage(weaponDamage);
    }

    private bool GetIsInRange()
    {
      return Vector3.Distance(target.transform.position, gameObject.transform.position) <= weaponRange;
    }

    public bool CanAttack(CombatTarget combatTarget)
    {
      if (combatTarget == null) return false;
      Health health = combatTarget.GetComponent<Health>();
      return health != null && !health.IsDead();
    }

    public void Attack(CombatTarget combatTarget)
    {
      GetComponent<ActionScheduler>().StartAction(this);
      target = combatTarget.GetComponent<Health>();
    }

    public void Cancel()
    {
      GetComponent<Animator>().SetTrigger("stopAttack");
      target = null;
    }
  }
}