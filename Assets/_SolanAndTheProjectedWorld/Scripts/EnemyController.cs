using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float detectRange = 10f;
    public float detectAngle = 45f;
    public float attackRange = 2f;

    public float minimumDistance = 2f;

    public int arrowDamage = 10;

    public string playerTag = "PseudoBody"; // Set the player tag in the editor.
    public string whatHitsTag = "FireableArrow";
    
    public Transform player;
    public Animator animator;
    public NavMeshAgent navMeshAgent;

    public string idleStateName = "IsIdle"; // Set the idle state name in the editor.
    public string runningStateName = "IsRunning"; // Set the running state name in the editor.
    public string attackingStateName = "IsAttacking"; // Set the attacking state name in the editor.
    public string deathTriggerStateName = "DeathTrigger"; // Set the die animation
    
    public int maxHealth = 100; // Set the maximum health in the editor.
    private int _currentHealth;
    
    private bool _playerInSight;
    private bool _playerInRange;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        _playerInSight = false;
        _playerInRange = false;
        
        _currentHealth = maxHealth;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is in sight.
        if (distanceToPlayer < detectRange)
        {
            // Perform line of sight check.
            if (HasLineOfSightToPlayer())
            {
                _playerInSight = true;
                _playerInRange = distanceToPlayer <= attackRange;

                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                animator.SetBool(idleStateName, false);
                animator.SetBool(runningStateName, !_playerInRange);
                animator.SetBool(attackingStateName, _playerInRange);

                // Check the distance between the enemy and the player.
                if (!_playerInRange)
                {
                    // The enemy is not in attacking range; set the destination to the player's position.
                    navMeshAgent.enabled = distanceToPlayer > minimumDistance;
                    if (navMeshAgent.enabled)
                    {
                        navMeshAgent.SetDestination(player.position);
                    }
                }
                else
                {
                    // The enemy is in attacking range; stop movement.
                    navMeshAgent.enabled = false;
                }
            }

        }
        else
        {
            _playerInSight = false;
            _playerInRange = false;
            animator.SetBool(idleStateName, true);
            animator.SetBool(runningStateName, false);
            animator.SetBool(attackingStateName, false);
        }

        if (_playerInSight)
        {
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }
    
    bool HasLineOfSightToPlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        if (Vector3.Angle(transform.forward, toPlayer) <= detectAngle / 2)
        {
            Debug.Log("Angle "+ Vector3.Angle(transform.forward, toPlayer) );
            if (Physics.Raycast(transform.position, toPlayer, out RaycastHit hit, detectRange))
            {
                Debug.Log("Player tag: " + playerTag);
                Debug.Log("Hit info: " + hit.transform.tag);
                
                Debug.Log("Raycast hit: " + hit.collider.name); // Print the name of the hit object for debugging.

                Debug.Log("Hit collider tag: " + hit.collider.tag);
                Debug.Log("Hit collider game object tag: "+ hit.collider.gameObject.tag);
                if (hit.collider.CompareTag(playerTag))
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Vector3 coneApex = transform.position + Vector3.up * 1.5f; // Adjust the height as needed.
            Quaternion rotation = Quaternion.Euler(0, -detectAngle / 2, 0);

            for (int i = 0; i <= 20; i++)
            {
                Vector3 rayDirection = rotation * transform.forward;
                Gizmos.DrawRay(coneApex, rayDirection * detectRange);
                rotation = Quaternion.Euler(0, detectAngle / 20, 0) * rotation;
            }
        }
    }
    
    // Detect arrow collisions and reduce health.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(whatHitsTag))
        {
            HandleArrowHit(arrowDamage); // You can adjust the damage value as needed.
        }
    }

    // Add a method to handle arrow hits.
    public void HandleArrowHit(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
            return;
        }

        Debug.Log("Kera is still alive and doing well with terror");
    }

    void Die()
    {
        animator.SetTrigger(deathTriggerStateName); // Trigger the death animation.
        navMeshAgent.enabled = false;
        navMeshAgent.angularSpeed = 0f; // Stop rotating.
        // Handle other death-related logic as needed.
    }
}
