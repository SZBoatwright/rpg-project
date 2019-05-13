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

    Transform target;
    float timeSinceLastAttack = 0;

    Mover mover;

    private void Start()
    {
      mover = gameObject.GetComponent<Mover>();
    }

    private void Update()
    {
      timeSinceLastAttack += Time.deltaTime;

      if (target == null) return;

      if (!GetIsInRange()) mover.MoveTo(target.position);
      else
      {
        mover.Cancel();
        AttackBehavior();
      }
    }

    private void AttackBehavior()
    {
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
      target.GetComponent<Health>().TakeDamage(weaponDamage);
    }

    private bool GetIsInRange()
    {
      return Vector3.Distance(target.position, gameObject.transform.position) <= weaponRange;
    }

    public void Attack(CombatTarget combatTarget)
    {
      GetComponent<ActionScheduler>().StartAction(this);
      target = combatTarget.transform;
    }

    public void Cancel()
    {
      target = null;
    }
  }
}