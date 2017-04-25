﻿// ***********************************************************************
// Copyright (c) 2017 Unity Technologies. All rights reserved.  
//
// Licensed under the ##LICENSENAME##. 
// See LICENSE.md file in the project root for full license information.
// ***********************************************************************

using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using FbxSdk;

namespace UnitTests
{
    public class FbxGeometryBaseTest : Base<FbxGeometryBase>
    {
        [Test]
        public void TestBasics()
        {
            var geometryBase = CreateObject ("geometry base");

            geometryBase.InitControlPoints (24);
            Assert.AreEqual (geometryBase.GetControlPointsCount (), 24);
            geometryBase.SetControlPointAt(new FbxVector4(1,2,3,4), 0);
            Assert.AreEqual(new FbxVector4(1,2,3,4), geometryBase.GetControlPointAt(0));

            int layerId0 = geometryBase.CreateLayer();
            int layerId1 = geometryBase.CreateLayer();
            var layer0 = geometryBase.GetLayer(layerId0);
            var layer1 = geometryBase.GetLayer(layerId1);
            Assert.AreNotEqual(layer0, layer1);
        }

        [Test]
        public void TestInitNegativeControlPoints ()
        {
            using (FbxGeometryBase geometryBase = CreateObject ("geometry base")) {
                // make sure this doesn't crash
                geometryBase.InitControlPoints (-1);
            }
        }

        [Test]
        public void TestSetControlPointAt ()
        {
            using (FbxGeometryBase geometryBase = CreateObject ("geometry base")) {
                geometryBase.InitControlPoints (5);
                FbxVector4 vector = new FbxVector4 ();
                geometryBase.SetControlPointAt (vector, 0);
                Assert.AreEqual (vector, geometryBase.GetControlPointAt (0));
            }
            using (FbxGeometryBase geometryBase = CreateObject ("geometry base")) {
                FbxVector4 vector = new FbxVector4 ();
                Assert.That (() => geometryBase.SetControlPointAt (vector, 0), Throws.Exception.TypeOf<System.IndexOutOfRangeException>());
            }
            using (FbxGeometryBase geometryBase = CreateObject ("geometry base")) {
                geometryBase.InitControlPoints (5);
                FbxVector4 vector = new FbxVector4 ();
                Assert.That (() => { geometryBase.SetControlPointAt (vector, -1); }, Throws.Exception.TypeOf<System.IndexOutOfRangeException>());
                Assert.That (() => { geometryBase.SetControlPointAt (vector, 6); }, Throws.Exception.TypeOf<System.IndexOutOfRangeException>());
            }
        }

        [Test]
        public void TestGetControlPointAtInvalidIndex ()
        {
            using (FbxGeometryBase geometryBase = CreateObject ("geometry base")) {
                geometryBase.InitControlPoints (5);
                // make sure it doesn't crash
                FbxVector4 vector = geometryBase.GetControlPointAt (-1);
                vector.X = 0;

                vector = geometryBase.GetControlPointAt(6);
                vector.X = 0;
            }
        }

        [Test]
        public void TestGetUninitializedControlPoint ()
        {
            using (FbxGeometryBase geometryBase = CreateObject ("geometry base")) {
                geometryBase.InitControlPoints (5);
                // just make sure it doesn't crash
                FbxVector4 vector = geometryBase.GetControlPointAt (0);
                vector.X = 0;
            }
        }
    }
}
