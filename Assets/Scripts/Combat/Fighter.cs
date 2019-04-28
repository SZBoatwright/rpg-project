using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;

namespace RPG.Combat
{
  public class Fighter : MonoBehaviour
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
      bool isInRange = Vector3.Distance(target.position, gameObject.transform.position) <= weaponRange;
      if (target != null && !isInRange) mover.MoveTo(target.position);
      else mover.Stop();
    }

    public void Attack(CombatTarget combatTarget)
    {
      target = combatTarget.transform;
    }
  }
}