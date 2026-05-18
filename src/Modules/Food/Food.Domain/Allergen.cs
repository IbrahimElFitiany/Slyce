namespace Food.Domain
{
    public sealed class Allergen (string name)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = name;
    }
}
