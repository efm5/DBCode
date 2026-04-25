# ThrowIfNull Refactoring Script
# Automatically converts null-check-and-return patterns to ThrowIfNull calls

$ErrorActionPreference = "Stop"
$filesProcessed = 0
$changesApplied = 0

# Target files from the analysis
$targetFiles = @(
    "DBCode\Diagnostics.cs",
    "DBCode\EnsurePanels.cs",
    "DBCode\LayoutHelpers\Clusters\TextFieldCluster.cs",
    "DBCode\LayoutHelpers\LayoutHelpersControlLines.cs",
    "DBCode\LayoutHelpers\LayoutHelpersFont.cs",
    "DBCode\LayoutHelpers\LayoutHelpersListBox.cs",
    "DBCode\LayoutHelpers\LayoutHelpersMath.cs",
    "DBCode\LayoutHelpers\LayoutHelpersScreen.cs",
    "DBCode\LayoutHelpers\LayoutHelpersUI.cs",
    "DBCode\LayoutHelpers\LayoutHelpersWidget.cs",
    "DBCode\MainEvents.cs",
    "DBCode\MainForm.cs",
    "DBCode\MainSupportMethods.cs",
    "DBCode\Pickers\ColorPickerEvents.cs",
    "DBCode\Pickers\ColorPickerPanel.cs",
    "DBCode\Pickers\FontPickerEvents.cs",
    "DBCode\Pickers\FontPickerPanel.cs",
    "DBCode\Pickers\ThemePickerEvents.cs",
    "DBCode\Themes\ThemeBinder.cs",
    "DBCode\Themes\ThemeDiagnostics.cs",
    "DBCode\Themes\ThemePanel.cs",
    "DBCode\Themes\ThemePanelEvents.cs",
    "DBCode\Themes\ThemePreviewRenderer.cs"
)

Write-Host "Starting ThrowIfNull refactoring..." -ForegroundColor Cyan
Write-Host "Files to process: $($targetFiles.Count)" -ForegroundColor Yellow

foreach ($file in $targetFiles) {
    if (-not (Test-Path $file)) {
        Write-Host "  [SKIP] File not found: $file" -ForegroundColor Yellow
        continue
    }

    Write-Host "`nProcessing: $file" -ForegroundColor Green
    $content = Get-Content $file -Raw
    $originalContent = $content

    # Pattern 1: Simple null check with return
    # if (variable == null) return;
    $pattern1 = '(?m)^\s*if\s*\((\w+)\s*==\s*null\)\s*return;?\s*$'
    $replacement1 = '         ThrowIfNull($1, nameof($1));'
    $content = $content -replace $pattern1, $replacement1

    # Pattern 2: Null check with return statement
    # if (variable == null)
    #    return;
    $pattern2 = '(?m)^\s*if\s*\((\w+)\s*==\s*null\)\s*\r?\n\s*return;?\s*$'
    $replacement2 = '         ThrowIfNull($1, nameof($1));'
    $content = $content -replace $pattern2, $replacement2

    # Pattern 3: Null check with return value
    # if (variable == null)
    #    return false;
    $pattern3 = '(?m)^\s*if\s*\((\w+)\s*==\s*null\)\s*\r?\n\s*return\s+\w+;?\s*$'
    $replacement3 = '         ThrowIfNull($1, nameof($1));'
    $content = $content -replace $pattern3, $replacement3

    if ($content -ne $originalContent) {
        Set-Content $file $content -NoNewline
        $filesProcessed++
        $changesApplied += ([regex]::Matches($originalContent, $pattern1).Count + 
                           [regex]::Matches($originalContent, $pattern2).Count +
                           [regex]::Matches($originalContent, $pattern3).Count)
        Write-Host "  [DONE] Changes applied" -ForegroundColor Green
    } else {
        Write-Host "  [INFO] No simple patterns found (may need manual review)" -ForegroundColor Gray
    }
}

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Refactoring Summary:" -ForegroundColor Cyan
Write-Host "  Files processed: $filesProcessed" -ForegroundColor Yellow
Write-Host "  Changes applied: ~$changesApplied" -ForegroundColor Yellow
Write-Host "`nNext steps:" -ForegroundColor Cyan
Write-Host "  1. Review the changes in your diff viewer" -ForegroundColor White
Write-Host "  2. Run: dotnet build" -ForegroundColor White
Write-Host "  3. Fix any compound null checks manually" -ForegroundColor White
Write-Host "  4. Remove null-forgiving operators (!) where appropriate" -ForegroundColor White
Write-Host "========================================`n" -ForegroundColor Cyan
