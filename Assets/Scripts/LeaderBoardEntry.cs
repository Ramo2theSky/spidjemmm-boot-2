using System;

namespace Trash {
    [Serializable]
    public struct LeaderBoardEntry : IEquatable<LeaderBoardEntry>, IComparable<LeaderBoardEntry> {

		public string Name;
		public float Time;
        public int CompareTo(LeaderBoardEntry other) => Time.CompareTo(other.Time);
        public bool Equals(LeaderBoardEntry other) => Name == other.Name && Time == other.Time;
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();

        public static bool operator ==(LeaderBoardEntry a, LeaderBoardEntry b) => a.Equals(b);
        public static bool operator !=(LeaderBoardEntry a, LeaderBoardEntry b) => !a.Equals(b);
    }
}