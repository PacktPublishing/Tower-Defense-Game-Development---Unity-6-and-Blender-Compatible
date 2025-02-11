using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyDetails", order = 1)]
public class EnemyDetails : ScriptableObject {

    public string cName;
    public float speed;
    public int maxHealth;
}
