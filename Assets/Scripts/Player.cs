using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Weapon weapon;

    private void Update()
    {
        Movement();
        Rotation();
        if (Input.GetButton("Fire1"))
            weapon.Shoot();
    }

    private void Rotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    private void Movement()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var ver = Input.GetAxisRaw("Vertical");

        agent.Move(new Vector3(hor, 0, ver) * agent.speed * Time.deltaTime);
    }
}
