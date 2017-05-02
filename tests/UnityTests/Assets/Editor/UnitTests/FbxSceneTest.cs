// ***********************************************************************
// Copyright (c) 2017 Unity Technologies. All rights reserved.
//
// Licensed under the ##LICENSENAME##.
// See LICENSE.md file in the project root for full license information.
// ***********************************************************************
using NUnit.Framework;
using FbxSdk;

namespace UnitTests
{
    public class FbxSceneTest : Base<FbxScene>
    {
        [Test]
        public void TestBasics()
        {
            using (var scene = FbxScene.Create(Manager, "scene")) {
                // Just call every function. TODO: and test them at least minimally!
                scene.GetGlobalSettings();
                scene.GetRootNode();

                var docInfo = FbxDocumentInfo.Create(Manager, "info");
                scene.SetDocumentInfo(docInfo);
                Assert.AreEqual(docInfo, scene.GetDocumentInfo());

                docInfo = FbxDocumentInfo.Create(Manager, "info2");
                scene.SetSceneInfo(docInfo);
                Assert.AreEqual(docInfo, scene.GetSceneInfo());

                scene.Clear();
            }
        }

        [Test]
        public override void TestDisposeDestroy ()
        {
           // The scene destroys recursively even if you ask it not to
           DoTestDisposeDestroy(canDestroyNonRecursive: false);
        }

        [Test]
        public void TestNodeCount ()
        {
            using (FbxScene newScene = FbxScene.Create (Manager, ""))
            {
                Assert.GreaterOrEqual (newScene.GetNodeCount (), 0);
            }
        }

        [Test]
        public void TestAddPose()
        {
            using (FbxScene newScene = FbxScene.Create (Manager, "")) {
                FbxPose fbxPose = FbxPose.Create (Manager, "pose");
                bool result = newScene.AddPose (fbxPose);
                Assert.IsTrue (result);
                Assert.AreEqual (fbxPose, newScene.GetPose (0));

                // test null
                Assert.That (() => { newScene.AddPose(null); }, Throws.Exception.TypeOf<System.NullReferenceException>());

                // test invalid
                fbxPose.Destroy();
                Assert.That (() => { newScene.AddPose(fbxPose); }, Throws.Exception.TypeOf<System.ArgumentNullException>());
            }
        }
    }
}
