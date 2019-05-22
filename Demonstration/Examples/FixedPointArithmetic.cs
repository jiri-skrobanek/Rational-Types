namespace Demonstration
{
    public struct FixedPointNumber
    {
        private ulong _whole, _part;

        public bool Sign
        {
            get; set;
        }

        public uint Whole
        {
            get
            {
                return (uint)_whole;
            }
            set
            {
                _whole = value;
            }
        }

        public uint Part
        {
            get
            {
                return (uint)_part;
            }
            set
            {
                _part = value;
            }
        }

        public FixedPointNumber(uint whole, uint part, bool sign)
        {
            _whole = whole;
            _part = part;
            Sign = sign;
        }

        public static FixedPointNumber Times(FixedPointNumber a, FixedPointNumber b)
        {
            uint whole = (uint)((a._whole * b._part + a._part * b._whole + (a._whole * b._whole << 32)) >> 32);
            uint part = (uint)((a._part * b._part + (a._whole * b._part + a._part * b._whole << 32)) >> 32);
            return new FixedPointNumber(whole, part, a.Sign ^ b.Sign);
        }
    }
}