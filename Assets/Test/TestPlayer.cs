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
            var playerObject = GameObject.Find("Player");
            Assert.IsNotNull(playerObject);

            var inputAutoMove = playerObject.AddComponent<UInput.InputAutoMove>();
            Assert.IsNotNull(inputAutoMove);

            var player = playerObject.GetComponent<Character.Player>();
            Assert.IsNotNull(player);

            var startPosX = playerObject.transform.position.x;

            player.SwapInput(inputAutoMove);

            yield return new WaitForSeconds(0.5f);

            var movedPosX = playerObject.transform.position.x;

            Debug.Log("start:" + startPosX + " moved:" + movedPosX);
            Assert.AreEqual(true, (movedPosX >= startPosX));
            Assert.AreEqual(true, player.CheckAnimatorStateName("run"));

            yield return null;
        }

        [UnityTest]
        public IEnumerator Jump()
        {
            var playerObject = GameObject.Find("Player");
            Assert.IsNotNull(playerObject);

            var inputAutoJump = playerObject.AddComponent<UInput.InputAutoJump>();
            Assert.IsNotNull(inputAutoJump);

            var player = playerObject.GetComponent<Character.Player>();
            Assert.IsNotNull(player);

            // 着地するまで少し待つ
            yield return new WaitForSeconds(0.5f);

            var startPosY = playerObject.transform.position.y;
            player.SwapInput(inputAutoJump);

            // 空中に上がるまで少し待つ
            yield return new WaitForSeconds(0.2f);

            // 飛べたか
            var movedPosY = playerObject.transform.position.y;
            Debug.Log("start:" + startPosY + " moved:" + movedPosY);
            Assert.AreEqual(true, (movedPosY >= startPosY));
            Assert.AreEqual(true, player.CheckAnimatorStateName("jump-up"));

            // 着地まで待つ
            yield return new WaitForSeconds(1.0f);

            // 着地できたか
            movedPosY = playerObject.transform.position.y;
            var diff = Mathf.Abs(movedPosY - startPosY);
            Debug.Log("start:" + startPosY + " moved:" + movedPosY + " diff:" + diff);
            Assert.AreEqual(true, (diff <= 0.001f));
            Assert.AreEqual(true, player.CheckAnimatorStateName("idle"));

            yield return null;
        }
    }
}
#endif // TEST