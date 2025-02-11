using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject core;
    public GameObject gun;
    public TurretProperties turretProperties;
    public AudioSource firingSound;
    public List<ParticleSystem> particleSystems;
    public int burstCount;

    FindHome currentTargetCode;
    GameObject currentTarget;
    Quaternion coreStartRotation;
    Quaternion gunStartRotation;
    bool coolDown = true;

    private void Start()
    {
        coreStartRotation = core.transform.rotation;
        gunStartRotation = gun.transform.localRotation;
        foreach (ParticleSystem p in particleSystems)
        {
            // Debug.Log("Switching of ParticleSystem " + p.name);
            p.Stop();
        }
    }

    void CoolDown()
    {

        coolDown = true;
    }

    void ShootTarget()
    {

        if (currentTarget && coolDown)
        {
            //Debug.Log("In shoot script");
            currentTargetCode.Hit((int)turretProperties.damage);
            firingSound.Play();
            foreach (ParticleSystem p in particleSystems)
            {
                //Debug.Log("Playing Particle System!");
                p.Play();
            }
            coolDown = false;
            Invoke("CoolDown", turretProperties.reloadTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("goob") && currentTarget == null)
        {

            currentTarget = other.gameObject;
            currentTargetCode = currentTarget.GetComponent<FindHome>();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == currentTarget)
        {

            currentTarget = null;
        }
    }

    void Update()
    {

        if (currentTarget)
        {

            Vector3 aimAt = new Vector3
                (currentTarget.transform.position.x,
                core.transform.position.y,
                currentTarget.transform.position.z);
            // gun.transform.LookAt(currentTarget.transform.position);
            float distToTarget = Vector3.Distance(aimAt, gun.transform.position);
            Vector3 relativeTargetPosition = gun.transform.position + (gun.transform.forward * distToTarget);

            relativeTargetPosition = new Vector3(
                relativeTargetPosition.x,
                currentTarget.transform.position.y,
                relativeTargetPosition.z);
            gun.transform.rotation = Quaternion.Slerp(
                gun.transform.rotation,
                Quaternion.LookRotation(relativeTargetPosition - gun.transform.position),
                Time.deltaTime * turretProperties.turnSpeed);

            // core.transform.LookAt(aimAt);
            core.transform.rotation = Quaternion.Slerp(
                core.transform.rotation,
                Quaternion.LookRotation(aimAt - core.transform.position),
                Time.deltaTime * turretProperties.turnSpeed);

            Vector3 directionToTarget = currentTarget.transform.position - gun.transform.position;

            if (Vector3.Angle(directionToTarget, gun.transform.forward) < turretProperties.aimingAccuracy)
            {  // accuracy

                if (Random.Range(0, 100) < turretProperties.accuracy) ShootTarget();
            }
        }
        else
        {

            gun.transform.localRotation = Quaternion.Slerp(
                gun.transform.localRotation,
                gunStartRotation,
                Time.deltaTime * turretProperties.turnSpeed);

            core.transform.rotation = Quaternion.Slerp(
                core.transform.rotation,
                coreStartRotation,
                Time.deltaTime * turretProperties.turnSpeed);
        }
    }
}
