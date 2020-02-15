using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1034", Justification = "xUnit test structure relies on nested classes")]
[assembly: SuppressMessage("Microsoft.Design", "CA1822", Justification = "Unit tests often don't access instance data but the runner requires them to be instance members")]
[assembly: SuppressMessage("Microsoft.Design", "CA1801", Justification = "Unit tests often have dummy classes that contain params that aren't used")]