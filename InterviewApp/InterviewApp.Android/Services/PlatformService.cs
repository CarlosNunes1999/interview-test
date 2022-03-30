using InterviewApp.Droid.Services;
using InterviewApp.Interfaces;
using System.Runtime.CompilerServices;
// I register the Dependency directly , and the cause of the bug was that the dependency was not registered ;
[assembly: Xamarin.Forms.Dependency(typeof(PlatformService))]

namespace InterviewApp.Droid.Services
{
    public class PlatformService : IPlatformService
    {
        // This could be any Android specific code. While this specific code may be possible to do in a
        // cross-platform manner, treat it as if it is neccesary to be called from the Android project.
        public string GetPlatformSpecificString() => $"{Android.OS.Build.Manufacturer} {Android.OS.Build.Model}".ToUpper();
    }
}
