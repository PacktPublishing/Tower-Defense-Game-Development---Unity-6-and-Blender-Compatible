using UnityEngine;

[CreateAssetMenu(fileName = "TProperties", menuName = "ScriptableObjects/TurretProperties", order = 2)]
public class TurretProperties : ScriptableObject {


    public float damage;
    public float accuracy;
    public float turnSpeed;
    public float reloadTime;
    public float aimingAccuracy;
}