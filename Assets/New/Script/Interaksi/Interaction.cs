using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interaction
{
    public void action(Transform Player);

    public void btnActive(GameObject btn, bool interactable);

    public void isTutorial(bool value);

    public void checkDistance();

    public bool Interactable();

    public void changeInteractable(bool value);
}

public interface IDialog{
    public void endDialog();

    public void startDialog();
}
