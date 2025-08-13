using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public TAsset Load<TAsset>(string path) where TAsset : UnityEngine.Object
        {
            return Resources.Load<TAsset>(path);
        }

        public List<string> GetAssetsListByPath(string label, Type type = null)
        {
            throw new NotImplementedException();
        }

        public TAsset[] LoadAll<TAsset>(List<string> keys) where TAsset : class
        {
            throw new NotImplementedException();
        }
    }
}