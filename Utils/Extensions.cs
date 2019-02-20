using System;
namespace mongoConsole.Utils 
{
    public static class Extensions
    {
        public static bool IsValidCommand(this string[] commandArguments) 
        {
            var command = Enum.Parse(typeof(ValidCommands), commandArguments[0],true);
            
            switch(command) {
                case ValidCommands.Show:
                    return true;
                case ValidCommands.Add:
                    return !string.IsNullOrWhiteSpace(commandArguments[1]) 
                        && !string.IsNullOrWhiteSpace(commandArguments[2]);
                case ValidCommands.Remove:
                case ValidCommands.Update:
                    return !string.IsNullOrWhiteSpace(commandArguments[1]);
                default:
                    return false;
            }
        }

        public static string GetOrDefault(this string[] array, int index, string defaultValue = "")
        {
            return index < array.Length ? array[index]: defaultValue;
        }

    }
}