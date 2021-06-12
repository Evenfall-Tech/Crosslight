using Antlr4.Runtime;
using Crosslight.API.IO.FileSystem;
using Crosslight.API.IO.FileSystem.Abstractions;
using Crosslight.API.IO.FileSystem.Implementations;
using Crosslight.API.Lang;
using Crosslight.Language.CSharp.Nodes.Generated;
using Crosslight.Language.CSharp.Nodes.Visitors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crosslight.Language.CSharp.Lang
{
    class CSharpInputLanguage : ILanguage
    {
        public string Name => "CSharp";
        public LanguageType LanguageType => LanguageType.Input;

        private CSharpVisitOptions options;
        public LanguageConfig Config { get; protected set; }
        public ILanguageOptions Options
        {
            get => options;
            set
            {
                options = value as CSharpVisitOptions;
            }
        }
        public void LoadOptionsFromConfig(LanguageConfig config)
        {
            throw new NotImplementedException();
        }

        public CSharpInputLanguage()
        {
            options = new CSharpVisitOptions();
        }

        public IFileSystemItem Translate(IFileSystemItem rootNode)
        {
            return ParseSource(rootNode, null);
        }

        private IFileSystemItem ParseSource(IFileSystemItem source, IDirectory parent = null)
        {
            if (source is IDirectory directory)
            {
                if (directory.Items.Count > 0)
                {
                    var resultingDirectory = FileSystem.CreateFileSystemCollection(directory.Name, parent);
                    var items = directory.Items
                        .Select(x => ParseSource(x, resultingDirectory));
                    foreach (var item in items)
                    {
                        resultingDirectory.Items.Add(item);
                    }
                    return resultingDirectory;
                }
                else return FileSystem.CreateFileSystemCollection(directory.Name, parent);
            }
            else if (source is IStringFile stringFile)
            {
                return ParseStringFile(stringFile, parent);
            }
            else if (source is IPhysicalFile physicalFile)
            {
                return ParsePhysicalFile(physicalFile, parent);
            }
            else throw new ArgumentException($"{source.GetType().Name} is not supported in {Name}.");
        }

        private IFileSystemItem ParseStringFile(IStringFile source, IDirectory parent = null)
        {
            throw new NotImplementedException($"{source.GetType().Name} is not supported in {Name}.");
        }

        private IFileSystemItem ParsePhysicalFile(IPhysicalFile source, IDirectory parent = null)
        {
            string path = source.Path;
            string code = File.ReadAllText(path);

            List<IToken> codeTokens = new List<IToken>();
            List<IToken> commentTokens = new List<IToken>();

            Lexer preprocessorLexer = new CSharpLexer(new AntlrInputStream(code));
            // Collect all tokens with lexer (CSharpLexer.g4).
            var tokens = preprocessorLexer.GetAllTokens();
            var directiveTokens = new List<IToken>();

            int index = 0;
            bool compiliedTokens = true;
            while (index < tokens.Count)
            {
                var token = tokens[index];
                if (token.Type == CSharpLexer.SHARP)
                {
                    directiveTokens.Clear();
                    int directiveTokenIndex = index + 1;
                    // Collect all preprocessor directive tokens.
                    while (directiveTokenIndex < tokens.Count &&
                           tokens[directiveTokenIndex].Type != CSharpLexer.Eof &&
                           tokens[directiveTokenIndex].Type != CSharpLexer.DIRECTIVE_NEW_LINE &&
                           tokens[directiveTokenIndex].Type != CSharpLexer.SHARP)
                    {
                        if (tokens[directiveTokenIndex].Channel == CSharpLexer.COMMENTS_CHANNEL)
                        {
                            commentTokens.Add(tokens[directiveTokenIndex]);
                        }
                        else if (tokens[directiveTokenIndex].Channel != Lexer.Hidden)
                        {
                            directiveTokens.Add(tokens[directiveTokenIndex]);
                        }
                        directiveTokenIndex++;
                    }

                    var directiveTokenSource = new ListTokenSource(directiveTokens);
                    var directiveTokenStream = new CommonTokenStream(directiveTokenSource, CSharpLexer.DIRECTIVE);
                    var preprocessorParser = new CSharpPreprocessorParser(directiveTokenStream);
                    // Parse condition in preprocessor directive (based on CSharpPreprocessorParser.g4 grammar).
                    CSharpPreprocessorParser.Preprocessor_directiveContext directive = preprocessorParser.preprocessor_directive();
                    // if true than next code is valid and not ignored.
                    compiliedTokens = directive.value;
                    String directiveStr = tokens[index + 1].Text.Trim();
                    if ("line" == directiveStr || "error" == directiveStr || "warning" == directiveStr || "define" == directiveStr || "endregion" == directiveStr || "endif" == directiveStr || "pragma" == directiveStr)
                    {
                        compiliedTokens = true;
                    }
                    String conditionalSymbol = null;
                    if ("define".Equals(tokens[index + 1].Text))
                    {
                        // add to the conditional symbols 
                        conditionalSymbol = tokens[index + 2].Text;
                        preprocessorParser.ConditionalSymbols.Add(conditionalSymbol);
                    }
                    if ("undef".Equals(tokens[index + 1].Text))
                    {
                        conditionalSymbol = tokens[index + 2].Text;
                        preprocessorParser.ConditionalSymbols.Remove(conditionalSymbol);
                    }
                    index = directiveTokenIndex - 1;
                }
                else if (token.Channel == CSharpLexer.COMMENTS_CHANNEL)
                {
                    commentTokens.Add(token); // Colect comment tokens (if required).
                }
                else if (token.Channel != Lexer.Hidden && token.Type != CSharpLexer.DIRECTIVE_NEW_LINE && compiliedTokens)
                {
                    codeTokens.Add(token); // Collect code tokens.
                }
                index++;
            }

            // At second stage tokens parsed in usual way.
            var codeTokenSource = new ListTokenSource(tokens);
            var codeTokenStream = new CommonTokenStream(codeTokenSource);
            CSharpParser parser = new CSharpParser(codeTokenStream);
            // Parse syntax tree (CSharpParser.g4)
            var compilationUnit = parser.compilation_unit();
            return new StringFile(Path.GetFileName(path), compilationUnit.ToStringTree());
        }
    }
}
