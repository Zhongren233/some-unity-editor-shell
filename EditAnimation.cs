using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class AnimationChangeTool : EditorWindow
    {
        [MenuItem("Tools/AnimationChangeTool")]
        private static void DoChangeAnimation()
        {
            var animationClips = GetAnimationClips();
            foreach (var animationClip in animationClips)
            {
                var editorCurveBindings = AnimationUtility.GetCurveBindings(animationClip);
                var xBinding = editorCurveBindings.First(i => i.propertyName == "m_LocalPosition.x");
                var yBinding = editorCurveBindings.First(i => i.propertyName == "m_LocalPosition.y");
                var zBinding = editorCurveBindings.First(i => i.propertyName == "m_LocalPosition.z");
                ReplaceAllValue(animationClip, xBinding, 0);
                ReplaceAllValue(animationClip, yBinding, 2.3f);
                ReplaceAllValue(animationClip, zBinding, 6.2f);
            }
        }

        private static void ReplaceAllValue(AnimationClip animationClip, EditorCurveBinding binding, float value)
        {
            var animationCurve = AnimationUtility.GetEditorCurve(animationClip, binding);
            var tmpAnimationCurve = new AnimationCurve();
            foreach (var animationCurveKey in animationCurve.keys)
            {
                tmpAnimationCurve.AddKey(animationCurveKey.time, value);
            }
            animationClip.SetCurve("", typeof(Transform), binding.propertyName, tmpAnimationCurve);
        }


        private static List<AnimationClip> GetAnimationClips()
        {
            var allAnimatingClips = new List<AnimationClip>();

            var selAnimations = Selection.objects;
            foreach (var a in selAnimations)
            {
                if (a is AnimationClip clip)
                {
                    allAnimatingClips.Add(clip);
                }
            }

            return allAnimatingClips;
        }
    }
}