using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems;
namespace EntityComponent
{
    public class EntityMovement : MonoBehaviour
    {
        public TilemapGrapher tilemapGrapher;
        private Stack<Vector2> path;
        private Rigidbody2D rb;
        private Vector2 moveVec;
        private Attributes attributes;
        private bool goToActive;
        public bool GoToActive { get => goToActive; }
        private Vector2 goToTarget;
        [SerializeField]
        private float goToAccuracy;

        

        // Start is called before the first frame update
        void Start()
        {
            path = new Stack<Vector2>();
            rb = GetComponent<Rigidbody2D>();
            attributes = GetComponent<Attributes>();
        }

        // Update is called once per frame
        void Update()
        {
            if(goToActive)
            {
                float distance = (goToTarget - rb.position).magnitude;
                if(distance < goToAccuracy)
                {
                    moveVec.Set(0, 0);
                    goToActive = false;
                }
            }
            Move();
        }

        private void Move()
        {
            rb.velocity = moveVec * attributes.moveSpeed.Value();
        }
        public void GoTo(Vector2 target)
        {
            goToTarget = target;
            moveVec = target - rb.position;
            moveVec.Normalize();
            goToActive = true;
        }
        public void GoToWithPF(Vector2 target)
        {
            path = tilemapGrapher.NodeToPath(tilemapGrapher.graph.BFS(tilemapGrapher.GlobalToLocal(rb.position), tilemapGrapher.GlobalToLocal(target)));
        }
        public void UpdateMoveVec(Vector2 vec)
        {
            moveVec = vec;
        }
        private void FollowPath()
        {
            float distance = (path.Peek() - rb.position).magnitude;
            if (distance < goToAccuracy)
            {
                moveVec.Set(0, 0);
                goToActive = false;
                path.Pop();
            }
            moveVec = path.Peek() - rb.position;
            moveVec.Normalize();
        }
    }
}
