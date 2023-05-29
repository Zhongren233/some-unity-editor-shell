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
                foreach (var editorCurveBinding in editorCurveBindings)
                {
                    Debug.Log(editorCurveBinding.propertyName);
                }
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalPosition.x"), "path_3440880065",0);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalPosition.y"), "path_3440880065",0);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalPosition.z"), "path_3440880065",0);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalPosition.x"), "",0.08f);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalPosition.y"), "",1.8f);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalPosition.z"), "",5f);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalRotation.x"), "",0);
                // ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalRotation.w"), "",1);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalRotation.y"), "",0);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalRotation.z"), "",0);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_LocalRotation.z"), "",0);
                ReplaceAllValue(animationClip, editorCurveBindings.First(i => i.propertyName == "m_FocalLength"), "",50);
            }
        }

        private static void ReplaceAllValue(AnimationClip animationClip, EditorCurveBinding binding, string path ,float value)
        {
            var animationCurve = AnimationUtility.GetEditorCurve(animationClip, binding);
            var tmpAnimationCurve = new AnimationCurve();
            foreach (var animationCurveKey in animationCurve.keys)
            {
                tmpAnimationCurve.AddKey(animationCurveKey.time, value);
            }
            
            animationClip.SetCurve(path, binding.type, binding.propertyName, tmpAnimationCurve);
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
