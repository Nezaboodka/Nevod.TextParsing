﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace TextParser.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void LatinTest()
        {
            string testString = "The (\"brown\") can't 32.3 feet, right?";
            Tuple<string, TokenKind>[] expectedResult =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("The", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("(", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("\"", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("brown", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("\"", TokenKind.Punctuation),
                new Tuple<string, TokenKind>(")", TokenKind.Punctuation),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("can", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("'", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("t", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("32", TokenKind.Numeric),
                new Tuple<string, TokenKind>(".", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("3", TokenKind.Numeric),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("feet", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>(",", TokenKind.Punctuation),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("right", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("?", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("", TokenKind.End),
            };
            ParsePlainTextAndTestTokens(testString, expectedResult);
        }

        [TestMethod]
        public void OneWordTest()
        {
            string testString = "word";
            Tuple<string, TokenKind>[] expectedResult =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("word", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParsePlainTextAndTestTokens(testString, expectedResult);
        }

        [TestMethod]
        public void WhitespacesTest()
        {
            string testString = "  \t";
            Tuple<string, TokenKind>[] expectedResult =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("  \t", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParsePlainTextAndTestTokens(testString, expectedResult);
        }

        [TestMethod]
        public void SymbolsTest()
        {
            string testString = "#hashtag @name";
            Tuple<string, TokenKind>[] expectedResult =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("#", TokenKind.Symbol),
                new Tuple<string, TokenKind>("hashtag", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("@", TokenKind.Symbol),
                new Tuple<string, TokenKind>("name", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParsePlainTextAndTestTokens(testString, expectedResult);
        }


        [TestMethod]
        public void EmptyStringTest()
        {
            string testString = "";
            Tuple<string, TokenKind>[] expectedResult = 
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParsePlainTextAndTestTokens(testString, expectedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTest()
        {
            string testString = null;
            PlainTextParser.Parse(testString);
        }

        [TestMethod]
        public void CyrillicTest()
        {
            string testString = "1А класс; 56,31 светового, 45.1 дня!";
            Tuple<string, TokenKind>[] expectedResult =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("1А", TokenKind.Alphanumeric),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("класс", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>(";", TokenKind.Punctuation),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("56", TokenKind.Numeric),
                new Tuple<string, TokenKind>(",", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("31", TokenKind.Numeric),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("светового", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>(",", TokenKind.Punctuation),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("45", TokenKind.Numeric),
                new Tuple<string, TokenKind>(".", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("1", TokenKind.Numeric),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("дня", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("!", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParsePlainTextAndTestTokens(testString, expectedResult);
        }

        [TestMethod]
        public void OneSymbolTest()
        {
            string testString = "L";
            Tuple<string, TokenKind>[] expectedResult =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("L", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParsePlainTextAndTestTokens(testString, expectedResult);
        }

        [TestMethod]
        public void FormatCharactersTest()
        {
            string testString = "a\u0308b\u0308cd 3.4";
            Tuple<string, TokenKind>[] expectedResult =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("a\u0308b\u0308cd", TokenKind.Symbol),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("3", TokenKind.Numeric),
                new Tuple<string, TokenKind>(".", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("4", TokenKind.Numeric),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParsePlainTextAndTestTokens(testString, expectedResult);
        }

        [TestMethod]
        public void LineSeparatorTest()
        {
            string testString = "a\nb\r\nc\rd";
            Tuple<string, TokenKind>[] expectedResult =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("a", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("\n", TokenKind.LineFeed),
                new Tuple<string, TokenKind>("b", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("\r\n", TokenKind.LineFeed),
                new Tuple<string, TokenKind>("c", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("\r", TokenKind.LineFeed), 
                new Tuple<string, TokenKind>("d", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParsePlainTextAndTestTokens(testString, expectedResult);
        }

        [TestMethod]
        public void PlainTextMultipleParagraphTagsTest()
        {
            string testString = "First paragraph\n\nSecond paragraph\n\nThird paragraph ab";
            string[] expectedResult =
            {
                "First paragraph",
                "Second paragraph",
                "Third paragraph ab"
            };
            ParsePlainTextAndTestTags(testString, expectedResult);
        }

        [TestMethod]
        public void PlainTextSingleParapgraphTagTest()
        {
            string testString = "A\nsingle\nparagraph ab";
            string[] expectedResult =
            {
                "A\nsingle\nparagraph ab"
            };
            ParsePlainTextAndTestTags(testString, expectedResult);
        }

        [TestMethod]
        public void XhtmlElementsTagAfterRootTagTest()
        {
            string testString = "<html><body>Test <empty/><b>string</b></body></html>";
            string[] expectedResult = { "<html>", "<body>", "Test ", "<empty/>", "<b>", "string", "</b>", "</body>", "</html>" };
            ParsePlainTextAndTestXhtmlElements(testString, expectedResult);
        }

        [TestMethod]
        public void XhtmlElementsTextAfterRootTagTest()
        {
            string testString = "<html>Some text</html>";
            string[] expectedResult = {"<html>", "Some text", "</html>" };
            ParsePlainTextAndTestXhtmlElements(testString, expectedResult);
        }

        [TestMethod]
        public void XhtmlElementsSingleRootTagTest()
        {
            string testString = "<html></html>";
            string[] expectedResult = {"<html>", "</html>"};
            ParsePlainTextAndTestXhtmlElements(testString, expectedResult);
        }

        [TestMethod]
        public void XhtmlElementsDocumentTagsTest()
        {
            string testString = @"<?xml version=""1.0"" encoding=""UTF-8""?><html>
                <head>
                <meta name=""Author"" content=""Иван Шимко""/>                
                <title>Title</title>
                </head><body><p>First paragraph.</p>                                
                </body></html>";
            string[] expectedRestult = { "<body>", "<p>", "First paragraph.","</p>", "</body>" };
            ParsePlainTextAndTestXhtmlElements(testString, expectedRestult);
        }

        [TestMethod]
        public void XhtmlElementsDocumentTagsEmptyTitleTest()
        {
            string testString = @"<?xml version=""1.0"" encoding=""UTF-8""?><html>
                <head>
                <meta name=""Author"" content=""Иван Шимко""/>                
                <title/>
                </head><body><p>First paragraph.</p>                                
                </body></html>";
            string[] expectedRestult = { "<body>", "<p>", "First paragraph.", "</p>", "</body>" };
            ParsePlainTextAndTestXhtmlElements(testString, expectedRestult);
        }

        [TestMethod]
        public void XhtmlTokensCompoundTokensTest()
        {
            string testString = "<p>Hello, <b>w</b>orld!</p>";
            Tuple<string, TokenKind>[] expectedTokens =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("Hello", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>(",", TokenKind.Punctuation),
                new Tuple<string, TokenKind>(" ", TokenKind.WhiteSpace),
                new Tuple<string, TokenKind>("world", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("!", TokenKind.Punctuation),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParseXhtmlAndTestTokens(testString, expectedTokens);
        }

        [TestMethod]
        public void XhtmlTokensOneSymbolTokensTest()
        {
            string testString = "<html><p>a</p><p>b</p><p>c</p><p>d</p></html>";
            Tuple<string, TokenKind>[] expectedTokens =
            {
                new Tuple<string, TokenKind>("", TokenKind.Start),
                new Tuple<string, TokenKind>("a", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("b", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("c", TokenKind.Alphabetic),               
                new Tuple<string, TokenKind>("d", TokenKind.Alphabetic),
                new Tuple<string, TokenKind>("", TokenKind.End)
            };
            ParseXhtmlAndTestTokens(testString, expectedTokens);
        }

        [TestMethod]
        public void XhtmlTagsParagraphsTest()
        {
            string testString = "<html><p>Paragraph1</p>\n<p>Paragraph2</p></html>";
            string[] expectedTags =
            {
                "Paragraph1",
                "Paragraph2"
            };
            ParseXhtmlAndTestTags(testString, expectedTags);
        }

        [TestMethod]
        public void XhtmlTagsOneSymbolTokensTest()
        {
            string testString = "<html><p>a</p><p>b</p><p>c</p></html>";
            string[] expectedTags =
            {
                "a",
                "b",
                "c"
            };
            ParseXhtmlAndTestTags(testString, expectedTags);
        }

        [TestMethod]
        public void XhtmlTagsEmptyTagTest()
        {
            string testString = "<html><p></p></html>";
            string[] expectedTags = { };
            ParseXhtmlAndTestTags(testString, expectedTags);
        }

        [TestMethod]
        public void XhtmlTagsSelfClosingTagTest()
        {
            string testString = "<html><p/></html>";
            string[] expectedTags = { };
            ParseXhtmlAndTestTags(testString, expectedTags);
        }

        [TestMethod]
        public void XhtmlTagsCompoundTokensTest()
        {
            string testString = "<html><p>Paragraph<b>1</b>\nstill paragraph1</p>\n<p>Paragraph2</p></html>";
            string[] expectedTags =
            {
                "Paragraph1\nstill paragraph1",
                "Paragraph2"
            };
            ParseXhtmlAndTestTags(testString, expectedTags);
        }

        [TestMethod]
        public void XhtmlDocumentTagsTest()
        {
            string testString =
                @"<?xml version=""1.0"" encoding=""UTF-8""?><html>
                <head>
                <meta name=""Author"" content=""Иван Шимко""/>
                <meta name=""publisher"" content=""Home""/>
                <meta name=""meta:page-count"" content=""1""/>
                <meta name=""dc:publisher"" content=""Home""/>
                <title>Title</title>
                </head>
                <body><h1>Title</h1>                                                                
                <p>First paragraph.</p>                                
                </body></html>";
            Tuple<string, string>[] expectedResult = 
            {
                new Tuple<string, string>("Author", "Иван Шимко"), 
                new Tuple<string, string>("publisher", "Home"), 
                new Tuple<string, string>("meta:page-count", "1"), 
                new Tuple<string, string>("dc:publisher", "Home"),
                new Tuple<string, string>("title", "Title"),
            };
            ParseXhtmlAndTestDocumentTags(testString, expectedResult);
        }

        [TestMethod]
        public void XhtmlDocumentTagsEmptyTitleTest()
        {
            string testString =
                @"<?xml version=""1.0"" encoding=""UTF-8""?><html>
                <head>
                <meta name=""Author"" content=""Иван Шимко""/>
                <title/>
                </head>
                <body><h1>Title</h1>                                                                
                <p>First paragraph.</p>                                
                </body></html>";
            Tuple<string, string>[] expectedResult =
            {
                new Tuple<string, string>("Author", "Иван Шимко"),
            };
            ParseXhtmlAndTestDocumentTags(testString, expectedResult);
        }

        // Static internal

        private static void ParsePlainTextAndTestTokens(string testString, Tuple<string, TokenKind>[] expectedResult)
        {
            Tuple<string, TokenKind>[] result = GetTokensFromParsedText(PlainTextParser.Parse(testString));
            CollectionAssert.AreEqual(result, expectedResult);
        }

        private static void ParsePlainTextAndTestTags(string testString, string[] expectedTags)
        {
            string[] actualTags = GetTagsFromParsedText(PlainTextParser.Parse(testString));
            CollectionAssert.AreEqual(expectedTags, actualTags);
        }

        private static void ParsePlainTextAndTestXhtmlElements(string testString, string[] expectedRestult)
        {
            string[] actualResult = XhtmlParser.Parse(testString).XhtmlElements.ToArray();
            CollectionAssert.AreEqual(expectedRestult, actualResult);
        }

        private static void ParseXhtmlAndTestTokens(string testString, Tuple<string, TokenKind>[] expectedTokens)
        {
            ParsedText parsedText = XhtmlParser.Parse(testString);
            Tuple<string, TokenKind>[] actualTokens = GetTokensFromParsedText(parsedText);
            CollectionAssert.AreEqual(expectedTokens, actualTokens);
        }

        private static void ParseXhtmlAndTestTags(string testString, string[] expectedTags)
        {
            ParsedText parsedText = XhtmlParser.Parse(testString);
            string[] actualTags = GetTagsFromParsedText(parsedText);
            CollectionAssert.AreEqual(expectedTags, actualTags);
        }        

        private static void ParseXhtmlAndTestDocumentTags(string testString, Tuple<string, string>[] expectedResult)
        {
            ParsedText parsedText = XhtmlParser.Parse(testString);
            Tuple<string, string>[] actualResult = GetDocumentTagsFromParsedText(parsedText);
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        private static string[] GetTagsFromParsedText(ParsedText parsedText)
        {
            return parsedText.FormattingTags.Select(parsedText.GetTagText).ToArray();
        }

        private static Tuple<string, TokenKind>[] GetTokensFromParsedText(ParsedText parsedText)
        {
            return parsedText
                .PlainTextTokens
                .Select(x => new Tuple<string, TokenKind>(parsedText.GetTokenText(x), x.TokenKind))
                .ToArray();
        }

        private static Tuple<string, string>[] GetDocumentTagsFromParsedText(ParsedText parsedText)
        {
            return parsedText.DocumentTags.Select(tag => new Tuple<string, string>(tag.TagName, tag.Content)).ToArray();
        }
    }
}
