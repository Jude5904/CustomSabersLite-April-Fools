﻿using UnityEngine;
using ReflectionUtil = IPA.Utilities.ReflectionUtil;

namespace CustomSaber.Utilities
{
    internal class CustomSaberTrail : SaberTrail
    {
        public SaberTrailRenderer TrailRendererPrefab
        {
            get { return ReflectionUtil.GetField<SaberTrailRenderer, SaberTrail>(this, "_trailRendererPrefab"); }
            set { ReflectionUtil.SetField<SaberTrail, SaberTrailRenderer>(this, "_trailRendererPrefab", value); }
        }

        public int SamplingFrequency
        {
            get { return ReflectionUtil.GetField<int, SaberTrail>(this, "_samplingFrequency"); }
            set { ReflectionUtil.SetField<SaberTrail, int>(this, "_samplingFrequency", value); }
        }

        public int Granularity
        {
            get { return ReflectionUtil.GetField<int, SaberTrail>(this, "_granularity"); }
            set { ReflectionUtil.SetField<SaberTrail, int>(this, "_granularity", value); }
        }

        public Color Color
        {
            get { return ReflectionUtil.GetField<Color, SaberTrail>(this, "_color"); }
            set { ReflectionUtil.SetField<SaberTrail, Color>(this, "_color", value); }
        }

        public IBladeMovementData MovementData
        {
            get { return ReflectionUtil.GetField<IBladeMovementData, SaberTrail>(this, "_movementData"); }
            set { ReflectionUtil.SetField<SaberTrail, IBladeMovementData>(this, "_movementData", value); }
        }

        public SaberTrailRenderer TrailRenderer
        {
            get { return ReflectionUtil.GetField<SaberTrailRenderer, SaberTrail>(this, "_trailRenderer"); }
            set { ReflectionUtil.SetField<SaberTrail, SaberTrailRenderer>(this, "_trailRenderer", value); }
        }
        
        public MeshRenderer MeshRenderer
        {
            get { return ReflectionUtil.GetField<MeshRenderer, SaberTrailRenderer>(TrailRenderer, "_meshRenderer"); }
            set { ReflectionUtil.SetField<SaberTrailRenderer, MeshRenderer>(TrailRenderer, "_meshRenderer", value); }
        }

        public TrailElementCollection TrailElementCollection
        {
            get { return ReflectionUtil.GetField<TrailElementCollection, SaberTrail>(this, "_trailElementCollection"); }
            set { ReflectionUtil.SetField<SaberTrail, TrailElementCollection>(this, "_trailElementCollection", value); }
        }

        private Transform customTrailTopTransform;

        private Transform customTrailBottomTransform;

        private readonly SaberMovementData customTrailMovementData = new SaberMovementData();

        private Vector3 customTrailTopPos;

        private Vector3 customTrailBottomPos;

        public SaberMovementData CustomTrailMovementData => customTrailMovementData;

        void Awake()
        {
            // i'm stupid and i don't know why i need this but i do so yer
        }

        public void Setup(Transform topTransform, Transform bottomTransform)
        {
            // Custom saber trails don't all work well with the regular trail values so we have to use their settings (currently done by handler)
            // Extra settings may be needed
            customTrailTopTransform = topTransform;
            customTrailBottomTransform = bottomTransform;
            gameObject.layer = 12;
        }

        void Update()
        {
            if (gameObject.activeInHierarchy)
            {
                customTrailTopPos = customTrailTopTransform.position;
                customTrailBottomPos = customTrailBottomTransform.position;
                customTrailMovementData.AddNewData(customTrailTopPos, customTrailBottomPos, TimeHelper.time);
            }
        }
    }
}