namespace Serialize.Linq.Core.Factories
{
    public class FactorySettings
    {
        public FactorySettings()
        {
            this.UseRelaxedTypeNames = true;
        }

        public bool UseRelaxedTypeNames { get; set; }

        public bool AllowPrivateFieldAccess { get; set; }
    }
}
