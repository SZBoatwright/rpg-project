using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] float weaponRange = 2f;
    Transform target;
    Mover mover;

    private void Start()
    {
      mover = gameObject.GetComponent<Mover>();
    }

    private void Update()
    {
      if (target != null && !GetIsInRange()) mover.MoveTo(target.position);
      else
      {
        mover.Cancel();
      }
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