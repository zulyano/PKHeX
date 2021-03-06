namespace PKHeX.Core
{
    /// <summary>
    /// Generation 4 <see cref="SaveFile"/> object that reads Generation 4 PokeStock .stk dumps.
    /// </summary>
    public sealed class Bank4 : BulkStorage
    {
        public Bank4(byte[] data) : base(data, typeof(PK4), 0)
        {
            Personal = PersonalTable.HGSS;
            Version = GameVersion.HGSS;
            HeldItems = Legal.HeldItems_HGSS;
        }

        public override string PlayTimeString => Checksums.CRC16(Data, 0, Data.Length).ToString("X4");
        protected override string BAKText => PlayTimeString;
        public override string Extension => ".stk";
        public override string Filter { get; } = "PokeStock G4 Storage|*.stk*";

        public override int BoxCount => 64;
        private const int BoxNameSize = 0x18;

        private int BoxDataSize => SlotsPerBox * SIZE_STORED;
        public override int GetBoxOffset(int box) => Box + (BoxDataSize * box);
        public override string GetBoxName(int box) => GetString(GetBoxNameOffset(box), BoxNameSize / 2);
        private static int GetBoxNameOffset(int box) => 0x3FC00 + (0x19 * box);
    }
}