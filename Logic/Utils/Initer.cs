using DddInPractice.Logic;
using DomainDrivenDesign.Logic.Common;
using DomainDrivenDesign.Logic.Management;

namespace DomainDrivenDesign.Logic.Utils
{
    public static class Initer
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
            HeadOfficeInstance.Init();
            DomainEvents.Init();
        }
    }
}
