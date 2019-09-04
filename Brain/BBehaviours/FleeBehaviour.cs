using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityComponent
{
    [CreateAssetMenu(fileName = "Flee", menuName = "FleeBehaviour", order = 3)]
    public class FleeBehaviour : BBehaviour
    {
        override public void Update()
        {
            Vector3 playerPos = brain.PlayerPosition();
            brain.movement.GoTo(new Vector2(brain.self.position.x * 2 - playerPos.x, brain.self.position.y * 2 - playerPos.y));
        }
    }
}
