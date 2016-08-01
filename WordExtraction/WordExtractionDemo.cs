﻿using System;

namespace WordExtraction
{
    class WordExtractionDemo
    {
        static void Main(string[] args)
        {
            WordExtractor wordExtractor = new StandardWordExtractor();
            foreach (string arg in args)
            {
                foreach (var word in wordExtractor.GetWords(arg))
                {
                    Console.WriteLine("\"" + word + "\"");
                }
            }
        }
    }
}
