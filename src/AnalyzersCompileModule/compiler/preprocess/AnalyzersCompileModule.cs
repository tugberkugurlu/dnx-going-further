using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.Runtime.Roslyn;

public class AnalyzersCompileModule : ICompileModule
{
    private readonly string _basePath;

    public AnalyzersCompileModule(IApplicationEnvironment appEnv)
    {
        _basePath = appEnv.ApplicationBasePath;
    }

    public void BeforeCompile(IBeforeCompileContext context)
    {
        string analyzerAssemblyPath = Path.Combine(_basePath, @"../../lib/DotNetDoodle.Analyzers.dll");
        ImmutableArray<DiagnosticAnalyzer> diagnosticAnalyzers = new AnalyzerFileReference(analyzerAssemblyPath).GetAnalyzers(LanguageNames.CSharp);
        var compilation = context.Compilation.WithAnalyzers(diagnosticAnalyzers);
        ImmutableArray<Diagnostic> diagsnostics = compilation.GetAnalyzerDiagnosticsAsync().Result;

        foreach (var diagsnostic in diagsnostics)
        {
            context.Diagnostics.Add(diagsnostic);
        }
    }

    public void AfterCompile(IAfterCompileContext context)
    {
    }
}