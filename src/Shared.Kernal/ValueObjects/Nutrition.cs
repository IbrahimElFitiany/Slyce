namespace Shared.Kernal.ValueObjects
{
    public sealed class Nutrition
    {
        private const decimal CaloriesPerGramOfFat = 9;
        private const decimal CaloriesPerGramOfProtein = 4;
        private const decimal CaloriesPerGramOfCarb = 4;

        public decimal Calories { get; }
        public decimal TotalFat { get; }
        public decimal SaturatedFat { get; }
        public decimal TransFat { get; }
        public decimal Cholesterol { get; }
        public decimal SodiumMg { get; }
        public decimal TotalCarbohydrate { get; }
        public decimal DietaryFiber { get; }
        public decimal SugarGrams { get; }
        public decimal Protein { get; }
        public decimal VitaminD { get; }
        public decimal CalciumMg { get; }
        public decimal IronMg { get; }
        public decimal PotassiumMg { get; }
        public decimal VitaminAMcg { get; }
        public decimal VitaminCMg { get; }

        private Nutrition() { }

        public Nutrition(
            decimal protein,
            decimal carb,
            decimal fat,
            decimal calories,
            decimal saturatedFat = 0,
            decimal transFat = 0,
            decimal cholesterol = 0,
            decimal sodiumMg = 0,
            decimal dietaryFiber = 0,
            decimal sugarGrams = 0,
            decimal vitaminD = 0,
            decimal calciumMg = 0,
            decimal ironMg = 0m,
            decimal potassiumMg = 0,
            decimal vitaminA_Mcg = 0,
            decimal vitaminC_Mg = 0)
        {

            if (calories < 0)
                throw new ArgumentException("Calories cannot be negative");

            if (protein < 0 || carb < 0 || fat < 0)
                throw new ArgumentException("Macros cannot be negative");

            if (saturatedFat < 0 || transFat < 0 || cholesterol < 0 || sodiumMg < 0 || dietaryFiber < 0 || sugarGrams < 0)
                throw new ArgumentException("Subcomponents of fats, carbs, cholesterol, sodium, fiber, and sugars cannot be negative.");

            if (vitaminD < 0 || calciumMg < 0 || ironMg < 0 || potassiumMg < 0 || vitaminA_Mcg < 0 || vitaminC_Mg < 0)
                throw new ArgumentException("Micronutrients cannot be negative");

            Protein = protein;
            TotalCarbohydrate = carb;
            TotalFat = fat;
            Calories = calories;
            SaturatedFat = saturatedFat;
            TransFat = transFat;
            Cholesterol = cholesterol;
            SodiumMg = sodiumMg;
            DietaryFiber = dietaryFiber;
            SugarGrams = sugarGrams;
            VitaminD = vitaminD;
            CalciumMg = calciumMg;
            IronMg = ironMg;
            PotassiumMg = potassiumMg;
            VitaminAMcg = vitaminA_Mcg;
            VitaminCMg = vitaminC_Mg;

            if (SaturatedFat + TransFat > TotalFat)
                throw new ArgumentException("Saturated and trans fat cannot exceed total fat");

            if (DietaryFiber > TotalCarbohydrate)
                throw new ArgumentException("Dietary fiber cannot exceed total carbohydrates");

            if (SugarGrams > TotalCarbohydrate)
                throw new ArgumentException("Sugar cannot exceed total carbohydrates");

            decimal totalMacroCalories = Protein * CaloriesPerGramOfProtein + TotalCarbohydrate * CaloriesPerGramOfCarb + TotalFat * CaloriesPerGramOfFat;

            if (totalMacroCalories > Calories)
                throw new ArgumentException("Macros exceed total calories");
        }

        public static Nutrition operator +(Nutrition a, Nutrition b)
        {
            return new Nutrition(
                protein: a.Protein + b.Protein,
                carb: a.TotalCarbohydrate + b.TotalCarbohydrate,
                fat: a.TotalFat + b.TotalFat,
                calories: a.Calories + b.Calories,
                saturatedFat: a.SaturatedFat + b.SaturatedFat,
                transFat: a.TransFat + b.TransFat,
                cholesterol: a.Cholesterol + b.Cholesterol,
                sodiumMg: a.SodiumMg + b.SodiumMg,
                dietaryFiber: a.DietaryFiber + b.DietaryFiber,
                sugarGrams: a.SugarGrams + b.SugarGrams,
                vitaminD: a.VitaminD + b.VitaminD,
                calciumMg: a.CalciumMg + b.CalciumMg,
                ironMg: a.IronMg + b.IronMg,
                potassiumMg: a.PotassiumMg + b.PotassiumMg,
                vitaminA_Mcg: a.VitaminAMcg + b.VitaminAMcg,
                vitaminC_Mg: a.VitaminCMg + b.VitaminCMg
            );
        }

        /// <summary>
        /// Multiplies all nutritional values by a positive factor.
        /// Commonly used for scaling per-100g values to a serving size.
        /// </summary>
        public Nutrition Multiply(decimal factor)
        {
            if (factor <= 0)
                throw new ArgumentException("Factor must be positive", nameof(factor));

            return new Nutrition(
                protein: Protein * factor,
                carb: TotalCarbohydrate * factor,
                fat: TotalFat * factor,
                calories: Calories * factor,
                saturatedFat: SaturatedFat * factor,
                transFat: TransFat * factor,
                cholesterol: Cholesterol * factor,
                sodiumMg: SodiumMg * factor,
                dietaryFiber: DietaryFiber * factor,
                sugarGrams: SugarGrams * factor,
                vitaminD: VitaminD * factor,
                calciumMg: CalciumMg * factor,
                ironMg: IronMg * factor,
                potassiumMg: PotassiumMg * factor,
                vitaminA_Mcg: VitaminAMcg * factor,
                vitaminC_Mg: VitaminCMg * factor
            );
        }
    }
}