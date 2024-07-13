using Runtime.Assets.Runtime.Scripts.MigrateToHelpers;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Base
{
    public class KeepPlayersInsidePolygon2D : MonoBehaviour
    {
        #region Properties

        [Header("The Collider inside of which the objects (Colliders2D) should be kept")]
        public PolygonCollider2D Collider;

        [Header("Which Layers to keep inside the Collider")]
        public LayerMask LayersToCheck;

        private Queue<GameObject> objectsAboutToExit = new Queue<GameObject>();

        #endregion Properties

        #region Methods


        private void OnTriggerExit2D(Collider2D collider)
        {
            if (LayerMaskHelper.Contains(this.LayersToCheck, collider.gameObject.layer))
            { 
                //TODO: revert gameobject back into bounds
                
            }
        }

        private void FixedUpdate()
        {

        }

        #endregion Methods
    }
}
