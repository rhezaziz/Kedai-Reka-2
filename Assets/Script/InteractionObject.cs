
using UnityEngine;
public class InteractionObject : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent action;
    
    public void interaction() => action?.Invoke();
   

    /*
    public void actionType(string value)
    {
        switch (type)
        {
            case interactionType.Tempat:
                PlayerController player = FindObjectOfType<PlayerController>();
                Transform spawnPos = transform.GetChild(0);

                player.profil.position = spawnPos.position;

                FindObjectOfType<DemoLoadScene>().LoadScene(value);

                break;

            case interactionType.Benda:
                gameObject.SetActive(false);
                break;

            case interactionType.animasi:
                PlayerController _player = FindObjectOfType<PlayerController>();
                float PosY = _player.transform.position.y;
                float PosX = transform.GetChild(0).position.x;
                float PosZ = transform.GetChild(0).position.z;
                _player.transform.position = new Vector3(PosX, PosY, PosZ);
                _player.startAnimation(value);
                _anim.SetTrigger("Animasi");
                Debug.Log(value);

                break;
        }
    }
    */
}
