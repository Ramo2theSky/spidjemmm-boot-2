using System;
using UnityEngine.SceneManagement;

namespace Furari {

	[Serializable]
	public class SceneReference : IEquatable<SceneReference>, IEquatable<Scene> {

		// To do: it should be internal. Reason for currently public is the drawer need access to these fields.
		public UnityEngine.Object asset;
		public string path;

		public string Path => path;
		public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);
		public int BuildIndex => SceneUtility.GetBuildIndexByScenePath(Path);
		public Scene LoadedScene => SceneManager.GetSceneByPath(Path);

		public SceneReference(Scene scene) {
			path = scene.path;
		}
		//-----------------------------------------

        public bool Equals(Scene other) => path == other.path;
        public bool Equals(SceneReference other) => path == other.path;
        public override bool Equals(object obj) {
			if (obj is Scene sc) { return Equals(sc); }
			if (obj is SceneReference sr) { return Equals(sr); }
			return false;
        }
        public override int GetHashCode() => base.GetHashCode();
        public static bool operator ==(SceneReference sref, Scene scene) => sref.Equals(scene);
		public static bool operator !=(SceneReference sref, Scene scene) => !sref.Equals(scene);
		public static bool operator ==(SceneReference a, SceneReference b) => a.Equals(b);
		public static bool operator !=(SceneReference a, SceneReference b) => !a.Equals(b);
	}

}