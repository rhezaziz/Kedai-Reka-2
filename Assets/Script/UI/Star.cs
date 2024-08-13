using DG.Tweening;
using System.Collections;

using UnityEngine;

public class Star : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UISkor;
    public IEnumerator changeSkor(GameObject position)
    {
        transform.localScale = Vector2.zero;
        transform.position = position.transform.position;
        //bintang.SetActive(true);
        transform.DOScale(new Vector2(.15f, .15f), 0.5f).SetEase(Ease.OutBounce);
        //Noda[index].GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);

        transform.DOMove(UISkor.transform.position, 1f);
        yield return new WaitForSeconds(1f);

        //textScore.text = scoreGame() + "/" + Noda.Length;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = Vector3.zero;
        //bintang.SetActive(false);
    }
}
