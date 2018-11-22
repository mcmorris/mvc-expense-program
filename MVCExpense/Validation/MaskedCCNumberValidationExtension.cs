namespace ExpenseModel.Validation
{
    using global::Validation.Properties;

    public static class MaskedCCNumberValidationExtension
    {
        private const int OctetLength = 4;
        private const string Mask = "********";

        public static string IsValidMaskedCC(this string potentialMaskedCC)
        {
            if (potentialMaskedCC.Length != 16) { return Resources.InvalidCCLength; }

            var firstOctet = potentialMaskedCC.Substring(0, MaskedCCNumberValidationExtension.OctetLength);
            if (int.TryParse(firstOctet, out var result) == false) { return Resources.InvalidCCNumber; }

            var lastOctet = potentialMaskedCC.Substring(MaskedCCNumberValidationExtension.OctetLength * 3, MaskedCCNumberValidationExtension.OctetLength);
            if (int.TryParse(lastOctet, out result) == false) { return Resources.InvalidCCNumber; }

            if (potentialMaskedCC.IndexOf("********") != MaskedCCNumberValidationExtension.OctetLength) { return Resources.CCMustBeMasked; }
            return null;
        }
    }
}
