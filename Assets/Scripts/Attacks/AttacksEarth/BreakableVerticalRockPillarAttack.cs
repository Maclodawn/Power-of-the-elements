﻿using UnityEngine;
using System.Collections;

public class BreakableVerticalRockPillarAttack : EarthAttack {

	public GameObject verticalRockPillar;

	protected override void updateMe() {
		basicAttack3();
	}
	
	protected override float WAIT_TIME() {
		return 0.3f;
	}

    protected void basicAttack3()
    {
        Ray ray = GetComponent<AttackLauncher>().getAimRay();
        RaycastHit hit;
        bool collided = Physics.Raycast(ray, out hit, 5000);

        if (!collided)
            hit.point = ray.direction * 5000;

        Quaternion xAndzRotation;
        if (transform.up == Vector3.up || transform.up == -Vector3.up)
            xAndzRotation = Quaternion.FromToRotation(transform.up + Vector3.forward * 0.01f, hit.normal + Vector3.forward * 0.01f);
        else if (transform.up == Vector3.right || transform.up == -Vector3.right
                 || transform.up == Vector3.forward || transform.up == -Vector3.forward)
            xAndzRotation = Quaternion.FromToRotation(transform.up + Vector3.up * 0.01f, hit.normal + Vector3.up * 0.01f);
        else
            xAndzRotation = Quaternion.FromToRotation(transform.up, hit.normal);

        Quaternion yRotation;
        if (transform.forward == Vector3.forward || transform.forward == -Vector3.forward)
            yRotation = Quaternion.FromToRotation(verticalRockPillar.transform.forward + Vector3.right * 0.01f, transform.forward + Vector3.right * 0.01f);
        else if (transform.forward == Vector3.right || transform.forward == -Vector3.right
                 || transform.forward == Vector3.up || transform.forward == -Vector3.up)
            yRotation = Quaternion.FromToRotation(verticalRockPillar.transform.forward + Vector3.forward * 0.01f, transform.forward + Vector3.forward * 0.01f);
        else
            yRotation = Quaternion.FromToRotation(verticalRockPillar.transform.forward, transform.forward);

        Quaternion rotation = xAndzRotation * yRotation;
        Vector3 newDirection = rotation * verticalRockPillar.transform.up;

        float ySize = 0;
        for (int i = 0; i < verticalRockPillar.transform.childCount; ++i)
        {
            MeshRenderer meshRenderer = verticalRockPillar.transform.GetChild(i).GetComponent<MeshRenderer>();
            ySize += meshRenderer.bounds.size.y;
        }

        Vector3 vect = newDirection * ySize / 2.0f;
        Instantiate(verticalRockPillar, hit.point - vect, rotation);
        //m_executingAtk3 = true;
        GetComponent<BasicMovement>().m_Animator.Play("Attack 03");
        GetComponent<BasicMovement>().m_Animator.CrossFade("Grounded", 1f);
    }
}
