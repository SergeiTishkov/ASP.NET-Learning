namespace SamopalIndustries.Entities.Enums
{
    /// <summary>
    /// Kind of constructor to late binding new instances.
    /// </summary>
    public enum LateBindingOptions
    {
        /// <summary>
        /// Default constructor will be used for late binding.
        /// If there isn't default constructor, LateBindingException will be thrown.
        /// </summary>
        DefaultCtor,

        /// <summary>
        /// Default constructor will be used for late binding.
        /// If there isn't default constructor, constructor with minimal number of arguments will be invoked instead.
        /// </summary>
        DefaultOrMinCtor,

        /// <summary>
        /// Default constructor will be used for late binding.
        /// If there isn't default constructor, constructor with maximal number of arguments will be invoked instead.
        /// </summary>
        DefaultOrMaxCtor,

        /// <summary>
        /// Constructor with maximal number of arguments will be used for late binding.
        /// This option is default for SamopalDI and CoolDI.
        /// </summary>
        MaxCtor
    }
}
