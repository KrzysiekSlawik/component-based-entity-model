using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityComponent
{
    [CreateAssetMenu(fileName = "Idle", menuName = "IdleBehaviour", order = 2)]
    public class IdleBehaviour : BBehaviour
    {
        private float goToTimer;
        public float goToCD;
        public float goToDist;
        // Update is called once per frame
        override public void Update()
        {
            Debug.Log("behaviour is alive");
            goToTimer += Time.deltaTime;
            if(!brain.movement.GoToActive && goToTimer > goToCD)
            {
                goToTimer = 0;
                brain.movement.GoTo(new Vector2(brain.self.position.x + Random.Range(-goToDist, goToDist), brain.self.position.y + Random.Range(-goToDist, goToDist)));
            }
        }
    }
}