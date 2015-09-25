using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Dnx.Compilation.CSharp;
using Microsoft.Dnx.Runtime;

public class AnalyzersCompilationModule : ICompileModule
{
    public void BeforeCompile(BeforeCompileContext context)
    {
        string analyzerAssemblyPath = Path.Combine(context.ProjectContext.ProjectDirectory, @"../../lib/DotNetDoodle.Analyzers.dll");
        ImmutableArray<DiagnosticAnalyzer> diagnosticAnalyzers = new AnalyzerFileReference(analyzerAssemblyPath, FromFileLoader.Instance).GetAnalyzers(LanguageNames.CSharp);
        var compilation = context.Compilation.WithAnalyzers(diagnosticAnalyzers);
        ImmutableArray<Diagnostic> diagsnostics = compilation.GetAnalyzerDiagnosticsAsync().Result;

        foreach (var diagsnostic in diagsnostics)
        {
            context.Diagnostics.Add(diagsnostic);
        }
    }

    public void AfterCompile(AfterCompileContext context)
    {
    }
    
    private class FromFileLoader : IAnalyzerAssemblyLoader
    {
        public static FromFileLoader Instance = new FromFileLoader();

        public void AddDependencyLocation(string fullPath)
        {
        }

        public Assembly LoadFromPath(string fullPath)
        {
            return Assembly.LoadFrom(fullPath);
        }
    }
}