﻿using System;
using System.Diagnostics;
using System.IO;
using TextParser;

namespace TextParserDemoApplication
{
    public class ParserDemo
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            string text = File.ReadAllText(fileName);
            long fileSize = new FileInfo(fileName).Length;            
            Console.WriteLine($"{fileSize} bytes");
            MeasurePlainTextParsing(text);
            MeasureXhtmlParsing(text);
        }

        // Static internal

        private static void MeasurePlainTextParsing(string text)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            PlainTextParser.Parse(text);
            stopwatch.Stop();
            Console.WriteLine($"As plain text: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void MeasureXhtmlParsing(string text)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            XhtmlParser.Parse(text);
            stopwatch.Stop();
            Console.WriteLine($"As xhtml: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
