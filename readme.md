# .NET File-based app templates

A minimal template pack for .NET file-based apps built around fast, file-first development. These templates are designed for people who want to scaffold practical starting points without creating full project structures up front. The package focuses on a small, opinionated set of defaults so each template is easy to understand and modify. As the pack grows, new templates will follow the same conventions for naming, options, and local development workflow.

This package currently includes two templates:

- [Console (file-based app)](#console-file-based-app)
- [Minimal API (file-based app)](#minimal-api-file-based-app)

## Install

Install the latest published package from NuGet with the following command.

```shell
dotnet new install dotnet-file-based-app-templates
```

## Usage

Use `dotnet new <template-name>` to generate a file-based app in the current directory.

```shell
dotnet new console-file

dotnet run console.cs
```

```shell
dotnet new webapi-file

dotnet run api.cs
```

## Templates

Each template in this package is intentionally minimal and optimized for direct file-based app workflows. You can generate and run quickly, then evolve the files into whatever structure you need.

Template names use the `-file` suffix so they are easy to distinguish from built-in project templates. This also keeps discovery simple when listing templates by tags.

### Console file-based app

The console template generates a single `console.cs` file and defaults to a Spectre.Console-based greeting panel for a richer terminal experience. This provides a better out-of-the-box visual result while keeping the app minimal and easy to extend with formatting, prompts, progress indicators, and other CLI-focused UX patterns.

When you want the smallest possible baseline with no extra package dependency, you can switch to plain mode. That option produces a straightforward `Console.WriteLine` starter while keeping the same template shape and command flow.

> [!TIP]
> If you prefer no external package and plain output, use the `--plain` option. In that mode, the generated app uses `Console.WriteLine` only.

#### Options

The console template currently exposes one user-facing option.

| Option | Type | Default | Description |
| --- | --- | --- | --- |
| `--plain` | `bool` | `false` | Generates a plain `Console.WriteLine` version |

### Minimal API file-based app

The minimal API template generates an `api.cs` file that demonstrates a practical, AOT-compatible API setup end-to-end. It uses `WebApplication.CreateSlimBuilder`, registers services with dependency injection, configures HTTP JSON serialization with source-generated metadata, and exposes a small set of endpoints that show both simple responses and DI-backed handlers.

The template also enables OpenAPI support so you can inspect and test the API surface quickly, and it includes `api.http` for immediate request testing in editors that support HTTP files. The goal is to provide a realistic reference for modern minimal APIs that keep performance-focused AOT settings without giving up day-to-day developer features.

## Local development

Fork and clone this repository, then run these commands.

1. Build and pack to the dedicated package folder.

    ```shell
    dotnet build
    ```

    ```shell
    dotnet pack --configuration release --output .\pkg
    ```

1. Install from the local package output using a wildcard.

    ```shell
    dotnet new install .\pkg\dotnet-file-based-app-templates.*.nupkg
    ```

1. Verify the new templates are installed using the `file-based` tag filter.

    ```shell
    dotnet new list --tag file-based
    ```

1. Test one of the templates by creating a new project and running it.

    | | Generate project | Run application |
    | --- | --- | --- |
    | **Console** | `dotnet new console-file` | `dotnet run console.cs` |
    | **Minimal API** | `dotnet new webapi-file` | `dotnet run api.cs` |

1. Once done, clean up the local installation.

    ```shell
    dotnet new uninstall dotnet-file-based-app-templates
    ```

## Contributing

Follow these steps when adding a new template.

1. Create a new folder under `content/` using the `-file` suffix.
1. Add the template source files and a `.template.config/template.json` file.
1. Add any template options to the template's `symbols` section and document them in this README.
1. Update the `Templates`, `Usage`, and `Local development` sections in this README if the new template changes the documented workflow.
1. Extend the validation coverage in `.github/workflows/validation.yml` so the new template is generated and minimally validated.
1. Add or update the corresponding test so the new template has an automated validation path.

### Best practices

Use these guidelines to keep new templates aligned with the existing package structure and developer experience.

- Keep generated output filenames intentional and stable, such as `console.cs` or `api.cs`.
- Prefer the smallest practical file set for the template scenario.
- Keep new templates consistent with the existing `console-file` and `webapi-file` conventions.
