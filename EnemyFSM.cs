using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum State
    {
        Move,
        Attack,
        Dead
    }

    public State currentState;

    public Transform[] waypoints;
    int index = 0;

    public float speed = 2f;

    void Start()
    {
        currentState = State.Move;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Move:
                Move();
                break;

            case State.Attack:
                Attack();
                break;

            case State.Dead:
                Dead();
                break;
        }
    }

    void Move()
    {
        Transform target = waypoints[index];

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            index++;

            if (index >= waypoints.Length)
            {
                currentState = State.Attack;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Nyampe base, nyerang!");
        currentState = State.Dead;
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
