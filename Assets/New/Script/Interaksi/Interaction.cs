using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interaction
{
    public void action(Transform Player);

    public void btnActive(GameObject btn, bool interactable);

    public bool isTutorial();
}

public interface IDialog{
    public void endDialog();

    public void startDialog();
}
