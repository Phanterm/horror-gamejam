using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

public class ImageAnimation : MonoBehaviour {

  public float speed;

  private Animator animator;
  private Image image;
  private int curIdx;
  private float curTime;
  private List<Sprite> sprites;
  private bool valid;

  void Awake() {
    image = GetComponent<Image> ();
    Color white = Color.white;
    white.a = 1;
    image.color = white;
    animator = GetComponent<Animator> ();
    if (animator.runtimeAnimatorController == null) {
      valid = false;
    } else {
      sprites = GetSpritesFromAnimator(animator);
      curIdx = 0;
      curTime = speed;
      valid = true;
    }
  }

  public void Reset(){
    Awake();
  }

  public void Flush(){
    Color white = Color.white;
    white.a = 0;
    image.color = white;
    valid = false;
  }

  void Update () {
     if (valid) {
       curTime -= Time.deltaTime;
       if ( curTime < 0 )
       {
          curTime = speed;
          if (curIdx >= sprites.Count){
            curIdx = 0;
          }
          image.sprite = sprites[curIdx];
          curIdx++;
       }
     }
  }

  #if UNITY_EDITOR
   public static List<Sprite> GetSpritesFromAnimator(Animator anim)
   {
       List<Sprite> _allSprites = new List<Sprite> ();
       foreach(AnimationClip ac in anim.runtimeAnimatorController.animationClips)
       {
           _allSprites.AddRange(GetSpritesFromClip(ac));
       }
       return _allSprites;
   }

   private static List<Sprite> GetSpritesFromClip(AnimationClip clip)
   {
       var _sprites = new List<Sprite> ();
       if (clip != null)
       {
           foreach (var binding in AnimationUtility.GetObjectReferenceCurveBindings (clip))
           {
               ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve (clip, binding);
               foreach (var frame in keyframes) {
                   _sprites.Add ((Sprite)frame.value);
               }
           }
       }
       return _sprites;
   }
  #endif
}