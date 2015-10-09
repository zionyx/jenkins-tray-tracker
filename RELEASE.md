# Release Procedure

0. Tools to use for markdown: http://jbt.github.io/markdown-editor/

## Pre-Release Procedures:
- Update `common\VersionInfo.cs`
```csharp
[assembly: System.Reflection.AssemblyFileVersion("X.X.X.0")]
[assembly: System.Reflection.AssemblyInformationalVersion("X.X.X.0")]
```
(Where X.X.X = Major.Minor.Build version number. The last digit is not used as Revision.)
- Commit and push to GitHub.

## Pre-Release Notes Procedures:
0. Find all the closed issues against a certain milestone: https://github.com/zionyx/jenkins-tray/milestones
0. Assure all issues closed in the release has the correct milestone set.
0. Draft a new release on GitHub, https://github.com/zionyx/jenkins-tray/releases/new
0. Tag master with an alpha tag with `-alpha` postfix. Eg: `vX.X.X-alpha`
0. Use previous releases as guides, will update it here soon.
0. Mark release as `This is a pre-release`.
0. Publish release notes.

## Post-Release Procedures:
0. Upon build completion, authenticode sign the new MSI file, with naming like `JenkinsTray_vX.X.X.0.msi`.
0. Update `scripts\version.properties`:
  0. `version.number=X.X.X.0`
  0. `version.installerUrl=https://github.com/zionyx/jenkins-tray/releases/download/vX.X.X/JenkinsTray_vX.X.X.0.msi`
0. Commit and push to GitHub.
0. Edit the pre-release notes.
  0. Upload the signed MSI file to the release.
  0. Replace the alpha tag with the same version tag without `-alpha` postfix. Eg: `vX.X.X`.
  (This is important! It has to match the `version.installerUrl` in `scripts\version.properties` file.
0. Uncheck `This is a pre-release`.
0. Publish the release.
0. Remove the alpha tag later.

## Side Note:
0. Build has to be made on a build environment with licensed DevExpress installed.