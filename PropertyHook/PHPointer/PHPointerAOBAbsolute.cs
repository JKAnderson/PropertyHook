namespace PropertyHook
{
    public class PHPointerAOBAbsolute : PHPointerAOB
    {
        public PHPointerAOBAbsolute(PHook parent, byte?[] aob, params int[] offsets) : base(parent, aob, offsets) { }
        
        internal override void ScanAOB(AOBScanner scanner)
        {
            AOBResult = scanner.Scan(AOB);
        }
    }
}
