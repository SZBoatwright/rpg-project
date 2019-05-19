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
        TriggerAttack();
        timeSinceLastAttack = Mathf.Infinity;
      }
    }

    private void TriggerAttack()
    {
      // this will trigger the Hit() event
      GetComponent<Animator>().ResetTrigger("stopAttack");
      GetComponent<Animator>().SetTrigger("attack");
    }

    // animation event
    void Hit()
    {
      if (target == null) return;
      target.TakeDamage(weaponDamage);
    }

    private bool GetIsInRange()
    {
      return Vector3.Distance(target.transform.position, gameObject.transform.position) <= weaponRange;
    }

    public bool CanAttack(GameObject combatTarget)
    {
      if (combatTarget == null) return false;
      Health health = combatTarget.GetComponent<Health>();
      return health != null && !health.IsDead();
    }

    public void Attack(GameObject combatTarget)
    {
      GetComponent<ActionScheduler>().StartAction(this);
      target = combatTarget.GetComponent<Health>();
    }

    public void Cancel()
    {
      StopAttack();
      target = null;
    }

    private void StopAttack()
    {
      GetComponent<Animator>().SetTrigger("stopAttack");
      GetComponent<Animator>().ResetTrigger("attack");
    }
  }
}