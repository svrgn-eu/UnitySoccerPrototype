using System.Linq;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    public static class GameObjectExtensions
    {
        #region GetParent: returns the parent of the GameObject this Extension Method is called upon
        /// <summary>
        /// returns the parent of the GameObject this Extension Method is called upon
        /// </summary>
        /// <param name="gameObject">the current GameObject</param>
        /// <returns>the Parent GameObject or null, if this GameObject is root (and subsequently has no parent at all)</returns>
        public static GameObject GetParent(this GameObject gameObject)
        {
            return gameObject.transform.parent.gameObject;
        }
        #endregion GetParent

        #region SetParent: sets a new parent for a given game object
        /// <summary>
        /// sets a new parent for a given game object
        /// </summary>
        /// <param name="gameObject">the current GameObject seeking to be adopted</param>
        /// <param name="NewParent">the new Parent Game Object</param>
        public static void SetParent(this GameObject gameObject, GameObject NewParent)
        {
            gameObject.transform.SetParent(NewParent.transform);
        }
        #endregion SetParent

        #region DestroyAllChildren: Destroys all children of the current game object
        /// <summary>
        /// Destroys all children of the current game object
        /// </summary>
        /// <param name="gameObject">the affected game object</param>
        /// <param name="DoImmediate">should the children be destroyed immediately (Editor Scripts) or not (Game Scripts, end of Frame)</param>
        public static void DestroyAllChildren(this GameObject gameObject, bool DoImmediate = false)
        {
            var tempList = gameObject.transform.Cast<Transform>().ToList();  // this is important due to https://answers.unity.com/questions/678069/destroyimmediate-on-children-not-working-correctly.html
            foreach (Transform child in tempList)  //iterate through all child transforms
            {
                if (DoImmediate)  //check, if we should delete immediately or at the end of the frame
                {
                    GameObject.DestroyImmediate(child.gameObject, true);  // needed in Editor scripts
                }
                else
                {
                    GameObject.Destroy(child.gameObject);  //needed in game scripts
                }
            }
        }
        #endregion DestroyAllChildren

        #region HasChild
        public static bool HasChild(this GameObject gameObject, string Name)
        {
            bool result = false;

            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.name.Equals(Name))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        #endregion HasChild

        #region IsChildOf: Returns a boolean value that indicates whether the transform is a child of a given transform. true if this transform is a child, deep child (child of a child) or identical to this transform, otherwise false.
        /// <summary>
        /// Returns a boolean value that indicates whether the transform is a child of a given transform. true if this transform is a child, deep child (child of a child) or identical to this transform, otherwise false.
        /// </summary>
        /// <param name="gameObject">The potential child</param>
        /// <param name="potentialNestedParent">the potential parent</param>
        /// <returns>true if this transform is a child, deep child (child of a child) or identical to this transform, otherwise false</returns>
        public static bool IsChildOf(this GameObject gameObject, GameObject potentialNestedParent)
        {
            bool result = false;

            if (gameObject.transform.IsChildOf(potentialNestedParent.transform))
            {
                result = true;
            }

            return result;
        }
        #endregion IsChildOf
    }
}
