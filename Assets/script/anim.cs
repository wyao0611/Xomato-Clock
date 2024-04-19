using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    public Animator animator; // 动画控制器
    private Coroutine countdownCoroutine; // 倒计时协程
    

    // 开始按钮点击事件
    public void StartButtonClicked() {
        if (countdownCoroutine != null) {
            StopCoroutine(countdownCoroutine);
        }
        animator.speed = 1; // 恢复动画播放
        countdownCoroutine = StartCoroutine(CountdownCoroutine());
    }

    // 暂停按钮点击事件
     public void PauseButtonClicked() {
        animator.speed = 0; // 暂停动画
    }
    
    // 重置按钮点击事件
    public void ResetButtonClicked() {
        if (countdownCoroutine != null) {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
        animator.speed = 0; // 暂停动画
        animator.ResetTrigger("sit");
        animator.ResetTrigger("eat");
    }

    // 倒计时协程
    private IEnumerator CountdownCoroutine() {
        while (true) {
            // 25分钟sit动画
            animator.SetTrigger("sit");
            yield return new WaitForSeconds(25 * 60);

            // 5分钟eat动画
            animator.SetTrigger("eat");
            yield return new WaitForSeconds(5 * 60);
        }
    }
}