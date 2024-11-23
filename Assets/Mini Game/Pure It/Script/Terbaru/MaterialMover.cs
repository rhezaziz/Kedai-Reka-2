using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru
{
    public class MaterialMover : MonoBehaviour
    {
        // Start is called before the first frame update

        PlacementMaterial place;

        public int idFactory;

        void Update()
        {
            //if dragged
            if (Input.GetMouseButton(0))
            {
                followInputPosition();
            }

            //if released and doesn't need process
            if ((!Input.GetMouseButton(0) && Input.touches.Length < 1))
            {
                //place.GetComponent<PlacementMaterial>().place(idFactory);
                //canGetDragged = false;
                checkCorrectPlacement();
            }

            //if released and needs process
            /*if ((!Input.GetMouseButton(0) && Input.touches.Length < 1) && !isFinished && needsProcess)
            {
                canGetDragged = false;
                checkCorrectPlacementOnProcessor();
            }
    */
            //if needs process and process is finished successfully
            /*if (needsProcess && isProcessed && !isOverburned && !MainGameController.deliveryQueueIsFull && !Bahan.itemIsInHand)
            {
                manageSecondDrag();
            }
    */
            //if needs process and process took too long and burger is overburned and must be discarded
            /*if (needsProcess && isProcessed && isOverburned && !MainGameController.deliveryQueueIsFull && !Bahan.itemIsInHand)
            {
                manageDiscard();
            }
    */
            //you can make some special effects like particle, smoke or other visuals when your ingredient in being processed


            //Optional - change target's color when this ingredient is near enough
            /*if (!processFlag || (isProcessed && target == serverPlate))
                changeTargetsColor(target);
    */
        }

        private Vector3 _Pos;
        void followInputPosition()
        {
            _Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Custom offset. these objects should be in front of every other GUI instances.
            _Pos = new Vector3(_Pos.x, _Pos.y, -0.5f);
            //follow player's finger
            transform.position = _Pos + new Vector3(0, 0, 0);
        }



        void checkCorrectPlacement()
        {

            //if this ingredient is close enough to serving plate, we can add it to main queue. otherwise drop and delete it.
            //float distanceToPlate = Vector3.Distance(serverPlate.transform.position, gameObject.transform.position);
            //print("distanceToPlate: " + distanceToPlate);
            //Bowl tempBowl = serverPlate.GetComponent<Bowl>();
            if (place)
            {
                place.place(idFactory);
                //close enough to land on plate

                //serverPlate.GetComponent<Bowl>().addIngrediant(factoryID);
                /* transform.parent = serverPlate.transform;
                 transform.position = new Vector3(serverPlate.transform.position.x,
                                                  serverPlate.transform.position.y + (0.35f * MainGameController.deliveryQueueItems),
                                                  serverPlate.transform.position.z - (0.2f * MainGameController.deliveryQueueItems + 0.1f));*/

                //            MainGameController.deliveryQueueItems++;
                //  MainGameController.deliveryQueueItemsContent.Add(factoryID);
                Destroy(gameObject);

            }
            else
            {
                Destroy(gameObject);
            }

            //Not draggable anymore.
            //isFinished = true;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null && !place)
            {

                if (other.TryGetComponent<PlacementMaterial>(out PlacementMaterial temp))
                {
                    place = temp;
                    temp.closesDistance();

                }

            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other != null)
            {
                place.LongDistance();
                place = null;
            }
        }
    }
}