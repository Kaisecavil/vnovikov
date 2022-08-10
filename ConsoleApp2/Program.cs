// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using ConsoleApp2;
using ConsoleApp2.Constants;

internal class Program
{
    private static void Main(string[] args)
    { 
        GameEngine gameEngine = new GameEngine();
        gameEngine.GameStart();
    }  
    
}