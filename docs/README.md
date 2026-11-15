# DSAPI HTML API documentation

This folder defines a DocFX site generated from the **public** RKM.DataStage.DSAPI library.

The source of truth for API descriptions is the C# XML documentation in `src/RKM.DataStage.DSAPI/`. Do not hand-edit the generated API YAML or HTML. DocFX **2.81.0** is pinned in the repository's `.config/dotnet-tools.json` manifest.

## Generate locally

From the repository root on a Windows machine with the .NET 10 SDK:

```powershell
dotnet restore src/RKM.DataStage.DSAPI/RKM.DataStage.DSAPI.csproj
dotnet build src/RKM.DataStage.DSAPI/RKM.DataStage.DSAPI.csproj -c Release -f net10.0-windows --no-restore
dotnet tool restore
dotnet tool run docfx -- docs/docfx.json
```

The HTML site is created at `docs/_site/`. To preview it, run:

```powershell
dotnet tool run docfx -- serve docs/_site
```

DocFX reads the built **net10.0-windows** assembly and its adjacent `RKM.DataStage.DSAPI.xml` comment file, generating the public library API pages without requiring a DataStage connection or native IBM DLL.

## GitHub build artifact

The `Build DSAPI API documentation` GitHub Actions workflow generates and validates the site on every push to `main`, pull request and manual workflow dispatch. Download the `dsapi-html-reference` artifact from a successful workflow run to inspect the HTML.

This workflow **does not publish** the site, modify the RKM Research website, or push any package to NuGet.org.

## Website publishing

The generated reference is published at https://rkmresearch.com/docs/dsapi/.

After documentation changes are merged, run **Publish DSAPI documentation to website** on `main` in the `RKM-Research/rkmresearch-website` repository. That workflow rebuilds from this repository's `main`, copies the complete generated site, and commits it as **Documentation improvements**. The website's existing Cloudflare Pages Git integration then deploys the update.

Regenerate the complete site so guide pages, API links, navigation and search indexes stay together. Do not hand-edit generated HTML. This process does not publish a NuGet package.

---

IBM, DataStage and InfoSphere are trademarks or registered trademarks of International Business Machines Corporation in the United States, other countries, or both. RKM Research Ltd is independent of IBM; these tools and documents are not affiliated with, sponsored by or endorsed by IBM. Other product and company names belong to their respective trademark owners.
