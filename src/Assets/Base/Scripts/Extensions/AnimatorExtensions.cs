using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    public static class AnimatorExtensions
    {
        #region GetClip
        public static AnimationClip GetClip(this Animator animator, string ClipName)
        {
            AnimationClip result = default;

            for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                if (animator.runtimeAnimatorController.animationClips[i].name.Equals(ClipName))
                {
                    result = animator.runtimeAnimatorController.animationClips[i];
                    break;
                }
            }

            return result;
        }
        #endregion GetClip
    }
}
