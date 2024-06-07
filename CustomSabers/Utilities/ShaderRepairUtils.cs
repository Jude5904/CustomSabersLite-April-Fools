﻿using AssetBundleLoadingTools.Utilities;
using CustomSaber;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomSabersLite.Utilities
{
    internal class ShaderRepairUtils
    {
        /// <summary>
        /// Uses AssetBundleLoadingTools to replace shaders with single-pass-instanced-compatible shaders
        /// </summary>
        /// <param name="sabersObject">The root object of the custom saber</param>
        /// <returns>true if all shaders are replaced successfully</returns>
        public static async Task<bool> RepairSaberShadersAsync(GameObject sabersObject)
        {
            List<Material> materials = ShaderRepair.GetMaterialsFromGameObjectRenderers(sabersObject);
            foreach (Material trailMaterial in sabersObject.GetComponentsInChildren<CustomTrail>().Select(t => t.TrailMaterial))
            {
                if (!materials.Contains(trailMaterial))
                {
                    materials.Add(trailMaterial);
                }
            }
            return (await ShaderRepair.FixShadersOnMaterialsAsync(materials)).AllShadersReplaced;
        }
    }
}
