using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EntityComponent
{
    [RequireComponent(typeof(EntityComponent.EntityMovement))]
    public class PlayerControler : MonoBehaviour
    {
        private EntityMovement moveComp;
        private Vector2 moveVector;
        [SerializeField]
        private bool useMouse = false;
        public void Start()
        {
            moveComp = GetComponent<EntityComponent.EntityMovement>();
        }
        public void Update()
        {
            if(!useMouse)
            {
                moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                moveComp.UpdateMoveVec(moveVector);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                moveVector = MouseClick();
                moveComp.GoTo(moveVector);
            }
        }
        private Vector2 MouseClick()
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            return new Vector2(pz.x, pz.y);
        }
        
    }
}

