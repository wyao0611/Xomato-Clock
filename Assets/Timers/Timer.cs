﻿using UnityEngine ;
using UnityEngine.UI ;
using UnityEngine.Events ;
using System.Collections ;

public class Timer : MonoBehaviour {
   [Header ("Timer UI references :")]
   [SerializeField] private Image uiFillImage ;
   [SerializeField] private Text uiText ;

   public int Duration { get; private set; }

   public bool IsPaused { get; private set; }

   private int remainingDuration ;

   // Events --
   private UnityAction onTimerBeginAction ;
   private UnityAction<int> onTimerChangeAction ;
   private UnityAction onTimerEndAction ;
   private UnityAction<bool> onTimerPauseAction ;

   private Coroutine countdownCoroutine;
   

   private void Awake () {
      ResetCountdown();
   }

   private IEnumerator CountdownCoroutine() {
      while (true) {
         // 25分钟倒计时
         Duration = remainingDuration = 25 * 60;
         while (remainingDuration > 0) {
            if (!IsPaused) {
               remainingDuration--;
               // 更新UI
               uiText.text = $"{remainingDuration / 60:D2}:{remainingDuration % 60:D2}";
               uiFillImage.fillAmount = (float)remainingDuration / Duration;
            }
            yield return new WaitForSeconds(1);
         }

         // 5分钟倒计时
         Duration = remainingDuration = 5 * 60;
         while (remainingDuration > 0) {
            if (!IsPaused) {
               remainingDuration--;
               // 更新UI
               uiText.text = $"{remainingDuration / 60:D2}:{remainingDuration % 60:D2}";
               uiFillImage.fillAmount = (float)remainingDuration / Duration;
            }
            yield return new WaitForSeconds(1);
         }
      }
   }

   public void StartCountdown() {
      if (countdownCoroutine != null) {
         StopCoroutine(countdownCoroutine);
      }
      countdownCoroutine = StartCoroutine(CountdownCoroutine());
   }

    public void StopCountdown() {
    if (countdownCoroutine != null) {
        StopCoroutine(countdownCoroutine);
        countdownCoroutine = null;
    }
   }

   public void ResetCountdown() {
      StopCountdown();
      uiText.text = "00:00";
      uiFillImage.fillAmount = 0f;
      Duration = remainingDuration = 0;
      
      
   }

  
   
   

   
      
   


   //-- Events ----------------------------------
   public Timer OnBegin (UnityAction action) {
      onTimerBeginAction = action ;
      return this ;
   }

   public Timer OnChange (UnityAction<int> action) {
      onTimerChangeAction = action ;
      return this ;
   }

   public Timer OnEnd (UnityAction action) {
      onTimerEndAction = action ;
      return this ;
   }

   public Timer OnPause (UnityAction<bool> action) {
      onTimerPauseAction = action ;
      return this ;
   }





   public void Begin () {
      if (onTimerBeginAction != null)
         onTimerBeginAction.Invoke () ;

      StopAllCoroutines () ;
      StartCoroutine (UpdateTimer ()) ;
   }

   private IEnumerator UpdateTimer () {
      while (remainingDuration > 0) {
         if (!IsPaused) {
            if (onTimerChangeAction != null)
               onTimerChangeAction.Invoke (remainingDuration) ;

            UpdateUI (remainingDuration) ;
            remainingDuration-- ;
         }
         yield return new WaitForSeconds (1f) ;
      }
      End () ;
   }

   private void UpdateUI (int seconds) {
      uiText.text = string.Format ("{0:D2}:{1:D2}", seconds / 60, seconds % 60) ;
      uiFillImage.fillAmount = Mathf.InverseLerp (0, Duration, seconds) ;
   }

   public void End () {
      if (onTimerEndAction != null)
         onTimerEndAction.Invoke () ;

      ResetCountdown();
   }


   private void OnDestroy () {
      StopAllCoroutines () ;
   }
 
}
