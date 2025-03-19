using DddInPractice.Logic;
using DomainDrivenDesign.Logic.Management;

namespace Logic
{
    public static class Initer
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
            HeadOfficeInstance.Init();
        }
    }
}
