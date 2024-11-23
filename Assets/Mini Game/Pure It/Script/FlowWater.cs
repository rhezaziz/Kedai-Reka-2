using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terbaru
{
    public class FlowWater : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private ParticleSystem splashParticle;

        private Coroutine pourRoutine;

        private Vector3 TargetPosition;
        public bool Flow;
        public Transform Water;
        public float posY;
        public float speed;
        public bool Atas;
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();

            splashParticle = GetComponentInChildren<ParticleSystem>();
        }

        private void Start()
        {
            MoveToPosition(0, transform.position);
            MoveToPosition(1, transform.position);

            //Begin();

        }

        public void Begin(Material[] mat)
        {
            if (!Atas)
            {
                Water.GetComponent<SpriteRenderer>().material = mat[1];
                lineRenderer.material = mat[0];
            }
            StartCoroutine(UpdateParticle());
            pourRoutine = StartCoroutine(BeginPour());

        }


        private IEnumerator BeginPour()
        {
            while (gameObject.activeSelf)
            {
                TargetPosition = FindEndPoint();

                MoveToPosition(0, transform.position);
                AnimatedToPosition(1, TargetPosition);


                yield return null;
            }
        }

        public void End()
        {
            StopCoroutine(pourRoutine);
            pourRoutine = StartCoroutine(EndPour());


        }

        private IEnumerator EndPour()
        {
            while (!hasReachedPosition(0, TargetPosition))
            {
                AnimatedToPosition(0, TargetPosition);
                AnimatedToPosition(1, TargetPosition);
                yield return null;
            }

            if (Atas) FindObjectOfType<ManagerPurify>().pourBawah();

            //StopCoroutine(pourRoutine);
            Destroy(gameObject);
        }


        private Vector3 FindEndPoint()
        {
            /*
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);

            Physics.Raycast(ray, out hit);
            Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);

            return endPoint;
            */

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down);
            Ray2D ray = new Ray2D(transform.position, Vector3.down);

            Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2f);

            return endPoint;
        }

        private void MoveToPosition(int index, Vector3 targetPosition)
        {
            lineRenderer.SetPosition(index, targetPosition);
        }


        private void AnimatedToPosition(int index, Vector3 targetPosition)
        {
            Vector3 currentPoint = lineRenderer.GetPosition(index);
            Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * speed);
            //Debug.Log($" Atas : {Atas} Posisi : {newPosition}");
            lineRenderer.SetPosition(index, newPosition);
        }

        private bool hasReachedPosition(int index, Vector3 targetPosition)
        {
            Vector3 currentPosition = lineRenderer.GetPosition(index);
            return currentPosition == targetPosition;
        }

        private IEnumerator UpdateParticle()
        {
            while (gameObject.activeSelf)
            {
                splashParticle.gameObject.transform.position = TargetPosition;

                bool isHitting = hasReachedPosition(1, TargetPosition);

                splashParticle.gameObject.SetActive(isHitting);

                fillWater(0.001f, isHitting);
                yield return null;
            }
        }

        void fillWater(float value, bool fill)
        {
            Vector2 pos = Water.localPosition;
            if (pos.y < posY && fill)
            {
                Water.localPosition = new Vector3(pos.x, pos.y + value, 1f);
                //Water.localScale = new Vector3(scale.x, scale.y + value, 1f);
            }

            else if (pos.y >= posY)
                End();
        }
    }
}