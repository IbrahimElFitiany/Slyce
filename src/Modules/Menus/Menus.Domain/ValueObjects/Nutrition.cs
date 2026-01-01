namespace Menus.Domain.ValueObjects
{
    public sealed class Nutrition
    {
        public int Calories { get; }
        public int ProteinGrams { get; }
        public int CarbGrams { get; }
        public int FatGrams { get; }
        public int SodiumMg { get; }
        public int SugarGrams { get; }

        private Nutrition() { }
        public Nutrition(
            int calories,
            int proteinGrams,
            int carbGrams,
            int fatGrams,
            int sodiumMg = 0,
            int sugarGrams = 0)
        {

            Calories = calories;
            ProteinGrams = proteinGrams;
            CarbGrams = carbGrams;
            FatGrams = fatGrams;
            SodiumMg = sodiumMg;
            SugarGrams = sugarGrams;
        }
    }
}
