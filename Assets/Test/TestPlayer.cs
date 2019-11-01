#if TEST
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class Player
    {
        [SetUp]
        public void Init()
        {
            SceneManager.LoadScene("SampleScene");
        }

        [UnityTest]
        public IEnumerator Run()
        {
            Character.Player player = null;
            UInput.InputAuto input = null;
            SetupPlayer(ref player, ref input);

            var playerTransform = player.gameObject.transform;
            var startPosX = playerTransform.position.x;
            input.RequestHorizontal(1.0f, 0.5f);

            yield return new WaitForSeconds(0.5f);

            var movedPosX = playerTransform.position.x;

            Debug.Log("start:" + startPosX + " moved:" + movedPosX);
            Assert.AreEqual(true, (movedPosX >= startPosX));
            Assert.AreEqual(true, player.CheckAnimatorStateName("run"));

            yield return null;
        }

        [UnityTest]
        public IEnumerator Jump()
        {
            Character.Player player = null;
            UInput.InputAuto input = null;
            SetupPlayer(ref player, ref input);

            var playerTransform = player.gameObject.transform;

            // 着地するまで少し待つ
            yield return new WaitForSeconds(0.1f);

            var startPosY = playerTransform.position.y; 

            input.RequestJump(0.1f);
            
            // 空中に上がるまで少し待つ
            yield return new WaitForSeconds(0.2f);

            // 飛べたか
            var movedPosY = playerTransform.position.y;
            Debug.Log("start:" + startPosY + " moved:" + movedPosY);
            Assert.AreEqual(true, (movedPosY >= startPosY));
            Assert.AreEqual(true, player.CheckAnimatorStateName("jump-up"));

            // 着地まで待つ
            yield return new WaitForSeconds(1.0f);

            // 着地できたか
            movedPosY = playerTransform.position.y;
            CheckNotMove(startPosY, movedPosY);

            // ジャンプモーションが終了してアイドルに戻っているか
            Assert.AreEqual(true, player.CheckAnimatorStateName("idle"));

            yield return null;
        }

        [UnityTest]
        public IEnumerator JumpMoveRight()
        {
            const float dir = 1.0f;

            Character.Player player = null;
            UInput.InputAuto input = null;
            SetupPlayer(ref player, ref input);

            var playerTransform = player.gameObject.transform;

            // 着地するまで少し待つ
            yield return new WaitForSeconds(0.1f);

            var startPosX = playerTransform.position.x;

            input.RequestJump(0.1f);
            yield return new WaitForSeconds(0.1f);

            input.RequestHorizontal(dir, 0.5f);
            yield return new WaitForSeconds(0.5f);

            var movedPosX = playerTransform.position.x;
            var diff = movedPosX - startPosX;
            Debug.Log("start:" + startPosX + " moved:" + movedPosX + " diff:" + diff);

            const float checkDistance = 1.0f;
            Assert.AreEqual(true, (diff >= checkDistance * dir));

            yield return null;
        }

        [UnityTest]
        public IEnumerator JumpMoveLeft()
        {
            const float dir = -1.0f;

            Character.Player player = null;
            UInput.InputAuto input = null;
            SetupPlayer(ref player, ref input);

            var playerTransform = player.gameObject.transform;

            // 着地するまで少し待つ
            yield return new WaitForSeconds(0.1f);

            var startPosX = playerTransform.position.x;

            input.RequestJump(0.1f);
            yield return new WaitForSeconds(0.1f);

            input.RequestHorizontal(dir, 0.5f);
            yield return new WaitForSeconds(0.5f);

            var movedPosX = playerTransform.position.x;
            var diff = movedPosX - startPosX;
            Debug.Log("start:" + startPosX + " moved:" + movedPosX + " diff:" + diff);

            const float checkDistance = 1.0f;
            Assert.AreEqual(true, (diff <= checkDistance * dir));

            yield return null;
        }

        [UnityTest]
        public IEnumerator JumpHitWallAndSlide()
        {
            Character.Player player = null;
            UInput.InputAuto input = null;
            SetupPlayer(ref player, ref input);
            var playerTransform = player.gameObject.transform;

            // 着地するまで少し待つ
            yield return new WaitForSeconds(0.1f);

            // 壁際までワープ
            var warpPos = playerTransform.position;
            warpPos.x = 4.0f; 
            playerTransform.position = warpPos;

            var startY = playerTransform.position.y;

            input.RequestJump(0.1f);
            yield return new WaitForSeconds(0.1f);

            input.RequestHorizontal(1.0f, 1.0f);
            yield return new WaitForSeconds(1.0f);

            var movedY = playerTransform.position.y;
            CheckNotMove(startY, movedY);

            Assert.AreEqual(true, player.CheckAnimatorStateName("run"));

            yield return null;
        }

        // Utility
        void SetupPlayer(ref Character.Player player, ref UInput.InputAuto input)
        {
            var playerObject = GameObject.Find("Player");
            Assert.IsNotNull(playerObject);

            input = playerObject.AddComponent<UInput.InputAuto>();
            Assert.IsNotNull(input);

            player = playerObject.GetComponent<Character.Player>();
            Assert.IsNotNull(player);

            player.SwapInput(input);
        }

        void CheckNotMove(float start, float moved, float threshold = 0.01f)
        {
            var diff = Mathf.Abs(moved - start);
            Debug.Log("start:" + start + " moved:" + moved + " diff:" + diff);
            Assert.AreEqual(true, (diff <= threshold));
        }
    }
}
#endif // TEST