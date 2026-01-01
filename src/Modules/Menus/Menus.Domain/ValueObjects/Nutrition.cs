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

        private const int caloriesPerGramOfFat = 9;
        private const int caloriesPerGramOfProtein = 4;
        private const int caloriesPerGramOfCarb = 4;
        private Nutrition() { }
        public Nutrition(
            int calories,
            int proteinGrams,
            int carbGrams,
            int fatGrams,
            int sodiumMg = 0,
            int sugarGrams = 0)
        {
            if (calories < 0)
                throw new ArgumentException("Calories cannot be negative");

            if (proteinGrams < 0 || carbGrams < 0 || fatGrams < 0)
                throw new ArgumentException("Macros cannot be negative");

            var totalMacroCalories = proteinGrams * caloriesPerGramOfProtein + carbGrams * caloriesPerGramOfCarb + fatGrams * caloriesPerGramOfFat;

            if (totalMacroCalories > calories)
                throw new ArgumentException("Macros exceed total calories");

            Calories = calories;
            ProteinGrams = proteinGrams;
            CarbGrams = carbGrams;
            FatGrams = fatGrams;
            SodiumMg = sodiumMg;
            SugarGrams = sugarGrams;
        }
    }
}