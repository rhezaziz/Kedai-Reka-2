
using UnityEngine;

public class CollisionObject
{
    string[] nameDir = new string[]
    {
        "Kanan",
        "Kiri",
        "Depan",
        "Belakang"
    };

    Vector3[] dir = new Vector3[]
    {
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 0, -1),
        new Vector3(0, 0, 1)
    };

    public Vector3 getDir(string value)
    {
        return dir[System.Array.IndexOf(nameDir,value)];
    }
    public bool collision(Vector3 pos, string value)
    {
        RaycastHit hit;
        if(Physics.Raycast(pos, dir[System.Array.IndexOf(nameDir, value)], out hit, 2.5f)){
            Debug.Log(hit.transform.name);
            return true;
        }
        return false;
    }
    
}
