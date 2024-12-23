﻿using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Maskable.Editor {
    public static class PackageResources {
        static string _packagePath = string.Empty;
        static string _resourcesPath = string.Empty;

        public static string packagePath {
            get {
                if (string.IsNullOrEmpty(_packagePath)) {
                    _packagePath = SearchForPackageRootPath();
                    if (string.IsNullOrEmpty(_packagePath))
                        Debug.LogError(
                            "Unable to locate Maskable root folder. " +
                            "Make sure the package has been installed correctly.");
                }
                return _packagePath;
            }
        }

        public static string generatedShaderResourcesPath {
            get {
                if (string.IsNullOrEmpty(_resourcesPath))
                    _resourcesPath = CombinePath(packagePath, "Shaders", "Generated", "Resources");
                return _resourcesPath;
            }
        }

        public const string MaskingCsGUID = "0bac33ade27cf4542bd53b1b13d90941";

        static string SearchForPackageRootPath() {
            var MaskingCsPath = AssetDatabase.GUIDToAssetPath(MaskingCsGUID);
            if (string.IsNullOrEmpty(MaskingCsPath))
                return "";
            var scriptsDir = Path.GetDirectoryName(MaskingCsPath);
            var packageDir = Path.GetDirectoryName(scriptsDir);
            return packageDir;
        }

        static string CombinePath(params string[] paths) {
            return (paths == null || paths.Length == 0) ? "" : paths.Aggregate(Path.Combine);
        }
    }
}
