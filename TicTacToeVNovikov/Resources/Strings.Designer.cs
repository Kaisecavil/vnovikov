﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicTacToeVNovikov.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TicTacToeVNovikov.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Player Age must be beetwen {0} and {1} ages.
        /// </summary>
        internal static string AgeIsOutOfLimits {
            get {
                return ResourceManager.GetString("AgeIsOutOfLimits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Player#{0} ({1}) is winner.
        /// </summary>
        internal static string AnnouncementOfWinner {
            get {
                return ResourceManager.GetString("AnnouncementOfWinner", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input command: .
        /// </summary>
        internal static string AskForCommand {
            get {
                return ResourceManager.GetString("AskForCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Would you like to start new TicTacToe game?{0}..Press Enter to begin or any othe button to exit.
        /// </summary>
        internal static string AskForNewGame {
            get {
                return ResourceManager.GetString("AskForNewGame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Player #{0} Input your Id Name Age if you registered, or Input your Name Age if you are not registered.
        /// </summary>
        internal static string AskForPlayerInfo {
            get {
                return ResourceManager.GetString("AskForPlayerInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Player #{0} ({1}) input two numbers in range of [1-{2}]:.
        /// </summary>
        internal static string AskForPlayerTurn {
            get {
                return ResourceManager.GetString("AskForPlayerTurn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The command was executed and a file with the name {0} was created. It&apos;s Path :{1}.
        /// </summary>
        internal static string CommandExecuted {
            get {
                return ResourceManager.GetString("CommandExecuted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cordinates out of range of gamefield, must be two numbers from 1 to {0}.
        /// </summary>
        internal static string CordinatesOutOfRange {
            get {
                return ResourceManager.GetString("CordinatesOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DRAW.
        /// </summary>
        internal static string Draw {
            get {
                return ResourceManager.GetString("Draw", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Player name can&apos;t be empty or contain only whitespaces.
        /// </summary>
        internal static string EmptyPlayerName {
            get {
                return ResourceManager.GetString("EmptyPlayerName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to generate file-report in json format with ALL game results.
        /// </summary>
        internal static string GenerateAllResultsDescription {
            get {
                return ResourceManager.GetString("GenerateAllResultsDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to generate file-report in json format with last game result.
        /// </summary>
        internal static string GenerateLastGameResultDescription {
            get {
                return ResourceManager.GetString("GenerateLastGameResultDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to show all games where two current players take part together.
        /// </summary>
        internal static string GenerateResultsForCurrentPlayersDescription {
            get {
                return ResourceManager.GetString("GenerateResultsForCurrentPlayersDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to show you all available command and theirs description.
        /// </summary>
        internal static string HelpDescription {
            get {
                return ResourceManager.GetString("HelpDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Id = {0} is alredy occupied by another Player.
        /// </summary>
        internal static string IdIsOccupied {
            get {
                return ResourceManager.GetString("IdIsOccupied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid player info:.
        /// </summary>
        internal static string InvalidPlayerInfo {
            get {
                return ResourceManager.GetString("InvalidPlayerInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is for {1}.
        /// </summary>
        internal static string KeyIsForValue {
            get {
                return ResourceManager.GetString("KeyIsForValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;Your age can&apos;t be lesser than before&quot;.
        /// </summary>
        internal static string LesserAge {
            get {
                return ResourceManager.GetString("LesserAge", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This spot is alredy marked, try another.
        /// </summary>
        internal static string MarkedSpot {
            get {
                return ResourceManager.GetString("MarkedSpot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Write the necessary abbreviation to choose language.
        /// </summary>
        internal static string NecessaryAbbreviation {
            get {
                return ResourceManager.GetString("NecessaryAbbreviation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Abbreviation can&apos;t be null.
        /// </summary>
        internal static string NullAbbreviation {
            get {
                return ResourceManager.GetString("NullAbbreviation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command can&apos;t be null.
        /// </summary>
        internal static string NullCommand {
            get {
                return ResourceManager.GetString("NullCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Information about Player can&apos;t be null.
        /// </summary>
        internal static string NullPlayerInfo {
            get {
                return ResourceManager.GetString("NullPlayerInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Player name can&apos;t be null.
        /// </summary>
        internal static string NullPlayerName {
            get {
                return ResourceManager.GetString("NullPlayerName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Information about turn can&apos;t be null.
        /// </summary>
        internal static string NullPlayerTurnInfo {
            get {
                return ResourceManager.GetString("NullPlayerTurnInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Player name length must be beetwen {0} and {1} characters.
        /// </summary>
        internal static string PlayerNameLengthIsOutOfLimits {
            get {
                return ResourceManager.GetString("PlayerNameLengthIsOutOfLimits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to If you dont want to input commands anymore.
        /// </summary>
        internal static string SkipDescription {
            get {
                return ResourceManager.GetString("SkipDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to you made {0} mistakes in a row, your turn is skipped.
        /// </summary>
        internal static string SkippedTurn {
            get {
                return ResourceManager.GetString("SkippedTurn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Registation complited successfully, {0} your Id = {1} remember it!.
        /// </summary>
        internal static string SuccessfullRegistation {
            get {
                return ResourceManager.GetString("SuccessfullRegistation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The is no available localization with this abbriveation ({0}).
        /// </summary>
        internal static string UnknownAbbreviation {
            get {
                return ResourceManager.GetString("UnknownAbbreviation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In a list of commands there is no one with name {0}. you can use command /help to see all available commands.
        /// </summary>
        internal static string UnknownCommand {
            get {
                return ResourceManager.GetString("UnknownCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No similar was found among the registered players, check the correctness of the data entry or register by entering your Name Age.
        /// </summary>
        internal static string UnknownPlayer {
            get {
                return ResourceManager.GetString("UnknownPlayer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wrong format of Info, must be Id Name Age if you are registered or Name Age if you aren&apos;t.
        /// </summary>
        internal static string WrongFormatOfPlayerInfo {
            get {
                return ResourceManager.GetString("WrongFormatOfPlayerInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wrong format of Info,must be two numbers from 1 to {0}.
        /// </summary>
        internal static string WrongFormatOfTurnInfo {
            get {
                return ResourceManager.GetString("WrongFormatOfTurnInfo", resourceCulture);
            }
        }
    }
}
